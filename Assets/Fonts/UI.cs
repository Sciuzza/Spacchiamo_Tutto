using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Text text;
    public Color mouseOverColor;
    public Color standardColor;

    public void ChangeColorRed()
    {
        text.color = Color.red;

        
    }

    public void ChangeColorBlack()
    {
        text.color = Color.black;

    }

}
