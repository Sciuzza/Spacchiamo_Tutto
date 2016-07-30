using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


namespace Spacchiamo
{
    public class Ui_Manager : MonoBehaviour
    {
        #region Hud References
        Text fear, turnCount;
        Slider fearBar;
        UILifePanelScript lifePanelScript;
        Button mainMenuReturn, resetLevel;
        GameObject gameOver, popupT;
        List<GameObject> trainerStories;
        #endregion

        #region Ability Ui References
        UIAbilitiesAndWeaponsCanvasScript abilitiesAndWeaponsCanvasScript;      //RIFERIMENTO AL CANVAS SCRIPT DELL'INTERFACCIA DELLE ARMI; DA ASSEGNARSI 
        #endregion

        #region Main Menu References
        Button newGame, options, comInstr, credits, exit;
        #endregion

        #region Option References
        Button accept;
        Dropdown difficulty;
        Toggle audioOn;
        Slider audioVolume;
        GameObject optionsVisual;
        GameObject warningVisual;
        #endregion

        #region Keybindings and Instructions
        GameObject phase1Instr, phase2Instr;
        #endregion

        #region Credits
        GameObject creditsNames;
        #endregion

        #region Story
        List<GameObject> storyParts;
        #endregion

        [HideInInspector]
        public static Ui_Manager instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

        }


        #region Switch Into Ability Section Methods
        //METODO, DA COMPILARSI CON I DOVUTI VALORI, PER PASSARE I SUDDETTI ALL'INTERFACCIA DI SELEZIONE ARMI ED ABILITA'
        public void SettingAbilitiesAndWeaponsUserInterface()
        {

            AbilitiesAndWeaponsInitialization initializationParameters = new AbilitiesAndWeaponsInitialization();

            //NON INIZIO GIOCO --> COMPLETARE LE ASSEGNAZIONI; SOLO COMPLETARLE
            if (Scene_Manager.instance.nextSceneIndex != 0)
            {

                initializationParameters.passiveAbility1Level = (ushort)Game_Controller.instance.playerStoredSettings.passiveStorage.regeneration.level;
                initializationParameters.passiveAbility2Level = (ushort)Game_Controller.instance.playerStoredSettings.passiveStorage.fighting.level;
                initializationParameters.passiveAbility3Level = (ushort)Game_Controller.instance.playerStoredSettings.passiveStorage.traveler.level;
                initializationParameters.passiveAbility4Level = (ushort)Game_Controller.instance.playerStoredSettings.passiveStorage.survivor.level;
                //			initializationParameters.passiveAbility5Level = (ushort) ;

                initializationParameters.primaryAbility1Level = (ushort)Game_Controller.instance.playerStoredSettings.activeStorage[0].level;
                initializationParameters.primaryAbility2Level = (ushort)Game_Controller.instance.playerStoredSettings.activeStorage[6].level;
                initializationParameters.primaryAbility3Level = (ushort)Game_Controller.instance.playerStoredSettings.activeStorage[9].level;
                //			initializationParameters.primaryAbility4Level = (ushort) ;
                //			initializationParameters.primaryAbility5Level = (ushort) ;

                initializationParameters.secondaryAbility1Level = (ushort)Game_Controller.instance.playerStoredSettings.activeStorage[3].level;
                initializationParameters.secondaryAbility2Level = (ushort)Game_Controller.instance.playerStoredSettings.activeStorage[12].level;
                initializationParameters.secondaryAbility3Level = (ushort)Game_Controller.instance.playerStoredSettings.activeStorage[15].level;
                //			initializationParameters.secondaryAbility4Level = (ushort) ;
                //			initializationParameters.secondaryAbility5Level = (ushort) ;

                initializationParameters.levelUpPoints = (ushort)Game_Controller.instance.playerStoredSettings.unspentAbilityPoints;

                initializationParameters.passiveAbilityUICharacteristic = (UIIMAGE)(passiveAbilityIndex(TakingCurrentPassiveAbility()) + 8);
                initializationParameters.primaryAbilityUICharacteristic = (UIIMAGE)(primaryAbilityIndex(TakingCurrentPrimaryAbility()) + 8);
                initializationParameters.secondaryAbilityUICharacteristic = (UIIMAGE)(secondAbilityIndex(TakingCurrentSecondAbility()) + 8);

                initializationParameters.primaryWeaponUICharacteristic = (UIIMAGE)(primaryWeaponIndex(TakingCurrentPrimaryWeapon()) + 13);
                initializationParameters.secondaryWeaponUICharacteristic = (UIIMAGE)(secondWeaponIndex(TakingCurrentSecondWeapon()) + 13);

            }   //FINE NON INIZIO GIOCO

            abilitiesAndWeaponsCanvasScript.InitializeAbilitiesAndWeaponsCanvas(initializationParameters);

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

        private int passiveAbilityIndex(pOriginalName passiveName)
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

        private int primaryWeaponIndex(weaponType weapon)
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
        #endregion



        #region Switch Out Ability Section Methods
        //METODO, DA COMPLARSI CON I DOVUTI VALORI, PER RICEVERE I SUDDETTI DALL'INTERFACCIA DI SELEZIONE ARMI ED ABILITA'
        public void ReceivingAbilitiesAndWeaponsUserInterface(AbilitiesAndWeaponsInitialization newAbilitiesAndWeaponsParameters)
        {

            PassiveSelectionTransfer((int)newAbilitiesAndWeaponsParameters.passiveAbilityUICharacteristic - 8);
            ActivePrimaryTransfer((int)newAbilitiesAndWeaponsParameters.primaryAbilityUICharacteristic - 8, (int)newAbilitiesAndWeaponsParameters.primaryWeaponUICharacteristic - 13);
            ActiveSecondTransfer((int)newAbilitiesAndWeaponsParameters.secondaryAbilityUICharacteristic - 8, (int)newAbilitiesAndWeaponsParameters.secondaryWeaponUICharacteristic - 13);


            Game_Controller.instance.SetRegeneration(newAbilitiesAndWeaponsParameters.passiveAbility1Level);
			Game_Controller.instance.SetFighting(newAbilitiesAndWeaponsParameters.passiveAbility2Level);
			Game_Controller.instance.SetTraveler(newAbilitiesAndWeaponsParameters.passiveAbility3Level);
			Game_Controller.instance.SetSurvivor(newAbilitiesAndWeaponsParameters.passiveAbility4Level);
            //		(int) = newAbilitiesAndWeaponsParameters.passiveAbility5Level;

            Game_Controller.instance.SetPrimSecondPhase1(newAbilitiesAndWeaponsParameters.primaryAbility1Level, originalName.Impeto);
            Game_Controller.instance.SetPrimSecondPhase1(newAbilitiesAndWeaponsParameters.secondaryAbility1Level, originalName.RespiroDelVento);



            Game_Controller.instance.playerStoredSettings.unspentAbilityPoints = newAbilitiesAndWeaponsParameters.levelUpPoints;



        }

        private void PassiveSelectionTransfer(int i)
        {
            switch (i)
            {
                case 1:
                    Game_Controller.instance.playerStoredSettings.passiveStorage.regeneration.active = true;
                    Game_Controller.instance.playerStoredSettings.passiveStorage.fighting.active = false;
                    Game_Controller.instance.playerStoredSettings.passiveStorage.traveler.active = false;
                    Game_Controller.instance.playerStoredSettings.passiveStorage.survivor.active = false;
                    break;
                case 2:
                    Game_Controller.instance.playerStoredSettings.passiveStorage.regeneration.active = false;
                    Game_Controller.instance.playerStoredSettings.passiveStorage.fighting.active = true;
                    Game_Controller.instance.playerStoredSettings.passiveStorage.traveler.active = false;
                    Game_Controller.instance.playerStoredSettings.passiveStorage.survivor.active = false;
                    break;
                case 3:
                    Game_Controller.instance.playerStoredSettings.passiveStorage.regeneration.active = false;
                    Game_Controller.instance.playerStoredSettings.passiveStorage.fighting.active = false;
                    Game_Controller.instance.playerStoredSettings.passiveStorage.traveler.active = true;
                    Game_Controller.instance.playerStoredSettings.passiveStorage.survivor.active = false;
                    break;
                case 4:
                    Game_Controller.instance.playerStoredSettings.passiveStorage.regeneration.active = false;
                    Game_Controller.instance.playerStoredSettings.passiveStorage.fighting.active = false;
                    Game_Controller.instance.playerStoredSettings.passiveStorage.traveler.active = false;
                    Game_Controller.instance.playerStoredSettings.passiveStorage.survivor.active = true;
                    break;
                default:
                    Debug.LogError("NON RIESCO AD ASSIMILARE L'ABILITA' PASSIVA DALL'INTERFACCIA");
                    break;
            }
        }

        private void ActivePrimaryTransfer(int i, int j)
        {


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


        }

        private void ActiveSecondTransfer(int i, int j)
        {

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



        }
        #endregion



        #region Reference Methods
        public void GivingCanvasAbiWeapScriptRef(UIAbilitiesAndWeaponsCanvasScript abiWeaRef)
        {
            abilitiesAndWeaponsCanvasScript = abiWeaRef;
        }

        public void TakingReferences(Text fearRef, Text turnCountRef, Slider fearBarRef, UILifePanelScript uiLifeRef)
        {

            fear = fearRef;
            turnCount = turnCountRef;
            fearBar = fearBarRef;
            lifePanelScript = uiLifeRef;

        }
        #endregion

        #region Hud Update
        public void UiInitialization()
        {
            lifePanelScript.UISetLife();
            SettingTurnValue(0);
            fear.text = string.Format("{00}", 0);
            fearBar.value = 0f;

            gameOver = GameObject.FindGameObjectWithTag("GameOver");
            mainMenuReturn = GameObject.FindGameObjectWithTag("GameOver").transform.FindChild("Esci al menu").GetComponent<Button>();
            resetLevel = GameObject.FindGameObjectWithTag("GameOver").transform.FindChild("Ricomincia il livello").GetComponent<Button>();

            gameOver.SetActive(false);

            popupT = GameObject.FindGameObjectWithTag("PopupT");

            trainerStories = new List<GameObject>();

            trainerStories.Add(GameObject.FindGameObjectWithTag("PopupT").transform.FindChild("Story1").gameObject);
            trainerStories.Add(GameObject.FindGameObjectWithTag("PopupT").transform.FindChild("Story2").gameObject);
            trainerStories.Add(GameObject.FindGameObjectWithTag("PopupT").transform.FindChild("Story3").gameObject);
            trainerStories.Add(GameObject.FindGameObjectWithTag("PopupT").transform.FindChild("Story4").gameObject);
            trainerStories.Add(GameObject.FindGameObjectWithTag("PopupT").transform.FindChild("Story5").gameObject);
            trainerStories.Add(GameObject.FindGameObjectWithTag("PopupT").transform.FindChild("Story6").gameObject);
            trainerStories.Add(GameObject.FindGameObjectWithTag("PopupT").transform.FindChild("Story7").gameObject);
            trainerStories.Add(GameObject.FindGameObjectWithTag("PopupT").transform.FindChild("Story8").gameObject);

            popupT.SetActive(false);

        }


        public void SettingFearValue(int playerFear)
        {
            fear.text = string.Format("{00}", playerFear);
            SettingFearBar(playerFear);
        }

        private void SettingFearBar(int playerFear)
        {
            fearBar.value = playerFear;
        }


        public void SettingTurnValue(int turnValue)
        {
            turnCount.text = string.Format("{000}", turnValue);
        }

        public void SettingLife(int playerLife)
        {
            lifePanelScript.UISetLife(playerLife);
        }
        #endregion


        #region Ui Main Menu Methods

        #region MainMenuOnly
        public void InitializingMainMenu()
        {
            newGame = GameObject.Find("Nuova Partita").GetComponent<Button>();
            options = GameObject.Find("Opzioni").GetComponent<Button>();
            comInstr = GameObject.Find("Comandi ed istruzioni").GetComponent<Button>();
            credits = GameObject.Find("Crediti").GetComponent<Button>();
            exit = GameObject.Find("Esci").GetComponent<Button>();

            newGame.onClick.AddListener(() => Scene_Manager.instance.LoadSpecificScene(4));
            options.onClick.AddListener(() => Scene_Manager.instance.LoadSpecificScene(1));
            comInstr.onClick.AddListener(() => Scene_Manager.instance.LoadSpecificScene(2));
            credits.onClick.AddListener(() => Scene_Manager.instance.LoadSpecificScene(3));
            exit.onClick.AddListener(() => Scene_Manager.instance.LoadExit());
        }
        #endregion

        #region Options
        public void InitializingOptions(int difficultySet, bool audioIsOn, float audioVolumeSet)
        {
            accept = GameObject.Find("Accetta").GetComponent<Button>();
            difficulty = GameObject.Find("Difficolta").GetComponent<Dropdown>();
            audioOn = GameObject.Find("Audio").GetComponent<Toggle>();
            audioVolume = GameObject.Find("Volume").GetComponent<Slider>();
            optionsVisual = GameObject.Find("Opzioni");
            warningVisual = GameObject.Find("Warning");

            warningVisual.SetActive(false);


            accept.onClick.AddListener(() => Game_Controller.instance.OptionSettingsSaving());
            accept.onClick.AddListener(() => Scene_Manager.instance.LoadSpecificScene(0));
            difficulty.value = difficultySet;
            audioOn.isOn = audioIsOn;
            audioVolume.value = audioVolumeSet;

        }

        public void TakingOptionSettings(out int difficultySet, out bool audioIsOn, out float audioVolumeSet)
        {
            difficultySet = difficulty.value;
            audioIsOn = audioOn.isOn;
            audioVolumeSet = audioVolume.value;
        } 

        public bool AreOptionsChanged(int difficultySet, bool audioIsOn, float audioVolumeSet)
        {
            if (difficultySet != difficulty.value || audioIsOn != audioOn.isOn || audioVolumeSet != audioVolume.value)
                return true;
            else
                return false;
        }

        public void PoppingOutWarning()
        {
            optionsVisual.SetActive(false);
            warningVisual.SetActive(true);
        }

        public void PoppingInDefault()
        {
            optionsVisual.SetActive(true);
            warningVisual.SetActive(false);
        }

        public bool IsWarningActiveInHierarchy()
        {
            return warningVisual.activeInHierarchy;
        }
        #endregion

        #region Keybindings and Instructions
        public void InitiliazingComInstr()
        {
            phase1Instr = GameObject.Find("Phase1");
            phase2Instr = GameObject.Find("Phase2");

            phase2Instr.SetActive(false);
        }

        public bool IsPhase2Activated()
        {
            return phase2Instr.activeInHierarchy;
        }

        public void ActivatePhase2()
        {
            phase1Instr.SetActive(false);
            phase2Instr.SetActive(true);
        }

        #endregion

        #region Story
        public void InitializeStory()
        {
            storyParts = new List<GameObject>();

            storyParts.Add(GameObject.Find("Story1"));
            storyParts.Add(GameObject.Find("Story2"));
            storyParts.Add(GameObject.Find("Story3"));
            storyParts.Add(GameObject.Find("Story4"));
            storyParts.Add(GameObject.Find("Story5"));
            storyParts.Add(GameObject.Find("Story6"));
            storyParts.Add(GameObject.Find("Story7"));
            storyParts.Add(GameObject.Find("Story8"));

            for (int i = 1; i < storyParts.Count; i++)
                storyParts[i].SetActive(false);
            
        }

        public void SetStoryPhase(int storyCounter)
        {
            storyParts[storyCounter - 1].SetActive(false);
            storyParts[storyCounter].SetActive(true);
        }
        #endregion

        #endregion

    }
}