using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonColor : MonoBehaviour
{
    
    private Color activeColor = new Color(0.3882f, 0.3882f, 0.3882f, 0.7f);
    private Color inactiveColor = new Color(0.3882f, 0.3882f, 0.3882f, 0f);
    private Image buttonImage;
    // Start is called before the first frame update

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.color = inactiveColor;
    }

    public void OnPointerHover()
    {
        buttonImage.color = activeColor;
        
    }

    public void OnPointerExit()
    {
        buttonImage.color = inactiveColor;
    }
}
