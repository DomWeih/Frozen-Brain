using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuyMax : MonoBehaviour
{
    private TMP_Text text;
    private int maxPrice;
    
    // Start is called before the first frame update
    void Start()
    {
           text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
       maxPrice = (PlayerManager.s_MaxLNitrogen - PlayerManager.s_LiquidNitrogen) * 2;
       text.text = maxPrice + " €";
    }
}
