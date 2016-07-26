using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Spacchiamo
{
    public class Ui_Manager : MonoBehaviour
    {
        Text fear, turnCount;
		Slider fearBar;
		UIAbilitiesAndWeaponsCanvasScript abilitiesAndWeaponsCanvasScript;		//RIFERIMENTO AL CANVAS SCRIPT DELL'INTERFACCIA DELLE ARMI; DA ASSEGNARSI
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


		//METODO, DA COMPILARSI CON I DOVUTI VALORI, PER PASSARE I SUDDETTI ALL'INTERFACCIA DI SELEZIONE ARMI ED ABILITA'
	//	public void SettingAbilitiesAndWeaponsUserInterface () {

	//		AbilitiesAndWeaponsInitialization initializationParameters = new AbilitiesAndWeaponsInitialization ();

			//NON INIZIO GIOCO --> COMPLETARE LE ASSEGNAZIONI; SOLO COMPLETARLE
	//		if () {

	//			initializationParameters.passiveAbility1Level = (ushort) ;
	//			initializationParameters.passiveAbility2Level = (ushort) ;
	//			initializationParameters.passiveAbility3Level = (ushort) ;
	//			initializationParameters.passiveAbility4Level = (ushort) ;
	//			initializationParameters.passiveAbility5Level = (ushort) ;

	//			initializationParameters.primaryAbility1Level = (ushort) ;
	//			initializationParameters.primaryAbility2Level = (ushort) ;
	//			initializationParameters.primaryAbility3Level = (ushort) ;
	//			initializationParameters.primaryAbility4Level = (ushort) ;
	//			initializationParameters.primaryAbility5Level = (ushort) ;

	//			initializationParameters.secondaryAbility1Level = (ushort) ;
	//			initializationParameters.secondaryAbility2Level = (ushort) ;
	//			initializationParameters.secondaryAbility3Level = (ushort) ;
	//			initializationParameters.secondaryAbility4Level = (ushort) ;
	//			initializationParameters.secondaryAbility5Level = (ushort) ;

	//			initializationParameters.levelUpPoints = (ushort) ;

	//			initializationParameters.passiveAbilityUICharacteristic = (UIIMAGE) (/* numero rappresentante l'abilità passiva; scrivere qui "zero" darebbe la "non abilità"; da zero a cinque */ + 8);
	//			initializationParameters.primaryAbilityUICharacteristic = (UIIMAGE) (/* numero rappresentante l'abilità primaria; scrivere qui "zero" darebbe la "non abilità"; da zero a cinque */ + 8);
	//			initializationParameters.secondaryAbilityUICharacteristic = (UIIMAGE) (/* numero rappresentante l'abilità secondaria; scrivere qui "zero" darebbe la "non abilità"; da zero a cinque */ + 8);

	//			initializationParameters.primaryWeaponUICharacteristic = (UIIMAGE) (/* numero rappresentante l'arma primaria; scrivere qui "uno" darebbe la "prima arma"; da uno a tre */ + 13);
	//			initializationParameters.secondaryWeaponUICharacteristic = (UIIMAGE) (/* numero rappresentante l'arma secondaria; scrivere qui "uno" darebbe la "prima arma"; da uno a tre */ + 13);

	//		}	//FINE NON INIZIO GIOCO

	//		abilitiesAndWeaponsCanvasScript.InitializeAbilitiesAndWeaponsCanvas (initializationParameters);

	//	}


		//METODO, DA COMPLARSI CON I DOVUTI VALORI, PER RICEVERE I SUDDETTI DALL'INTERFACCIA DI SELEZIONE ARMI ED ABILITA'
	//	public void ReceivingAbilitiesAndWeaponsUserInterface (AbilitiesAndWeaponsInitialization newAbilitiesAndWeaponsParameters) {

	//		(int) = newAbilitiesAndWeaponsParameters.passiveAbility1Level;
	//		(int) = newAbilitiesAndWeaponsParameters.passiveAbility2Level;
	//		(int) = newAbilitiesAndWeaponsParameters.passiveAbility3Level;
	//		(int) = newAbilitiesAndWeaponsParameters.passiveAbility4Level;
	//		(int) = newAbilitiesAndWeaponsParameters.passiveAbility5Level;

	//		(int) = newAbilitiesAndWeaponsParameters.primaryAbility1Level;
	//		(int) = newAbilitiesAndWeaponsParameters.primaryAbility2Level;
	//		(int) = newAbilitiesAndWeaponsParameters.primaryAbility3Level;
	//		(int) = newAbilitiesAndWeaponsParameters.primaryAbility4Level;
	//		(int) = newAbilitiesAndWeaponsParameters.primaryAbility5Level;

	//		(int) = newAbilitiesAndWeaponsParameters.secondaryAbility1Level;
	//		(int) = newAbilitiesAndWeaponsParameters.secondaryAbility2Level;
	//		(int) = newAbilitiesAndWeaponsParameters.secondaryAbility3Level;
	//		(int) = newAbilitiesAndWeaponsParameters.secondaryAbility4Level;
	//		(int) = newAbilitiesAndWeaponsParameters.secondaryAbility5Level;

	//		(int) = newAbilitiesAndWeaponsParameters.levelUpPoints;

	//		/* numero rappresentante l'abilità passiva; "zero" qui sarebbe la "non abilità"; da zero a cinque */ = ((int)newAbilitiesAndWeaponsParameters.passiveAbilityUICharacteristic - 8);
	//		/* numero rappresentante l'abilità primaria; "zero" qui sarebbe la "non abilità"; da zero a cinque */ = ((int)newAbilitiesAndWeaponsParameters.primaryAbilityUICharacteristic - 8);
	//		/* numero rappresentante l'abilità secondaria; "zero" qui sarebbe la "non abilità"; da zero a cinque */ = ((int)newAbilitiesAndWeaponsParameters.secondaryAbilityUICharacteristic - 8);

	//		/* numero rappresentante l'arma primaria; "uno" qui sarebbe la "prima arma"; da uno a tre */ = ((int)newAbilitiesAndWeaponsParameters.primaryWeaponUICharacteristic - 13);
	//		/* numero rappresentante l'arma secondaria; "uno" qui sarebbe la "prima arma"; da uno a tre */ = ((int)newAbilitiesAndWeaponsParameters.secondaryWeaponUICharacteristic - 13);

	//	}
        

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