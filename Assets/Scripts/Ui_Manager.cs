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


			fear = GameObject.Find("Fear Counter").GetComponent<Text>();
			fearBar = GameObject.Find("Fear Bar").GetComponent<Slider>();

			turnCount = GameObject.Find("Turn counter").GetComponent<Text>();

			lifePanelScript = GameObject.Find ("Life Panel").GetComponent<UILifePanelScript>();

        }

		//AGGIUNTA DI MARCO
		void Start ()
		{
			lifePanelScript.UISetLife ();
			SettingTurnValue (0);
			fear.text = string.Format ("{00}", 0);
			fearBar.value = 0f;
		}
		//FINE AGGIUNTA DI MARCO


		public void SettingFearValue(int playerFear)
        {
			fear.text = string.Format ("{00}", playerFear);
			SettingFearBar (playerFear);
        }

		private void SettingFearBar(int playerFear)
        {
			fearBar.value = 1f/playerFear;
        }


		public void SettingTurnValue(int turnValue)
        {
			turnCount.text = string.Format ("{000}", turnValue);
        }

		public void SettingLife (int playerLife)
		{
			lifePanelScript.UISetLife (playerLife);
		}
    }
}