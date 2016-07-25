using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Spacchiamo
{
    public class Ui_Manager : MonoBehaviour
    {
        Text fear, turnCount;
		Slider fearBar;
		UILifePanelScript lifePanelScript;


        [HideInInspector]
        public static Ui_Manager instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

        }
        

		public void SettingFearValue(int playerFear)
        {
			fear.text = string.Format ("{00}", playerFear);
			SettingFearBar (playerFear);
        }

		private void SettingFearBar(int playerFear)
        {
			fearBar.value = playerFear;
        }


		public void SettingTurnValue(int turnValue)
        {
			turnCount.text = string.Format ("{000}", turnValue);
        }

		public void SettingLife (int playerLife)
		{
			lifePanelScript.UISetLife (playerLife);
		}


        public void TakingReferences(Text fearRef, Text turnCountRef, Slider fearBarRef, UILifePanelScript uiLifeRef) {

            fear = fearRef;
            turnCount = turnCountRef;
            fearBar = fearBarRef;
            lifePanelScript = uiLifeRef;

        }

        public void UiInitialization()
        {
            lifePanelScript.UISetLife();
            SettingTurnValue(0);
            fear.text = string.Format("{00}", 0);
            fearBar.value = 0f;
        }
    }
}