using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Spacchiamo
{

    #region Player Data Structure

    public enum type { Primary, Secondary };

    public enum originalName { Impeto, RespiroDelVento };

    public enum weaponType { ArmaBianca, Catalizzatore, ArmaRanged };

    public enum pOriginalName { Rigenerazione, NotFound };

    [System.Serializable]
    public struct actPlayerAbility
    {
        public type category;
        public originalName oname;
        public string customName;
        public weaponType weapon;
        public int level;
        public int maxLevel;
        public float damage;
        public float damIncPerLevel;
        public int range;
        public int rangeIncPerLevel;
        public int cooldown;
        public int cooldownDecPerLevel;
        public int areaEffect;
        public int aeIncPerLevel;
        public int knockBack;
        public int kbIncPerLevel;
        public bool discovered;
        public bool active;
        public bool isInCooldown;
    }

    [System.Serializable]
    public struct regAbility
    {
        public const pOriginalName oname = pOriginalName.Rigenerazione;
        public const string customName = "Rigenerazione";
        public int level;
        public int maxLevel;
        public float regPower;
        public float rpIncPerLevel;
        public int cooldown;
        public int cooldownDecPerLevel;
        public int regCounter;
        public bool discovered;
        public bool active;
    }

    [System.Serializable]
    public struct passAbilities
    {
        public regAbility regeneration;
    }

    [System.Serializable]
    public struct playerSettings
    {
        public float Life;
        public int expGained;
        public int unspentAbilityPoints;
        public int playerLevel;
        public int healthPotStacks;
        public int TurnValue;
        public int FearValue;
        public int fearTurnCounter;
        public bool fear1Activated;
        public bool fear2Activated;
        public float fear1Percent;
        public float fear2Percent;
        public List<actPlayerAbility> activeStorage;
        public passAbilities passiveStorage;
    }
    #endregion


    #region Enemies Data Structure

    public enum eneOriginalName { melee, ranged, Kamikaze }

    public enum enemy { enemy1, enemy2, enemy3, enemy4, enemy5, enemy6, boss };

    public enum behaviour { normal, kamikaze, ghost, fearMonster, boss };

    public enum patrolArea { manhattan, horizontal, vertical };

    public enum patrolStyle { standingStill, randomic, precRight, precLeft, precUp, precDown };

    public enum aggroStyle { standingStill, following };

    [System.Serializable]
    public struct actEnemyAbility
    {
        public eneOriginalName oname;
        public float damage;
        public float damIncPerLevel;
        public int range;
        public int rangeIncPerLevel;
        public int cooldown;
        public int cooldownDecPerLevel;
        public bool isInCooldown;
    }

    [System.Serializable]
    public struct enemySetting
    {
        public enemy name;
        public int level;
        public float life;
        public float lifeIncPerLevel;
        public int moveRate;
        public int moveRateDecPerLevel;
        public int aggroRange;
        public int aggroRangeIncPerLevel;
        public int experience;
        public int expIncPerLevel;
        public patrolArea patrolArea;
        public patrolStyle patrolStyle;
        public int patrolRange;
        public aggroStyle aggroStyle;
        public behaviour behaviour;
        public actEnemyAbility ability;
    }

    #endregion


    public enum GAME_PHASE : byte { init, playerTurn, npcEnemyTurn, knockAni, dialogue};



    public class Game_Controller : MonoBehaviour
    {

        #region Phase Variables
        // obvious LOL
        public GAME_PHASE currentPhase = GAME_PHASE.init;
        public GAME_PHASE previousPhase = GAME_PHASE.playerTurn;
        #endregion

        #region All Reference Variables
        // to be passed to Enemy Manager
        GameObject[] enemy1Array, enemy2Array, enemy3Array, enemy4Array, enemy5Array, enemy6Array, enemy7Array;

        // to be passed to Grid Manager
        GameObject[] faloList;

        //Exit Reference
        GameObject exit;

        //Ui References to be Passed
        Text fear, turnCount;
        Slider fearBar;
        UILifePanelScript lifePanelScript;

        // to give Camera the player as target
        Camera_Movement cameraLink;

        // player reference to be passed on many scripts
        [HideInInspector]
        public GameObject playerLink;

        #endregion

        #region Player Data Save New
        public playerSettings playerStoredSettings = new playerSettings(); 
        #endregion

        #region SingleTone
        [HideInInspector]
        public static Game_Controller instance = null;

        void Awake()
        {

           

            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(this.gameObject);

        }
        #endregion

        void Start()
        {
            #region Taking All References
            // Finding the necessary References to start the initialization sequence
            playerLink = GameObject.FindGameObjectWithTag("Player");
            cameraLink = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Movement>();
            faloList = GameObject.FindGameObjectsWithTag("Falo");
            exit = GameObject.FindGameObjectWithTag("Finish");
            enemy1Array = GameObject.FindGameObjectsWithTag("Enemy1");
            enemy2Array = GameObject.FindGameObjectsWithTag("Enemy2");
            enemy3Array = GameObject.FindGameObjectsWithTag("Enemy3");
            enemy4Array = GameObject.FindGameObjectsWithTag("Enemy4");
            enemy5Array = GameObject.FindGameObjectsWithTag("Enemy5");
            enemy6Array = GameObject.FindGameObjectsWithTag("Enemy6");
            enemy7Array = GameObject.FindGameObjectsWithTag("Enemy7");

            fear = GameObject.Find("Fear Counter").GetComponent<Text>();
            fearBar = GameObject.Find("Fear Bar").GetComponent<Slider>();
            turnCount = GameObject.Find("Turn counter").GetComponent<Text>();
            lifePanelScript = GameObject.Find("Life Panel").GetComponent<UILifePanelScript>();
            #endregion

            #region Hud Initialization (all gameplay scenes)
            //Ui initialization
            Ui_Manager.instance.TakingReferences(fear, turnCount, fearBar, lifePanelScript);
            Ui_Manager.instance.UiInitialization();
            #endregion

            #region Scene Manager Initialization (all scenes)
            //Scene Manager initilization
            Scene_Manager.instance.SceneManagerInitialization();
            #endregion

            #region Grid Initialization (all gameplay scenes)
            // Grid Initialization
            Grid_Manager.instance.GivingPlayerRef(playerLink);
            Grid_Manager.instance.PreparingOptimizedGridSpace();
            Grid_Manager.instance.LinkingFaloMechanic(faloList);
            Grid_Manager.instance.LinkingExit(exit);
            #endregion

            #region Player Scene Initialization (all gameplay scenes)
            //Player Initialization
            Grid_Manager.instance.GettingLight(playerLink.GetComponent<playerActions>().GettingXPlayer(), playerLink.GetComponent<playerActions>().GettingyPlayer());
            playerActions playerPosition = playerLink.GetComponent<playerActions>();
            Grid_Manager.instance.SwitchingOccupiedStatus(playerPosition.GettingXPlayer(), playerPosition.GettingyPlayer());
            playerPosition.whereToGo = Grid_Manager.instance.GetCellTransform(playerPosition.GettingXPlayer(), playerPosition.GettingyPlayer());
            cameraLink.transform.position = playerLink.transform.position - new Vector3(0, 0, 10);
            cameraLink.target = playerLink;
            playerLink.GetComponent<playerActions>().InitializeSortingOrder();

            if (playerStoredSettings.activeStorage.Count == 0)
            {
                InitializePlayerStats();
                InitializeAbiStorage();
                TakingDesAbilitiesExp();
                playerLink.GetComponent<Player_Controller>().CurSet = playerStoredSettings;
                playerLink.GetComponent<Player_Controller>().SelectingActiveAbilities();
            }
            else
            {
                playerLink.GetComponent<Player_Controller>().CurSet = playerStoredSettings;
                playerLink.GetComponent<Player_Controller>().SelectingActiveAbilities();
            } 
   

            #endregion

            #region Enemy Scene Initialization (only first call is scene based, otherwise is for all gameplay scenes)
            // Enemies Initialization
            AbiRepository.instance.SetEnemyLevels(1, 1, 1, 1, 1);
            Enemies_Manager.instance.SetEnemyManagerStructs();

            Enemies_Manager.instance.ClearEnemyReferences();

            Enemies_Manager.instance.PassingEnemyList(enemy1Array);
            Enemies_Manager.instance.PassingEnemyList(enemy2Array);
            Enemies_Manager.instance.PassingEnemyList(enemy3Array);
            Enemies_Manager.instance.PassingEnemyList(enemy4Array);
            Enemies_Manager.instance.PassingEnemyList(enemy5Array);
            Enemies_Manager.instance.PassingEnemyList(enemy6Array);
            Enemies_Manager.instance.PassingEnemyList(enemy7Array);

            Enemies_Manager.instance.GivingPlayerRef(playerLink);


            Enemies_Manager.instance.ImplementingEachEnemySettings();
            Enemies_Manager.instance.SettingOccupiedInitialStatus();
            Enemies_Manager.instance.SettingEnemyVisibility();

            Enemies_Manager.instance.SettingSortingOrder();
            Enemies_Manager.instance.InitializeWhereToGo();
            Enemies_Manager.instance.InitilizeLifeFeedBack();

            Grid_Manager.instance.AddingElementsAStarCells(Enemies_Manager.instance.RetrieveEnemiesNumber());

            currentPhase = GAME_PHASE.playerTurn;
            #endregion

        }

        void OnLevelWasLoaded(int level)
        {

            if (level != 0)
            {

                #region Taking All References
                // Finding the necessary References to start the initialization sequence
                playerLink = GameObject.FindGameObjectWithTag("Player");
                cameraLink = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Movement>();
                faloList = GameObject.FindGameObjectsWithTag("Falo");
                exit = GameObject.FindGameObjectWithTag("Finish");
                enemy1Array = GameObject.FindGameObjectsWithTag("Enemy1");
                enemy2Array = GameObject.FindGameObjectsWithTag("Enemy2");
                enemy3Array = GameObject.FindGameObjectsWithTag("Enemy3");
                enemy4Array = GameObject.FindGameObjectsWithTag("Enemy4");
                enemy5Array = GameObject.FindGameObjectsWithTag("Enemy5");
                enemy6Array = GameObject.FindGameObjectsWithTag("Enemy6");
                enemy7Array = GameObject.FindGameObjectsWithTag("Enemy7");

                fear = GameObject.Find("Fear Counter").GetComponent<Text>();
                fearBar = GameObject.Find("Fear Bar").GetComponent<Slider>();
                turnCount = GameObject.Find("Turn counter").GetComponent<Text>();
                lifePanelScript = GameObject.Find("Life Panel").GetComponent<UILifePanelScript>();
                #endregion

                #region Hud Initialization (all gameplay scenes)
                //Ui initialization
                Ui_Manager.instance.TakingReferences(fear, turnCount, fearBar, lifePanelScript);
                Ui_Manager.instance.UiInitialization();
                #endregion

                #region Scene Manager Initialization (all scenes)
                //Scene Manager initilization
                Scene_Manager.instance.SceneManagerInitialization();
                #endregion

                #region Grid Initialization (all gameplay scenes)
                // Grid Initialization
                Grid_Manager.instance.GivingPlayerRef(playerLink);
                Grid_Manager.instance.PreparingOptimizedGridSpace();
                Grid_Manager.instance.LinkingFaloMechanic(faloList);
                Grid_Manager.instance.LinkingExit(exit);
                #endregion

                #region Player Scene Initialization (all gameplay scenes)
                //Player Initialization
                Grid_Manager.instance.GettingLight(playerLink.GetComponent<playerActions>().GettingXPlayer(), playerLink.GetComponent<playerActions>().GettingyPlayer());
                playerActions playerPosition = playerLink.GetComponent<playerActions>();
                Grid_Manager.instance.SwitchingOccupiedStatus(playerPosition.GettingXPlayer(), playerPosition.GettingyPlayer());
                playerPosition.whereToGo = Grid_Manager.instance.GetCellTransform(playerPosition.GettingXPlayer(), playerPosition.GettingyPlayer());
                cameraLink.transform.position = playerLink.transform.position - new Vector3(0, 0, 10);
                cameraLink.target = playerLink;
                playerLink.GetComponent<playerActions>().InitializeSortingOrder();


                if (playerStoredSettings.activeStorage.Count == 0)
                {
                    InitializePlayerStats();
                    InitializeAbiStorage();
                    TakingDesAbilitiesExp();
                    playerLink.GetComponent<Player_Controller>().CurSet = playerStoredSettings;
                    playerLink.GetComponent<Player_Controller>().SelectingActiveAbilities();
                }
                else
                {
                    playerLink.GetComponent<Player_Controller>().CurSet = playerStoredSettings;
                    playerLink.GetComponent<Player_Controller>().SelectingActiveAbilities();
                }


                #endregion

                #region Enemy Scene Initialization (only first call is scene based, otherwise is for all gameplay scenes)
                // Enemies Initialization
                AbiRepository.instance.SetEnemyLevels(1, 1, 1, 1, 1);
                Enemies_Manager.instance.SetEnemyManagerStructs();

                Enemies_Manager.instance.ClearEnemyReferences();

                Enemies_Manager.instance.PassingEnemyList(enemy1Array);
                Enemies_Manager.instance.PassingEnemyList(enemy2Array);
                Enemies_Manager.instance.PassingEnemyList(enemy3Array);
                Enemies_Manager.instance.PassingEnemyList(enemy4Array);
                Enemies_Manager.instance.PassingEnemyList(enemy5Array);
                Enemies_Manager.instance.PassingEnemyList(enemy6Array);
                Enemies_Manager.instance.PassingEnemyList(enemy7Array);

                Enemies_Manager.instance.GivingPlayerRef(playerLink);


                Enemies_Manager.instance.ImplementingEachEnemySettings();
                Enemies_Manager.instance.SettingOccupiedInitialStatus();
                Enemies_Manager.instance.SettingEnemyVisibility();

                Enemies_Manager.instance.SettingSortingOrder();
                Enemies_Manager.instance.InitializeWhereToGo();
                Enemies_Manager.instance.InitilizeLifeFeedBack();

                Grid_Manager.instance.AddingElementsAStarCells(Enemies_Manager.instance.RetrieveEnemiesNumber());

                currentPhase = GAME_PHASE.playerTurn;
                #endregion

            }
            else
            {
                #region Ability Ui Initialization

                Ui_Manager.instance.

                #endregion
            }


        }


        #region Player Initialization Methods

        private void InitializePlayerStats()
        {
            playerStoredSettings.Life = AbiRepository.instance.playerInitialSetting.Life;
            playerStoredSettings.expGained = AbiRepository.instance.playerInitialSetting.expGained;
            playerStoredSettings.unspentAbilityPoints = AbiRepository.instance.playerInitialSetting.unspentAbilityPoints;
            playerStoredSettings.playerLevel = AbiRepository.instance.playerInitialSetting.playerLevel;
            playerStoredSettings.healthPotStacks = AbiRepository.instance.playerInitialSetting.healthPotStacks;
            playerStoredSettings.FearValue = AbiRepository.instance.playerInitialSetting.FearValue;
            playerStoredSettings.TurnValue = AbiRepository.instance.playerInitialSetting.TurnValue;
            playerStoredSettings.fearTurnCounter = AbiRepository.instance.playerInitialSetting.fearTurnCounter;
            playerStoredSettings.fear1Activated = AbiRepository.instance.playerInitialSetting.fear1Activated;
            playerStoredSettings.fear2Activated = AbiRepository.instance.playerInitialSetting.fear2Activated;
            playerStoredSettings.fear1Percent = AbiRepository.instance.playerInitialSetting.fear1Percent;
            playerStoredSettings.fear2Percent = AbiRepository.instance.playerInitialSetting.fear2Percent;
        }

        private void InitializeAbiStorage()
        {
            playerStoredSettings.activeStorage.AddRange(AbiRepository.instance.playerInitialSetting.activeStorage);
            playerStoredSettings.passiveStorage = AbiRepository.instance.playerInitialSetting.passiveStorage;
        }

        private void TakingDesAbilitiesExp()
        {
            actPlayerAbility currentAbility = new actPlayerAbility();

            currentAbility = AbiRepository.instance.playerInitialSetting.activeStorage.Find(x => x.oname == Designer_Tweaks.instance.primaryTesting && x.weapon == Designer_Tweaks.instance.primaryWeapon);
            currentAbility.active = true;
            currentAbility.discovered = true;

            for (int i = 1; i < Designer_Tweaks.instance.primaryLevel; i++)
                currentAbility = IncreaseActAbilityLevel(currentAbility);

            int abiIndex;

            abiIndex = playerStoredSettings.activeStorage.FindIndex(x => x.oname == Designer_Tweaks.instance.primaryTesting && x.weapon == Designer_Tweaks.instance.primaryWeapon);

            playerStoredSettings.activeStorage.RemoveAt(abiIndex);
            playerStoredSettings.activeStorage.Insert(abiIndex, currentAbility);

            

            currentAbility = AbiRepository.instance.playerInitialSetting.activeStorage.Find(x => x.oname == Designer_Tweaks.instance.seconTesting && x.weapon == Designer_Tweaks.instance.seconWeapon);
            currentAbility.active = true;
            currentAbility.discovered = true;

            for (int i = 1; i < Designer_Tweaks.instance.seconLevel; i++)
                currentAbility = IncreaseActAbilityLevel(currentAbility);

            abiIndex = playerStoredSettings.activeStorage.FindIndex(x => x.oname == Designer_Tweaks.instance.seconTesting && x.weapon == Designer_Tweaks.instance.seconWeapon);

            playerStoredSettings.activeStorage.RemoveAt(abiIndex);
            playerStoredSettings.activeStorage.Insert(abiIndex, currentAbility);

            if (Designer_Tweaks.instance.passiveTesting == pOriginalName.Rigenerazione)
            {
                regAbility regPassive = new regAbility();
                regPassive = AbiRepository.instance.playerInitialSetting.passiveStorage.regeneration;

                regPassive.active = true;
                regPassive.discovered = true;

                for (int i = 1; i < Designer_Tweaks.instance.passiveLevel; i++)
                    regPassive = IncreaseRegLevel(regPassive);

                playerStoredSettings.passiveStorage.regeneration = regPassive;
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

        public regAbility IncreaseRegLevel(regAbility increase)
        {

            increase.level++;
            increase.regPower += increase.rpIncPerLevel;
            increase.cooldown -= increase.cooldownDecPerLevel;


            return increase;
        }
        #endregion

        #region General Methods (called by other scripts)
        // Methods to be Called on Request by other scripts
        public GameObject TakingPlayerRef()
        {
            return playerLink;
        } 

        public void SavePlayerData(playerSettings currentSetting)
        {
            playerStoredSettings = currentSetting;
        }

        #endregion
    }
}