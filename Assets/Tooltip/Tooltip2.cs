/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip2 : MonoBehaviour {

    private static Tooltip instance;

    [SerializeField]
    private Camera uiCamera;

    public TMP_Text tooltipText;
    public RectTransform backgroundRectTransform;

    private void Awake() {
        instance = this;
        HideTooltip();
    }

    private void Update() {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        localPoint += new Vector2 (25 , 100); 
        transform.localPosition = localPoint;
        
    }

    public void ShowTooltip(string tooltipString) {
        gameObject.SetActive(true);

        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2, tooltipText.preferredHeight + textPaddingSize * 2f);
        tooltipText.rectTransform.sizeDelta = backgroundSize;
        backgroundSize = new Vector2(tooltipText.preferredWidth / 5, tooltipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
        
        Update();
    }

    public void HideTooltip() {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string tooltipString) {
        instance.ShowTooltip(tooltipString);
    }

    public static void HideTooltip_Static() {
        instance.HideTooltip();
    }

}
*/