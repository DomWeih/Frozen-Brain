using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManagement : MonoBehaviour
{


    private float firstEventTime = 5f;
    private float EventTime = 5f;
    public int monthlyCost = 100;

    private int anzahl_seiten = 2;

    private int newChance = 0;
    public GameObject BlackMarket;
    public GameObject BMBody;

    [SerializeField] Slider PaymentSlider;
    [SerializeField] TMP_Text PaymentText;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("NewCustomer", firstEventTime, EventTime);
        InvokeRepeating("MonthlyPayments", 0, 5);
        InvokeRepeating("NewBody", 0, 10);
        PaymentText.text = monthlyCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changePayment()
    {
        monthlyCost = (int)PaymentSlider.value;
        PaymentText.text = monthlyCost.ToString();
        Debug.Log(monthlyCost);
    }

    public void NewCustomer()
    {
        switch(this.monthlyCost) {
            case var _ when this.monthlyCost >= 100 && this.monthlyCost < 300:
                anzahl_seiten = 2;
            break;
            case var _ when this.monthlyCost >= 300 && this.monthlyCost < 600:
                anzahl_seiten = 4;
            break;
            case var _ when this.monthlyCost >= 600 && this.monthlyCost < 800:
                anzahl_seiten = 8;
            break;
            case var _ when this.monthlyCost >= 800 && this.monthlyCost < 900:
                anzahl_seiten = 12;
            break;
            case var _ when this.monthlyCost >= 900 && this.monthlyCost < 1000:
                anzahl_seiten = 20;
            break;
        }

        newChance = Random.Range(1,anzahl_seiten);
        if (newChance == 1)
        {
            PlayerManager.s_Customers++;
        }
        if (newChance % 2 == 0 && PlayerManager.s_Customers > 0) PlayerManager.s_Customers--;
        
        // int newChance = Random.Range(1, 100);

        // //new Customer
        // if(newChance < 50 && newChance != 1)
        // {
        //     PlayerManager.s_Customers++;
        // }
        // else if (newChance >= 51 && newChance <= 75) return;
        // else if (newChance >75 && newChance != 100) PlayerManager.s_Customers--;


        // else if (newChance == 100) PlayerManager.s_Customers -= 10;
        // else if (newChance == 1) PlayerManager.s_Customers += 10;

    }
    public void NewBody()
    {
        
        //insurance that gets paid on customers death
        if(PlayerManager.s_Customers > 0 && (PlayerManager.s_BodyTank - PlayerManager.s_CurrentBodies) > 0)
        {
            int insurace = Random.Range(25000, 250000);
            PlayerManager.s_Money += insurace;
            //Customer becomes a body
            PlayerManager.s_Customers--;
            PlayerManager.s_CurrentBodies++;
            Instantiate (BMBody, BlackMarket.transform);
            Debug.Log(insurace);
        }
        else Debug.Log("no place left / no customers");
        
    }

    public void MonthlyPayments()
    {
        PlayerManager.s_Money += (monthlyCost * PlayerManager.s_Customers);
        PlayerManager.s_lastCPayment = (monthlyCost * PlayerManager.s_Customers);
    }
}
