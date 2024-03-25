using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{

    public InputAction playerControls;

    Vector2 moveDirection = Vector2.zero;

    //Main Resources
    public static int s_Money = 0;
    public static int s_operatingCost = 0;
    public static int s_LiquidNitrogen = 0;
    public static int s_MaxLNitrogen = 0;
    public static int NitrogenUsage = 0;
    public static int NitrogenGeneration = 0;

    //Cryo Resources(Body, Heads, etc.)
    public static int s_BodyTank = 0;
    public static int s_CurrentBodies = 0;
    public static int s_Customers = 0;
    public static int s_lastCPayment = 0;
    public TMP_Text moneyChange;





    Camera main;
    Vector3 cameraPosition;
    Quaternion cameraRotation;
    public float cameraSpeed;
    // Start is called before the first frame update
    void Start()
    {
        s_Money = 100000;
        s_LiquidNitrogen = 0;
        s_MaxLNitrogen = 0;
        main = Camera.main;
        cameraPosition = main.transform.position;
        cameraRotation = main.transform.rotation;
        InvokeRepeating("UseNitrogen", 0, 5);
        InvokeRepeating("recurringCost", 0, 5);
    }



    private void onEnable()
    {
        playerControls.Enable();
    }
    private void onDisable()
    {
        playerControls.Disable();
    }


    // Update is called once per frame
    
    
    void Update()
    {
        #region camera movement
        // //Movement
        // if(Input.GetKey(KeyCode.W))
        // {
        //     cameraPosition.z += cameraSpeed / 10;          
        //     cameraPosition.x += cameraSpeed / 10;
        // }          
        // if(Input.GetKey(KeyCode.D))
        // {
        //     cameraPosition.x += cameraSpeed / 10;       
        //     cameraPosition.z -= cameraSpeed / 10;
        // }
        // if(Input.GetKey(KeyCode.S))
        // {
        //     cameraPosition.z -= cameraSpeed / 10;
        //     cameraPosition.x -= cameraSpeed / 10;
        // }
        // if(Input.GetKey(KeyCode.A))
        // {
        //     cameraPosition.x -= cameraSpeed / 10;
        //     cameraPosition.z += cameraSpeed / 10;
        // }

        // //Rotate
        // if(Input.GetKey(KeyCode.E))
        // {
        //     Debug.Log("E");
        //     cameraRotation.y -= cameraSpeed / 10;
        // }
        // if(Input.GetKey(KeyCode.Q))
        // {
        //     Debug.Log("Q");
        //     cameraRotation.y -= cameraSpeed / 10;
        // }

        // //Zoom
        // if(Input.mouseScrollDelta.y > 0)
        //     cameraPosition.y -=1;
        // if(Input.mouseScrollDelta.y < 0)
        //     cameraPosition.y +=1;

        // main.transform.position = cameraPosition;
        #endregion

    }

    #region Resource Management
    public void BuyNitrogen(int Amount)
    {
        int buyableAmount = s_MaxLNitrogen - s_LiquidNitrogen;
        if (Amount > buyableAmount) Amount = buyableAmount;
        int price = Amount * 2;

        //not enough Money
        if (price > s_Money) return;

        s_LiquidNitrogen += Amount;
        s_Money -= price;
    }

    public void BuyNitroMax ()
    {
        int Amount = s_MaxLNitrogen - s_LiquidNitrogen;
        int price = Amount * 2;

        //not enough Money
        if (price > s_Money) return;

        s_LiquidNitrogen += Amount;
        s_Money -= price;
    }

    public void UseNitrogen ()
    {
        if (s_LiquidNitrogen < s_MaxLNitrogen) s_LiquidNitrogen += NitrogenGeneration;
        
        if (s_LiquidNitrogen <= 0)
        {
            return;
        }
        s_LiquidNitrogen -= NitrogenUsage;
    }
    private void recurringCost ()
    {
            s_Money -= s_operatingCost;

            int changeInt = s_lastCPayment - s_operatingCost;
            string change = changeInt.ToString();

            if (changeInt <0)
            {
                moneyChange.text = "" + change.ToString();
            }
            else if (changeInt > 0)
            {
                moneyChange.text = "+ " + changeInt.ToString();
            }
            else 
            {
                moneyChange.text = "+/- 0";
            }
    }
    #endregion

    private void FixedUpdate()
    {
        
    }
}
