using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Spacchiamo
{
    public class Ui_Manager : MonoBehaviour
    {
        Text fear, turnCount;
        Image fearBar;
        int turnCounter = 0;


        [HideInInspector]
        public static Ui_Manager instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);


            fear = GameObject.Find("Fear Value").GetComponent<Text>();
            fearBar = GameObject.Find("Fear Bar").GetComponent<Image>();

            turnCount = GameObject.Find("Turn Count").GetComponent<Text>();

        }



        public void SettingFearValue()
        {
            fear.text = "Fear : " + Mathf.RoundToInt(fearBar.fillAmount / 0.010f);
        }

        public void IncreasingFearBar()
        {
            fearBar.fillAmount += 0.010f;
        }

        public void ResettingFearBar()
        {
            fearBar.fillAmount = 0.0f;
        }


        public void IncreasingTurnCount()
        {
            turnCounter++;
            turnCount.text = "Turn Number : " + turnCounter;
        }
    }
}