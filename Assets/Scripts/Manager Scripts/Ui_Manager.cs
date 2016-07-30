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