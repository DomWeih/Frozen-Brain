using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildMenu : MonoBehaviour
{

    [SerializeField] GameObject LN2Tab;
    [SerializeField] GameObject BodyTab;
    [SerializeField] GameObject LN2GenTab;
    [SerializeField] GameObject LN2Canvas;
    [SerializeField] GameObject BodyCanvas;
    [SerializeField] GameObject LN2GenCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenTab(GameObject openTab)
    {
        openTab.SetActive(true);
    }
    public void CloseTab()
    {
        LN2GenCanvas.SetActive(false);
        LN2Canvas.SetActive(false);
        BodyCanvas.SetActive(false);
        
    }
}
