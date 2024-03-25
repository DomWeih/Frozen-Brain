using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public bool mouseTip = false;
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseTip)
        {
            button.SetActive(true);
        }
        else button.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseTip = true;
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseTip = false;
    }
}
