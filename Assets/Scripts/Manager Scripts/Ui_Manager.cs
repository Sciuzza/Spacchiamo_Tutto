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
        public Button mainMenuReturn, resetLevel, resume, exitByPause, comInstrByPause;
        public GameObject gameOver, popupT, popupP, instr1, instr2;
        public List<GameObject> trainerStories;
        Text instrText;
        #endregion

        #region Ability Ui References
        public Button impetoUp, respiroUp, esploChoice, combChoice, soprChoice, passiveUp;
        public Button fhArmaBianca, fhCatalizzatore, fhArmaRanged, shArmaBianca, shCatalizzatore, shArmaRanged;
        public Image fhImmagine, shImmagine;
        public Text tooltipTitle, tooltipText, unspentPoints;
        public Button playGame;

        actPlayerAbility abilityMaxLevel;
        passAbilities pAbilityMaxLevel;

        public playerSettings playerStoredSettings, storedSettingsInProgress;
        int ImpetoUpCounter, RespiroUpCounter;
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

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q) && (Scene_Manager.instance.GetCurrentSceneIndex() == 5 || Game_Controller.instance.currentPhase == GAME_PHASE.dialogue))
            {
                ResetImpeto();
                ResetRespiro();
                ResetEverythingElse();
                EnableUpgrades();
            }
        }


        #region AbilityUi Initialization
        public void TakingRefAbilityUi()
        {
            impetoUp = GameObject.FindGameObjectWithTag("ImpetoUp").GetComponent<Button>();
            respiroUp = GameObject.FindGameObjectWithTag("RespiroUp").GetComponent<Button>();
            esploChoice = GameObject.FindGameObjectWithTag("EsploChoice").GetComponent<Button>();
            combChoice = GameObject.FindGameObjectWithTag("CombChoice").GetComponent<Button>();
            soprChoice = GameObject.FindGameObjectWithTag("SoprChoice").GetComponent<Button>();
            passiveUp = GameObject.FindGameObjectWithTag("PassiveUp").GetComponent<Button>();

            fhArmaBianca = GameObject.FindGameObjectWithTag("FH Arma Bianca").GetComponent<Button>();
            fhCatalizzatore = GameObject.FindGameObjectWithTag("FH Catalizzatore").GetComponent<Button>();
            fhArmaRanged = GameObject.FindGameObjectWithTag("FH Arma Ranged").GetComponent<Button>();
           
            shArmaBianca = GameObject.FindGameObjectWithTag("SH Arma Bianca").GetComponent<Button>();
            shCatalizzatore = GameObject.FindGameObjectWithTag("SH Catalizzatore").GetComponent<Button>();      
            shArmaRanged = GameObject.FindGameObjectWithTag("SH Arma Ranged").GetComponent<Button>();     

            fhImmagine = GameObject.FindGameObjectWithTag("FH Immagine Arma").GetComponent<Image>();
            shImmagine = GameObject.FindGameObjectWithTag("SH Immagine Arma").GetComponent<Image>();

            tooltipTitle = GameObject.FindGameObjectWithTag("Tooltip Title").GetComponent<Text>();
            tooltipText = GameObject.FindGameObjectWithTag("Tooltip Text").GetComponent<Text>();

            unspentPoints = GameObject.FindGameObjectWithTag("Unspent Points").GetComponent<Text>();

            playGame = GameObject.FindGameObjectWithTag("Play Game").GetComponent<Button>();


        }

        public void GivingPlayerCurrentSettings(playerSettings storedValues)
        {
             playerStoredSettings = storedValues;
            storedSettingsInProgress = playerStoredSettings;
        }

        public void ApplyingStoredSettingsEffects()
        {

            ImpetoUpCounter = 0;
            RespiroUpCounter = 0;

            #region Passive Icons Initialization
            if (storedSettingsInProgress.passiveStorage.fighting.active)
            {
                combChoice.image.sprite = Resources.Load<Sprite>("UI\\Passive Skills\\Combattente");
            }
            else if (storedSettingsInProgress.passiveStorage.traveler.active)
            {
                esploChoice.image.sprite = Resources.Load<Sprite>("UI\\Passive Skills\\Esploratore");
            }
            else if (storedSettingsInProgress.passiveStorage.survivor.active)
            {
                esploChoice.image.sprite = Resources.Load<Sprite>("UI\\Passive Skills\\Sopravvissuto");
            }
            else
            {
                combChoice.image.sprite = Resources.Load<Sprite>("UI\\Passive Skills\\Combattente");
                storedSettingsInProgress.passiveStorage.fighting.active = true;
            }
            #endregion

            #region Left Weapon Initialization
            if (storedSettingsInProgress.activeStorage.Find(x => x.oname == originalName.Impeto && x.weapon == weaponType.ArmaBianca && x.active == true).level != 0)
            {
                fhImmagine.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Spada");
                FHArmaBiancaSelected();
            }
            else if (storedSettingsInProgress.activeStorage.Find(x => x.oname == originalName.Impeto && x.weapon == weaponType.Catalizzatore && x.active == true).level != 0)
            {
                fhImmagine.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Catalizzatore");
                FHCatalizzatoreSelected();
            }
            else if (storedSettingsInProgress.activeStorage.Find(x => x.oname == originalName.Impeto && x.weapon == weaponType.ArmaRanged && x.active == true).level != 0)
            {
                fhImmagine.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Balestra");
                FHArmaRangedSelected();
            }
            else
            {
                fhImmagine.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Spada");
                FHArmaBiancaSelected();
                SetActiveToTrue(type.Primary, weaponType.ArmaBianca);
            }
            #endregion

            #region Right Weapon Initialization
            if (storedSettingsInProgress.activeStorage.Find(x => x.oname == originalName.RespiroDelVento && x.weapon == weaponType.ArmaBianca && x.active == true).level != 0)
            {
                shImmagine.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Spada");
                SHArmaBiancaSelected();
            }
            else if (storedSettingsInProgress.activeStorage.Find(x => x.oname == originalName.RespiroDelVento && x.weapon == weaponType.Catalizzatore && x.active == true).level != 0)
            {
                shImmagine.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Catalizzatore");
                SHCatalizzatoreSelected();
            }
            else if (storedSettingsInProgress.activeStorage.Find(x => x.oname == originalName.RespiroDelVento && x.weapon == weaponType.ArmaRanged && x.active == true).level != 0)
            {
                shImmagine.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Balestra");
                SHArmaRangedSelected();
            }
            else
            {
                shImmagine.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Catalizzatore");
                SHCatalizzatoreSelected();
                SetActiveToTrue(type.Secondary, weaponType.Catalizzatore);
            }
            #endregion

            unspentPoints.text = "" + storedSettingsInProgress.unspentAbilityPoints;

            CheckUpgradePossibility();
            CheckWhatPassiveSelected();

            
        }

        public void SetButtonFunctionality()
        {
            combChoice.onClick.AddListener(() => CombSelected());
            combChoice.onClick.AddListener(() => EsploDeSelected());
            combChoice.onClick.AddListener(() => SoprDeSelected());
            combChoice.onClick.AddListener(() => CheckWhatPassiveSelected());
            combChoice.onClick.AddListener(() => CheckCombUpgrade());

            esploChoice.onClick.AddListener(() => EsploSelected());
            esploChoice.onClick.AddListener(() => CombDeSelected());
            esploChoice.onClick.AddListener(() => SoprDeSelected());
            esploChoice.onClick.AddListener(() => CheckWhatPassiveSelected());
            esploChoice.onClick.AddListener(() => CheckEsploUpgrade());

            soprChoice.onClick.AddListener(() => SoprSelected());
            soprChoice.onClick.AddListener(() => CombDeSelected());
            soprChoice.onClick.AddListener(() => EsploDeSelected());
            soprChoice.onClick.AddListener(() => CheckWhatPassiveSelected());
            soprChoice.onClick.AddListener(() => CheckSoprUpgrade());

            fhArmaBianca.onClick.AddListener(() => FHArmaBiancaSelected());
            fhArmaBianca.onClick.AddListener(() => FHCatalizzatoreDeSelected());
            fhArmaBianca.onClick.AddListener(() => FHArmaRangedDeSelected());
            fhArmaBianca.onClick.AddListener(() => SetActiveToTrue(type.Primary, weaponType.ArmaBianca));
            fhArmaBianca.onClick.AddListener(() => CheckActiveUpgrade());

            fhCatalizzatore.onClick.AddListener(() => FHCatalizzatoreSelected());
            fhCatalizzatore.onClick.AddListener(() => FHArmaBiancaDeSelected());
            fhCatalizzatore.onClick.AddListener(() => FHArmaRangedDeSelected());
            fhCatalizzatore.onClick.AddListener(() => SetActiveToTrue(type.Primary, weaponType.Catalizzatore));
            fhCatalizzatore.onClick.AddListener(() => CheckActiveUpgrade());

            fhArmaRanged.onClick.AddListener(() => FHArmaRangedSelected());
            fhArmaRanged.onClick.AddListener(() => FHCatalizzatoreDeSelected());
            fhArmaRanged.onClick.AddListener(() => FHArmaBiancaDeSelected());
            fhArmaRanged.onClick.AddListener(() => SetActiveToTrue(type.Primary, weaponType.ArmaRanged));
            fhArmaRanged.onClick.AddListener(() => CheckActiveUpgrade());

            shArmaBianca.onClick.AddListener(() => SHArmaBiancaSelected());
            shArmaBianca.onClick.AddListener(() => SHCatalizzatoreDeSelected());
            shArmaBianca.onClick.AddListener(() => SHArmaRangedDeSelected());
            shArmaBianca.onClick.AddListener(() => SetActiveToTrue(type.Secondary, weaponType.ArmaBianca));
            shArmaBianca.onClick.AddListener(() => CheckActiveUpgrade());

            shCatalizzatore.onClick.AddListener(() => SHCatalizzatoreSelected());
            shCatalizzatore.onClick.AddListener(() => SHArmaBiancaDeSelected());
            shCatalizzatore.onClick.AddListener(() => SHArmaRangedDeSelected());
            shCatalizzatore.onClick.AddListener(() => SetActiveToTrue(type.Secondary, weaponType.Catalizzatore));
            shCatalizzatore.onClick.AddListener(() => CheckActiveUpgrade());

            shArmaRanged.onClick.AddListener(() => SHArmaRangedSelected());
            shArmaRanged.onClick.AddListener(() => SHCatalizzatoreDeSelected());
            shArmaRanged.onClick.AddListener(() => SHArmaBiancaDeSelected());
            shArmaRanged.onClick.AddListener(() => SetActiveToTrue(type.Secondary, weaponType.ArmaRanged));
            shArmaRanged.onClick.AddListener(() => CheckActiveUpgrade());

            impetoUp.onClick.AddListener(() => UpgradeImpeto());
            impetoUp.onClick.AddListener(() => CheckUpgradePossibility());

            respiroUp.onClick.AddListener(() => UpgradeRespiro());
            respiroUp.onClick.AddListener(() => CheckUpgradePossibility());

            playGame.onClick.AddListener(() => passingSettings());
            playGame.onClick.AddListener(() => Scene_Manager.instance.LoadNextLevel());
        }

        #region Active Methods Section
        public void CheckActiveUpgrade()
        {
            abilityMaxLevel = storedSettingsInProgress.activeStorage.Find(x => x.oname == originalName.Impeto && x.level == x.maxLevel);

            if (abilityMaxLevel.level != 0)
                impetoUp.interactable = false;

            abilityMaxLevel = storedSettingsInProgress.activeStorage.Find(x => x.oname == originalName.RespiroDelVento && x.level == x.maxLevel);

            if (abilityMaxLevel.level != 0)
                respiroUp.interactable = false;
        } 

        private void FHArmaBiancaSelected()
        {
            fhArmaBianca.image.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Spada");
            fhImmagine.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Spada");
        }

        private void FHCatalizzatoreSelected()
        {
            fhCatalizzatore.image.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Catalizzatore");
            fhImmagine.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Catalizzatore");
        }

        private void FHArmaRangedSelected()
        {
            fhArmaRanged.image.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Balestra");
            fhImmagine.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Balestra");
        }

        private void SHArmaBiancaSelected()
        {
            shArmaBianca.image.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Spada");
            shImmagine.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Spada");
        }

        private void SHCatalizzatoreSelected()
        {
            shCatalizzatore.image.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Catalizzatore");
            shImmagine.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Catalizzatore");
        }

        private void SHArmaRangedSelected()
        {
            shArmaRanged.image.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Balestra");
            shImmagine.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\Balestra");
        }

        private void FHArmaBiancaDeSelected()
        {
            fhArmaBianca.image.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\SpadaDis");
        }

        private void FHCatalizzatoreDeSelected()
        {
            fhCatalizzatore.image.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\CatalizzatoreDis");
        }

        private void FHArmaRangedDeSelected()
        {
            fhArmaRanged.image.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\BalestraDis");
        }

        private void SHArmaBiancaDeSelected()
        {
            shArmaBianca.image.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\SpadaDis");
        }

        private void SHCatalizzatoreDeSelected()
        {
            shCatalizzatore.image.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\CatalizzatoreDis");
        }

        private void SHArmaRangedDeSelected()
        {
            shArmaRanged.image.sprite = Resources.Load<Sprite>("UI\\Weapon Icons\\BalestraDis");
        }

        private void SetActiveToTrue(type categorySelected, weaponType weaponSelected)
        {
           int abiIndex = storedSettingsInProgress.activeStorage.FindIndex(x => x.category == categorySelected && x.weapon == weaponSelected);
            actPlayerAbility abilityToModify;

            for (int i = 0; i < storedSettingsInProgress.activeStorage.Count; i++)
            {
                if (i == abiIndex)
                {
                    abilityToModify = storedSettingsInProgress.activeStorage[abiIndex];
                    abilityToModify.active = true;
                    storedSettingsInProgress.activeStorage[abiIndex] = abilityToModify;
                }
                else if (storedSettingsInProgress.activeStorage[i].category == categorySelected)
                {
                    abilityToModify = storedSettingsInProgress.activeStorage[i];
                    abilityToModify.active = false;
                    storedSettingsInProgress.activeStorage[i] = abilityToModify;
                }
            }
        }
        #endregion

        #region Passive Methods Section
        private void CombSelected()
        {
            combChoice.image.sprite = Resources.Load<Sprite>("UI\\Passive Skills\\Combattente");
            storedSettingsInProgress.passiveStorage.fighting.active = true;
        }

        private void EsploSelected()
        {
            esploChoice.image.sprite = Resources.Load<Sprite>("UI\\Passive Skills\\Esploratore");
            storedSettingsInProgress.passiveStorage.traveler.active = true;
        }

        private void SoprSelected()
        {
            soprChoice.image.sprite = Resources.Load<Sprite>("UI\\Passive Skills\\Sopravvissuto");
            storedSettingsInProgress.passiveStorage.survivor.active = true;
        }

        private void CombDeSelected()
        {
            combChoice.image.sprite = Resources.Load<Sprite>("UI\\Passive Skills\\CombattenteDisabled");
            storedSettingsInProgress.passiveStorage.fighting.active = false;
        }

        private void EsploDeSelected()
        {
            esploChoice.image.sprite = Resources.Load<Sprite>("UI\\Passive Skills\\EsploratoreDisabled");
            storedSettingsInProgress.passiveStorage.traveler.active = false;
        }

        private void SoprDeSelected()
        {
            soprChoice.image.sprite = Resources.Load<Sprite>("UI\\Passive Skills\\SopravvissutoDisabled");
            storedSettingsInProgress.passiveStorage.survivor.active = false;
        }

        private void CheckCombUpgrade()
        {
            if (storedSettingsInProgress.passiveStorage.fighting.level == storedSettingsInProgress.passiveStorage.fighting.maxLevel)
                passiveUp.interactable = false;
            else
                passiveUp.interactable = true;
        }

        private void CheckEsploUpgrade()
        {
            if (storedSettingsInProgress.passiveStorage.traveler.level == storedSettingsInProgress.passiveStorage.traveler.maxLevel)
                passiveUp.interactable = false;
            else
                passiveUp.interactable = true;
        }

        private void CheckSoprUpgrade()
        {
            if (storedSettingsInProgress.passiveStorage.survivor.level == storedSettingsInProgress.passiveStorage.survivor.maxLevel)
                passiveUp.interactable = false;
            else
                passiveUp.interactable = true;
        }
        #endregion

        #region Upgrade Methods Section

        private void CheckUpgradePossibility()
        {
            if (!UpgradePossible())
                DisableUpgrades();
        }

        private bool UpgradePossible()
        {
            if (storedSettingsInProgress.unspentAbilityPoints == 0)
                return false;
            else
                return true;
        }

        private void DisableUpgrades()
        {
            impetoUp.interactable = false;
            respiroUp.interactable = false;
            passiveUp.interactable = false;
        }

        private void EnableUpgrades()
        {
            impetoUp.interactable = true;
            respiroUp.interactable = true;
            passiveUp.interactable = true;
        }

        private void UpgradeImpeto()
        {
            Debug.Log("Impeto Upgraded");
            IncreasePrimSecAbilityPower(originalName.Impeto);
            storedSettingsInProgress.unspentAbilityPoints--;
            ImpetoUpCounter++;
            unspentPoints.text = "" + storedSettingsInProgress.unspentAbilityPoints;
        }

        private void UpgradeRespiro()
        {
            Debug.Log("Respiro Upgraded");
            IncreasePrimSecAbilityPower(originalName.RespiroDelVento);
            storedSettingsInProgress.unspentAbilityPoints--;
            RespiroUpCounter++;
            unspentPoints.text = "" + storedSettingsInProgress.unspentAbilityPoints;
        }

        private void CheckWhatPassiveSelected()
        {
            if (combChoice.image.sprite.name == "Combattente")
            {
                passiveUp.onClick.RemoveAllListeners();
                passiveUp.onClick.AddListener(() => UpgradeComb());
                passiveUp.onClick.AddListener(() => CheckUpgradePossibility());
            }
            else if (esploChoice.image.sprite.name == "Esploratore")
            {
                passiveUp.onClick.RemoveAllListeners();
                passiveUp.onClick.AddListener(() => UpgradeEsplo());
                passiveUp.onClick.AddListener(() => CheckUpgradePossibility());
            }
            else if (soprChoice.image.sprite.name == "Sopravvissuto")
            {
                passiveUp.onClick.RemoveAllListeners();
                passiveUp.onClick.AddListener(() => UpgradeSopr());
                passiveUp.onClick.AddListener(() => CheckUpgradePossibility());
            }
        }

        private void UpgradeComb()
        {
            Debug.Log("Comb Upgraded");
            IncreaseFighter();
            storedSettingsInProgress.unspentAbilityPoints--;
            unspentPoints.text = "" + storedSettingsInProgress.unspentAbilityPoints;
        }

        private void UpgradeEsplo()
        {
            Debug.Log("Esplo Upgraded");
            IncreaseTrav();
            storedSettingsInProgress.unspentAbilityPoints--;
            unspentPoints.text = "" + storedSettingsInProgress.unspentAbilityPoints;
        }

        private void UpgradeSopr()
        {
            Debug.Log("Sopr Upgraded");
            IncreaseSurv();
            storedSettingsInProgress.unspentAbilityPoints--;
            unspentPoints.text = "" + storedSettingsInProgress.unspentAbilityPoints;
        }


        public void IncreasePrimSecAbilityPower(originalName abilityname)
        {
            for (int i = 0; i < playerStoredSettings.activeStorage.Count; i++)
            {
                if (storedSettingsInProgress.activeStorage[i].oname == abilityname)
                    storedSettingsInProgress.activeStorage[i] = IncreaseActAbilityLevel(playerStoredSettings.activeStorage[i]);
            }

        }

        public actPlayerAbility IncreaseActAbilityLevel(actPlayerAbility abiToIncrease)
        {

            abiToIncrease.level++;
            abiToIncrease.damage += abiToIncrease.damIncPerLevel;
            abiToIncrease.range += abiToIncrease.rangeIncPerLevel;
            abiToIncrease.cooldown -= abiToIncrease.cooldownDecPerLevel;
            abiToIncrease.areaEffect += abiToIncrease.aeIncPerLevel;
            abiToIncrease.knockBack += abiToIncrease.kbIncPerLevel;

            return abiToIncrease;
        }

        public void IncreaseFighter()
        {
            storedSettingsInProgress.passiveStorage.fighting.level++;
            storedSettingsInProgress.passiveStorage.fighting.comPower += storedSettingsInProgress.passiveStorage.fighting.comIncPerLevel;
        }

        public void IncreaseTrav()
        {
            storedSettingsInProgress.passiveStorage.traveler.level++;
            storedSettingsInProgress.passiveStorage.traveler.espPower += storedSettingsInProgress.passiveStorage.traveler.espIncPerLevel;
        }

        public void IncreaseSurv()
        {
            storedSettingsInProgress.passiveStorage.survivor.level++;
            storedSettingsInProgress.passiveStorage.survivor.sopPower += storedSettingsInProgress.passiveStorage.survivor.sopIncPerLevel;
        }

        #endregion

        #region Reset Methods Section
        private void ResetImpeto()
        {
            for (int i = 0; i < ImpetoUpCounter; i++)
                DecreasePrimSecAbilityPower(originalName.Impeto);

            ImpetoUpCounter = 0;
        }

        private void ResetRespiro()
        {
            for (int i = 0; i < RespiroUpCounter; i++)
                DecreasePrimSecAbilityPower(originalName.RespiroDelVento);

            RespiroUpCounter = 0;
        }

        private void ResetEverythingElse()
        {
            storedSettingsInProgress = playerStoredSettings;
            unspentPoints.text = "" + storedSettingsInProgress.unspentAbilityPoints;
        }

        public void DecreasePrimSecAbilityPower(originalName abilityname)
        {
            for (int i = 0; i < playerStoredSettings.activeStorage.Count; i++)
            {
                if (storedSettingsInProgress.activeStorage[i].oname == abilityname)
                    storedSettingsInProgress.activeStorage[i] = DecreaseActAbilityLevel(playerStoredSettings.activeStorage[i]);
            }

        }

        public actPlayerAbility DecreaseActAbilityLevel(actPlayerAbility abiToDecrease)
        {

            abiToDecrease.level--;
            abiToDecrease.damage -= abiToDecrease.damIncPerLevel;
            abiToDecrease.range -= abiToDecrease.rangeIncPerLevel;
            abiToDecrease.cooldown += abiToDecrease.cooldownDecPerLevel;
            abiToDecrease.areaEffect -= abiToDecrease.aeIncPerLevel;
            abiToDecrease.knockBack -= abiToDecrease.kbIncPerLevel;

            return abiToDecrease;
        }
        #endregion

        private void passingSettings()
        {
            Game_Controller.instance.playerStoredSettings = storedSettingsInProgress;
        }

        #endregion

        #region Reference Methods


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

            mainMenuReturn.onClick.AddListener(() => Scene_Manager.instance.LoadSpecificScene(0));
            resetLevel.onClick.AddListener(() => Scene_Manager.instance.ResettingLevel());

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


            popupP = GameObject.FindGameObjectWithTag("PopupP");

            resume = GameObject.FindGameObjectWithTag("PopupP").transform.FindChild("Riprendi Partita").GetComponent<Button>();
            exitByPause = GameObject.FindGameObjectWithTag("PopupP").transform.FindChild("Esci").GetComponent<Button>();
            comInstrByPause = GameObject.FindGameObjectWithTag("PopupP").transform.FindChild("ComInstr").GetComponent<Button>();
            instr1 = GameObject.FindGameObjectWithTag("PopupP").transform.FindChild("ComInstr").transform.FindChild("Instr1").gameObject;
            instr2 = GameObject.FindGameObjectWithTag("PopupP").transform.FindChild("ComInstr").transform.FindChild("Instr2").gameObject;
            instrText = GameObject.FindGameObjectWithTag("PopupP").transform.Find("ComInstr").transform.Find("Text").GetComponent<Text>();

            exitByPause.onClick.AddListener(() => Scene_Manager.instance.LoadSpecificScene(0));
            resume.onClick.AddListener(() => Resume());
            comInstrByPause.onClick.AddListener(() => ComInstrByPause());

            instr2.SetActive(false);
            popupP.SetActive(false);

            
        }

        #region Hud Update
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

        #region Pause
        public void ShowPause()
        {
            Game_Controller.instance.currentPhase = GAME_PHASE.dialogue;
            popupP.SetActive(true);
            exitByPause.gameObject.SetActive(true);
            resume.gameObject.SetActive(true);
            comInstrByPause.gameObject.SetActive(true);
            instrText.gameObject.SetActive(true);
            instr1.SetActive(false);
            instr2.SetActive(false);
        }

        private void Resume()
        {
            popupP.SetActive(false);
            Game_Controller.instance.currentPhase = GAME_PHASE.playerTurn;
        }

        private void ComInstrByPause()
        {
            instr1.SetActive(true);
            exitByPause.gameObject.SetActive(false);
            resume.gameObject.SetActive(false);

            instrText.gameObject.SetActive(false);
        }

        public bool IsPhase1ComInstrActive()
        {
            return instr1.activeInHierarchy;
        }

        public void SetPhase2ComInstrActive()
        {
            instr1.SetActive(false);
            instr2.SetActive(true);
        }

        public bool IsPhase2ComInstrActive()
        {
            return instr2.activeInHierarchy;
        }
        #endregion

        #region GameOver
        public void ShowGameOver()
        {
            gameOver.SetActive(true);
            Game_Controller.instance.currentPhase = GAME_PHASE.dialogue;
        }
        #endregion

        #region Trainer
        public void SetStory(int storyNumber)
        {
            popupT.SetActive(true);

            for (int i = 0; i < trainerStories.Count; i++)
            {
                if (i != storyNumber)
                    trainerStories[i].SetActive(false);
                else
                    trainerStories[i].SetActive(true);
            }

            Game_Controller.instance.currentPhase = GAME_PHASE.dialogue;
        }

        public void UnSetStory()
        {
            for (int i = 0; i < trainerStories.Count; i++)
            {
                    trainerStories[i].SetActive(false);       
            }

            popupT.SetActive(false);

            Game_Controller.instance.currentPhase = GAME_PHASE.playerTurn;
        }

        public bool isStoryActive()
        {
            return popupT.activeInHierarchy;
        }
        #endregion

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