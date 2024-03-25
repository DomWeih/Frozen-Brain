using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    #region Organs
        public bool heart = true;
        public bool lung = true;
        public bool kidneyRight = true;
        public bool kidneyLeft = true;
        public bool  brain = true;
        public bool skin = true;
        public bool eyeRight = true;
        public bool eyeLeft = true;
        
    #endregion

    public Button disposeButton;
    public int disposePrice = 10000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.s_Money < disposePrice)
        {
            disposeButton.interactable = false;
        }
        else if (PlayerManager.s_Money >= disposePrice)
        {
            disposeButton.interactable = true;
        }
    }
    public void SellOrgans(String organName)
    {
        int organPrice = 0;
        switch(organName) {
            case var _ when organName.Equals("heart"):
                organPrice = UnityEngine.Random.Range(75000, 150000);
                PlayerManager.s_Money += organPrice;
                heart = false;
            break;
            case var _ when organName.Equals("lung"):
                organPrice = UnityEngine.Random.Range(100000, 200000);
                PlayerManager.s_Money += organPrice;
                lung = false;
            break;            
            case var _ when organName.Equals("kidney right"):
                organPrice = UnityEngine.Random.Range(100000, 200000);
                PlayerManager.s_Money += organPrice;
                kidneyRight = false;
            break;            
            case var _ when organName.Equals("kidney left"):
            organPrice = UnityEngine.Random.Range(100000, 200000);
                PlayerManager.s_Money += organPrice;
                kidneyLeft = false;
            break;            
            case var _ when organName.Equals("brain"):
                brain = false;
            break;            
            case var _ when organName.Equals("skin"):
                skin = false;
            break;            
            case var _ when organName.Equals("eyeRight"):
                organPrice = UnityEngine.Random.Range(10000, 30000);
                PlayerManager.s_Money += organPrice;
                eyeRight = false;
            break;
            case var _ when organName.Equals("eyeLeft"):
                organPrice = UnityEngine.Random.Range(10000, 30000);
                PlayerManager.s_Money += organPrice;
                eyeLeft = false;
            break;
        }
    }
    public void SellBody ()
    {
        PlayerManager.s_Money -= disposePrice;
        PlayerManager.s_CurrentBodies--;
        Destroy(this.gameObject);
    }
}
