using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Security.Cryptography;

public class UIScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] TMP_Text Money;
    [SerializeField] TMP_Text LiquidNitrogen;
    [SerializeField] TMP_Text BodyCapacity;
    [SerializeField] TMP_Text Customers;

    public static bool mouse_over = false;

    #region NitrogenMarket
    [SerializeField] Selectable button;
    #endregion



    #region mouse hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Money.text = PlayerManager.s_Money.ToString("N2");
        LiquidNitrogen.text = PlayerManager.s_MaxLNitrogen.ToString() + "L / " + PlayerManager.s_LiquidNitrogen + " L";
        BodyCapacity.text = PlayerManager.s_BodyTank.ToString() + " / " + PlayerManager.s_CurrentBodies;
        Customers.text = PlayerManager.s_Customers.ToString();

        if((PlayerManager.s_MaxLNitrogen - PlayerManager.s_LiquidNitrogen) < 1000)
        {
            button.interactable = false;
        }
        else button.interactable = true;
    }


}
