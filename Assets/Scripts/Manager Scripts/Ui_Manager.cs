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
		public void SettingAbilitiesAndWeaponsUserInterface () {

			AbilitiesAndWeaponsInitialization initializationParameters = new AbilitiesAndWeaponsInitialization ();

			//NON INIZIO GIOCO --> COMPLETARE LE ASSEGNAZIONI; SOLO COMPLETARLE
			if (Scene_Manager.instance.nextSceneIndex != 0) {

				initializationParameters.passiveAbility1Level = (ushort) Game_Controller.instance.playerStoredSettings.passiveStorage.regeneration.level;
	//			initializationParameters.passiveAbility2Level = (ushort) ;
	//			initializationParameters.passiveAbility3Level = (ushort) ;
	//			initializationParameters.passiveAbility4Level = (ushort) ;
	//			initializationParameters.passiveAbility5Level = (ushort) ;

				initializationParameters.primaryAbility1Level = (ushort) Game_Controller.instance.playerStoredSettings.activeStorage[0].level;
	//			initializationParameters.primaryAbility2Level = (ushort) ;
	//			initializationParameters.primaryAbility3Level = (ushort) ;
	//			initializationParameters.primaryAbility4Level = (ushort) ;
	//			initializationParameters.primaryAbility5Level = (ushort) ;

				initializationParameters.secondaryAbility1Level = (ushort) Game_Controller.instance.playerStoredSettings.activeStorage[3].level; 
	//			initializationParameters.secondaryAbility2Level = (ushort) ;
	//			initializationParameters.secondaryAbility3Level = (ushort) ;
	//			initializationParameters.secondaryAbility4Level = (ushort) ;
	//			initializationParameters.secondaryAbility5Level = (ushort) ;

				initializationParameters.levelUpPoints = (ushort) Game_Controller.instance.playerStoredSettings.unspentAbilityPoints;

				initializationParameters.passiveAbilityUICharacteristic = (UIIMAGE) (passiveAbilityIndex(TakingCurrentPassiveAbility()) + 8);
				initializationParameters.primaryAbilityUICharacteristic = (UIIMAGE) (primaryAbilityIndex(TakingCurrentPrimaryAbility()) + 8);
				initializationParameters.secondaryAbilityUICharacteristic = (UIIMAGE) (secondAbilityIndex(TakingCurrentSecondAbility()) + 8);

				initializationParameters.primaryWeaponUICharacteristic = (UIIMAGE) (primaryWeaponIndex(TakingCurrentPrimaryWeapon()) + 13);
				initializationParameters.secondaryWeaponUICharacteristic = (UIIMAGE) (secondWeaponIndex(TakingCurrentSecondWeapon()) + 13);

			}	//FINE NON INIZIO GIOCO

			abilitiesAndWeaponsCanvasScript.InitializeAbilitiesAndWeaponsCanvas (initializationParameters);

		}

        private originalName TakingCurrentPrimaryAbility()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Game_Controller.instance.playerStoredSettings.activeStorage[i].active)
                    return Game_Controller.instance.playerStoredSettings.activeStorage[i].oname;
            }

            return originalName.NotFound;
        }

        private int primaryAbilityIndex(originalName activeName)
        {
            if (activeName == originalName.Impeto)
                return 1;
            else
                return 0;
        }


        private originalName TakingCurrentSecondAbility()
        {
            for (int i = 3; i < 6; i++)
            {
                if (Game_Controller.instance.playerStoredSettings.activeStorage[i].active)
                    return Game_Controller.instance.playerStoredSettings.activeStorage[i].oname;
            }

            return originalName.NotFound;
        }

        private int secondAbilityIndex(originalName activeName)
        {
            if (activeName == originalName.RespiroDelVento)
                return 1;
            else
                return 0;
        }


        private pOriginalName TakingCurrentPassiveAbility()
        {
            if (Game_Controller.instance.playerStoredSettings.passiveStorage.regeneration.active)
                return pOriginalName.Rigenerazione;
            else
                return pOriginalName.NotFound;
        }

        private int passiveAbilityIndex (pOriginalName passiveName)
        {
            if (passiveName == pOriginalName.Rigenerazione)
                return 1;
            else
                return 0;
        }


        private weaponType TakingCurrentPrimaryWeapon()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Game_Controller.instance.playerStoredSettings.activeStorage[i].active)
                    return Game_Controller.instance.playerStoredSettings.activeStorage[i].weapon;
            }

            return weaponType.NotFound;
        }

        private int primaryWeaponIndex (weaponType weapon)
        {
            if (weapon == weaponType.ArmaBianca)
                return 1;
            else if (weapon == weaponType.Catalizzatore)
                return 2;
            else if (weapon == weaponType.ArmaRanged)
                return 3;
            else
                return 0;
        }


        private weaponType TakingCurrentSecondWeapon()
        {
            for (int i = 3; i < 6; i++)
            {
                if (Game_Controller.instance.playerStoredSettings.activeStorage[i].active)
                    return Game_Controller.instance.playerStoredSettings.activeStorage[i].weapon;
            }

            return weaponType.NotFound;
        }

        private int secondWeaponIndex(weaponType weapon)
        {
            if (weapon == weaponType.ArmaBianca)
                return 1;
            else if (weapon == weaponType.Catalizzatore)
                return 2;
            else if (weapon == weaponType.ArmaRanged)
                return 3;
            else
                return 0;
        }

        public void GivingCanvasAbiWeapScriptRef(UIAbilitiesAndWeaponsCanvasScript abiWeaRef)
        {
            abilitiesAndWeaponsCanvasScript = abiWeaRef;
        }

        //METODO, DA COMPLARSI CON I DOVUTI VALORI, PER RICEVERE I SUDDETTI DALL'INTERFACCIA DI SELEZIONE ARMI ED ABILITA'
        	public void ReceivingAbilitiesAndWeaponsUserInterface (AbilitiesAndWeaponsInitialization newAbilitiesAndWeaponsParameters) {

            PassiveSelectionTransfer((int)newAbilitiesAndWeaponsParameters.passiveAbilityUICharacteristic - 8);
            ActivePrimaryTransfer((int)newAbilitiesAndWeaponsParameters.primaryAbilityUICharacteristic - 8, (int)newAbilitiesAndWeaponsParameters.primaryWeaponUICharacteristic - 13);
            ActiveSecondTransfer((int)newAbilitiesAndWeaponsParameters.secondaryAbilityUICharacteristic - 8, (int)newAbilitiesAndWeaponsParameters.secondaryWeaponUICharacteristic - 13);


            Game_Controller.instance.SetRegeneration(newAbilitiesAndWeaponsParameters.passiveAbility1Level);
        //		(int) = newAbilitiesAndWeaponsParameters.passiveAbility2Level;
        //		(int) = newAbilitiesAndWeaponsParameters.passiveAbility3Level;
        //		(int) = newAbilitiesAndWeaponsParameters.passiveAbility4Level;
        //		(int) = newAbilitiesAndWeaponsParameters.passiveAbility5Level;

        		Game_Controller.instance.SetImpeto(newAbilitiesAndWeaponsParameters.primaryAbility1Level);
        //		(int) = newAbilitiesAndWeaponsParameters.primaryAbility2Level;
        //		(int) = newAbilitiesAndWeaponsParameters.primaryAbility3Level;
        //		(int) = newAbilitiesAndWeaponsParameters.primaryAbility4Level;
        //		(int) = newAbilitiesAndWeaponsParameters.primaryAbility5Level;

        		Game_Controller.instance.SetRespiroDelVento(newAbilitiesAndWeaponsParameters.secondaryAbility1Level);
        //		(int) = newAbilitiesAndWeaponsParameters.secondaryAbility2Level;
        //		(int) = newAbilitiesAndWeaponsParameters.secondaryAbility3Level;
        //		(int) = newAbilitiesAndWeaponsParameters.secondaryAbility4Level;
        //		(int) = newAbilitiesAndWeaponsParameters.secondaryAbility5Level;

        		Game_Controller.instance.playerStoredSettings.unspentAbilityPoints = newAbilitiesAndWeaponsParameters.levelUpPoints;

        		 

        	}

        private void PassiveSelectionTransfer(int i)
        {
            switch (i)
            {
                case 1:
                    Game_Controller.instance.playerStoredSettings.passiveStorage.regeneration.active = true;
                    //Altre abilità che non sono la 1: mettere a false
                    break;
                case 2:
                    //Game_Controller.instance.playerStoredSettings.passiveStorage.regeneration.active = false;
                    //Altre abilità che non sono la 2: mettere a false
                    break;
                case 3:
                    //Game_Controller.instance.playerStoredSettings.passiveStorage.regeneration.active = false;
                    //Altre abilità che non sono la 3: mettere a false
                    break;
                default:
                    Debug.LogError("NON RIESCO AD ASSIMILARE L'ABILITA' PASSIVA DALL'INTERFACCIA");
                    break;
            }
        }

        private void ActivePrimaryTransfer(int i, int j)
        {
            switch (i)
            {
                case 1:

                    switch (j)
                    {
                        case 1:
                            Game_Controller.instance.SetActivePrim(originalName.Impeto, weaponType.ArmaBianca);
                            break;
                        case 2:
                            Game_Controller.instance.SetActivePrim(originalName.Impeto, weaponType.Catalizzatore);
                            break;
                        case 3:
                            Game_Controller.instance.SetActivePrim(originalName.Impeto, weaponType.ArmaRanged);
                            break;
                        default:
                            Debug.LogError("NON RIESCO AD ASSIMILARE L'ARMA' PRIMARIA DALL'INTERFACCIA");
                            break;
                    }

                    break;
                case 2:

                    switch (j)
                    {
                        case 1:
                          
                            break;
                        case 2:
                         
                            break;
                        case 3:
                          
                            break;
                        default:
                            Debug.LogError("NON RIESCO AD ASSIMILARE L'ARMA' PRIMARIA DALL'INTERFACCIA");
                            break;
                    }

                    break;
                case 3:

                    switch (j)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        default:
                            Debug.LogError("NON RIESCO AD ASSIMILARE L'ARMA' PRIMARIA DALL'INTERFACCIA");
                            break;
                    }

                    break;
                default:
                    Debug.LogError("NON RIESCO AD ASSIMILARE L'ABILITA' PRIMARIA DALL'INTERFACCIA");
                    break;
            }
        }

        private void ActiveSecondTransfer(int i, int j)
        {
            switch (i)
            {
                case 1:

                    switch (j)
                    {
                        case 1:
                            Game_Controller.instance.SetActiveSec(originalName.RespiroDelVento, weaponType.ArmaBianca);
                            break;
                        case 2:
                            Game_Controller.instance.SetActiveSec(originalName.RespiroDelVento, weaponType.Catalizzatore);
                            break;
                        case 3:
                            Game_Controller.instance.SetActiveSec(originalName.RespiroDelVento, weaponType.ArmaRanged);
                            break;
                        default:
                            Debug.LogError("NON RIESCO AD ASSIMILARE L'ARMA' SECONDARIA DALL'INTERFACCIA");
                            break;
                    }

                    break;
                case 2:

                    switch (j)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        default:
                            Debug.LogError("NON RIESCO AD ASSIMILARE L'ARMA' SECONDARIA DALL'INTERFACCIA");
                            break;
                    }

                    break;
                case 3:

                    switch (j)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        default:
                            Debug.LogError("NON RIESCO AD ASSIMILARE L'ARMA' SECONDARIA DALL'INTERFACCIA");
                            break;
                    }

                    break;
                default:
                    Debug.LogError("NON RIESCO AD ASSIMILARE L'ABILITA' SECONDARIA DALL'INTERFACCIA");
                    break;
            }
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