using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Text text;
    public Color mouseOverColor;
    public Color standardColor;

    public void ChangeColorEnter()
    {
        text.color = mouseOverColor;

        
    }

    public void ChangeColorExit()
    {
        text.color = standardColor;

    }

}
