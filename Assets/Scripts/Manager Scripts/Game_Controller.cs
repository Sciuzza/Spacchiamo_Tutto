using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Spacchiamo
{

    #region Player Data Structure

    public enum type { Primary, Secondary };

    public enum originalName { Impeto, RespiroDelVento, NotFound };

    public enum weaponType { ArmaBianca, Catalizzatore, ArmaRanged, NotFound };

    public enum pOriginalName { Rigenerazione, Combattente, Esploratore, Sopravvissuto, NotFound };

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
        public int cdCounter;
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
    public struct combattente
    {
        public const pOriginalName oname = pOriginalName.Combattente;
        public const string customName = "Combattente";
        public int level;
        public int maxLevel;
        public float comPower;
        public float comIncPerLevel;
        public bool discovered;
        public bool active;
    }

    [System.Serializable]
    public struct esploratore
    {
        public const pOriginalName oname = pOriginalName.Esploratore;
        public const string customName = "Esploratore";
        public int level;
        public int maxLevel;
        public float espPower;
        public float espIncPerLevel;
        public bool discovered;
        public bool active;
    }

    [System.Serializable]
    public struct sopravvissuto
    {
        public const pOriginalName oname = pOriginalName.Sopravvissuto;
        public const string customName = "Sopravvissuto";
        public int level;
        public int maxLevel;
        public float sopPower;
        public float sopIncPerLevel;
        public bool discovered;
        public bool active;
    }

    [System.Serializable]
    public struct passAbilities
    {
        public regAbility regeneration;
        public combattente fighting;
        public esploratore traveler;
        public sopravvissuto survivor;
    }

    [System.Serializable]
    public struct playerSettings
    {
        public float Life;
        public float maxLife;
        public int lightRange;
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


    public enum GAME_PHASE : byte { init, playerTurn, npcEnemyTurn, knockAni, dialogue };



    public class Game_Controller : MonoBehaviour
    {

        #region Phase Variables
        // obvious LOL
        public GAME_PHASE currentPhase;
        public GAME_PHASE previousPhase;
        #endregion

        #region All Reference Variables
        // to be passed to Enemy Manager
        GameObject[] enemy1Array, enemy2Array, enemy3Array, enemy4Array, enemy5Array, enemy6Array, enemy7Array;

        // trainer Ref
        GameObject trainer;
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

        // References necessary to initilize Ability UI
        UIAbilitiesAndWeaponsCanvasScript abilitiesAndWeaponsCanvasScript;
        UIAbilitiesAndWeaponsTooltipCallerScript callerAbiWea;
        UIAbilitiesAndWeaponsPointsScript callerPoints;

        #endregion

        #region Player Data Save New
        public playerSettings playerStoredSettings = new playerSettings();
        #endregion

        #region Option Values
        int difficultySet = 0;
        bool audioIsOn = false;
        float audioVolumeSet = 0.0f;

        #endregion

        #region EndCredits Check
        public bool creditsEnded;
        #endregion

        #region Story Counter
        int storyCounter = 0;
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

            #region Scene Manager Initialization (all scenes)
            //Scene Manager initilization
            Scene_Manager.instance.SceneManagerInitialization();
            #endregion

            #region Ui Menu Initilization
           // Ui_Manager.instance.InitializingMainMenu();
            #endregion



            
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
            trainer = GameObject.FindGameObjectWithTag("Trainer");


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

          

            #region Grid Initialization (all gameplay scenes)
            // Grid Initialization
            Grid_Manager.instance.GivingPlayerRef(playerLink);
            Grid_Manager.instance.PreparingOptimizedGridSpace();
            Grid_Manager.instance.LinkingFaloMechanic(faloList);
            if (exit != null)
                Grid_Manager.instance.LinkingExit(exit);
            if (trainer != null)
                Grid_Manager.instance.LinkingTrainer(trainer);
            #endregion

            #region Player Scene Initialization (all gameplay scenes)
            //Player Initialization
            playerActions playerPosition = playerLink.GetComponent<playerActions>();
            Grid_Manager.instance.SwitchingOccupiedStatus(playerPosition.GettingXPlayer(), playerPosition.GettingyPlayer());
            playerPosition.whereToGo = Grid_Manager.instance.GetCellTransform(playerPosition.GettingXPlayer(), playerPosition.GettingyPlayer());
            cameraLink.transform.position = playerLink.transform.position - new Vector3(0, 0, 10);
            cameraLink.target = playerLink;
            playerLink.GetComponent<playerActions>().InitializeSortingOrder();


            playerStoredSettings.healthPotStacks = 2;
            playerStoredSettings.lightRange = Designer_Tweaks.instance.playerLightM;

            if (playerStoredSettings.activeStorage.Count == 0)
            {
                playerStoredSettings = AbiRepository.instance.playerInitialSetting;
                TakingDesAbilitiesExp();
                playerLink.GetComponent<Player_Controller>().CurSet = playerStoredSettings;
                playerLink.GetComponent<Player_Controller>().SelectingActiveAbilities();
            }
            else
            {
                playerLink.GetComponent<Player_Controller>().CurSet = playerStoredSettings;
                playerLink.GetComponent<Player_Controller>().SelectingActiveAbilities();
            }

            Grid_Manager.instance.GettingLight(playerLink.GetComponent<playerActions>().GettingXPlayer(), playerLink.GetComponent<playerActions>().GettingyPlayer());
            Ui_Manager.instance.SettingLife((int)playerStoredSettings.Life);
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


            Enemies_Manager.instance.ImplementingEachEnemySettings(0);
            Enemies_Manager.instance.SettingOccupiedInitialStatus(0);
            Enemies_Manager.instance.SettingEnemyVisibility(0);

            Enemies_Manager.instance.SettingSortingOrder(0);
            Enemies_Manager.instance.InitializeWhereToGo(0);
            Enemies_Manager.instance.InitilizeLifeFeedBack(0);
            Enemies_Manager.instance.InitializeAggroFeedBack(0);

            Grid_Manager.instance.AddingElementsAStarCells(Enemies_Manager.instance.RetrieveEnemiesNumber());

            currentPhase = GAME_PHASE.playerTurn;
            #endregion
            
        }

        void OnLevelWasLoaded(int level)
        {

            #region Scene Manager Initialization (all scenes)
            //Scene Manager initilization
            Scene_Manager.instance.SceneManagerInitialization();
            #endregion


            if (level > 5)
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
                trainer = GameObject.FindGameObjectWithTag("Trainer");

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

                #region Grid Initialization (all gameplay scenes)
                // Grid Initialization
                Grid_Manager.instance.GivingPlayerRef(playerLink);
                Grid_Manager.instance.PreparingOptimizedGridSpace();
                Grid_Manager.instance.LinkingFaloMechanic(faloList);
                if (exit != null)
                    Grid_Manager.instance.LinkingExit(exit);
                if (trainer != null)
                    Grid_Manager.instance.LinkingTrainer(trainer);
                #endregion

                #region Player Scene Initialization (all gameplay scenes)
                //Player Initialization
                playerActions playerPosition = playerLink.GetComponent<playerActions>();
                Grid_Manager.instance.SwitchingOccupiedStatus(playerPosition.GettingXPlayer(), playerPosition.GettingyPlayer());
                playerPosition.whereToGo = Grid_Manager.instance.GetCellTransform(playerPosition.GettingXPlayer(), playerPosition.GettingyPlayer());
                cameraLink.transform.position = playerLink.transform.position - new Vector3(0, 0, 10);
                cameraLink.target = playerLink;
                playerLink.GetComponent<playerActions>().InitializeSortingOrder();

                playerStoredSettings.healthPotStacks = 2;
                playerStoredSettings.lightRange = Designer_Tweaks.instance.playerLightM;

                if (playerStoredSettings.activeStorage.Count == 0)
                {
                    playerStoredSettings = AbiRepository.instance.playerInitialSetting;
                    TakingDesAbilitiesExp();
                    playerLink.GetComponent<Player_Controller>().CurSet = playerStoredSettings;
                    playerLink.GetComponent<Player_Controller>().SelectingActiveAbilities();
                }
                else
                {
                    playerLink.GetComponent<Player_Controller>().CurSet = playerStoredSettings;
                    playerLink.GetComponent<Player_Controller>().SelectingActiveAbilities();
                }

                Grid_Manager.instance.GettingLight(playerLink.GetComponent<playerActions>().GettingXPlayer(), playerLink.GetComponent<playerActions>().GettingyPlayer());
                Ui_Manager.instance.SettingLife((int)playerStoredSettings.Life);
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


                Enemies_Manager.instance.ImplementingEachEnemySettings(0);
                Enemies_Manager.instance.SettingOccupiedInitialStatus(0);
                Enemies_Manager.instance.SettingEnemyVisibility(0);

                Enemies_Manager.instance.SettingSortingOrder(0);
                Enemies_Manager.instance.InitializeWhereToGo(0);
                Enemies_Manager.instance.InitilizeLifeFeedBack(0);
                Enemies_Manager.instance.InitializeAggroFeedBack(0);

                Grid_Manager.instance.AddingElementsAStarCells(Enemies_Manager.instance.RetrieveEnemiesNumber());

                currentPhase = GAME_PHASE.playerTurn;
                #endregion

            }
            else if (level == 5)
            {
            

               
            }
            else if (level == 0)
            {
                Ui_Manager.instance.InitializingMainMenu();
            }
            else if (level == 1)
            {
                Ui_Manager.instance.InitializingOptions(difficultySet, audioIsOn, audioVolumeSet);
            }
            else if (level == 2)
                Ui_Manager.instance.InitiliazingComInstr();
            else if (level == 4)
                Ui_Manager.instance.InitializeStory();
           


        }


        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Scene_Manager.instance.GetCurrentSceneIndex() < 4 && Scene_Manager.instance.GetCurrentSceneIndex() >= 2)
                {
                    Scene_Manager.instance.LoadSpecificScene(0);
                }
                else if (Scene_Manager.instance.GetCurrentSceneIndex() == 1)
                {
                    if (Ui_Manager.instance.AreOptionsChanged(difficultySet, audioIsOn, audioVolumeSet))
                    {
                        if (!Ui_Manager.instance.IsWarningActiveInHierarchy())
                            Ui_Manager.instance.PoppingOutWarning();
                        else
                            Ui_Manager.instance.PoppingInDefault();
                    }
                    else
                        Scene_Manager.instance.LoadSpecificScene(0);
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                if (Scene_Manager.instance.GetCurrentSceneIndex() == 1 && Ui_Manager.instance.IsWarningActiveInHierarchy())
                    Scene_Manager.instance.LoadSpecificScene(0);
                else if (Scene_Manager.instance.GetCurrentSceneIndex() == 2 && !Ui_Manager.instance.IsPhase2Activated())
                    Ui_Manager.instance.ActivatePhase2();
                else if (Scene_Manager.instance.GetCurrentSceneIndex() == 4)
                {
                    if (storyCounter < 7)
                    {
                        storyCounter++;
                        Ui_Manager.instance.SetStoryPhase(storyCounter);
                    }
                    else
                    {
                        Scene_Manager.instance.nextSceneIndex = 5;
                        Scene_Manager.instance.LoadSpecificScene(5);
                    }
                }
            }
        }

        #region Player Initialization Methods

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

        #region AbilityStorageUpgrade

        public void SetRegeneration(int level)
        {
            int levelDiff = level - playerStoredSettings.passiveStorage.regeneration.level;

            for (int i = 0; i < levelDiff; i++)
            {
                playerStoredSettings.passiveStorage.regeneration.regPower += playerStoredSettings.passiveStorage.regeneration.rpIncPerLevel;
                playerStoredSettings.passiveStorage.regeneration.cooldown -= playerStoredSettings.passiveStorage.regeneration.cooldownDecPerLevel;
            }

            playerStoredSettings.passiveStorage.regeneration.level = level;
        }

        public void SetFighting(int level)
        {
            int levelDiff = level - playerStoredSettings.passiveStorage.fighting.level;

            for (int i = 0; i < levelDiff; i++)
            {
                playerStoredSettings.passiveStorage.fighting.comPower += playerStoredSettings.passiveStorage.fighting.comIncPerLevel;
            }

            playerStoredSettings.passiveStorage.fighting.level = level;
        }

        public void SetTraveler(int level)
        {
            int levelDiff = level - playerStoredSettings.passiveStorage.traveler.level;

            for (int i = 0; i < levelDiff; i++)
            {
                playerStoredSettings.passiveStorage.traveler.espPower += playerStoredSettings.passiveStorage.traveler.espIncPerLevel;

            }

            playerStoredSettings.passiveStorage.traveler.level = level;
        }

        public void SetSurvivor(int level)
        {
            int levelDiff = level - playerStoredSettings.passiveStorage.survivor.level;

            for (int i = 0; i < levelDiff; i++)
            {
                playerStoredSettings.passiveStorage.survivor.sopPower += playerStoredSettings.passiveStorage.survivor.sopIncPerLevel;

            }

            playerStoredSettings.passiveStorage.survivor.level = level;
        }

        #region Old
        public void SetImpeto(int level)
        {
            int levelDiff = level - playerStoredSettings.activeStorage[0].level;

            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < levelDiff; i++)
                {
                    playerStoredSettings.activeStorage[j] = IncreaseActAbilityLevel(playerStoredSettings.activeStorage[j]);
                }

            }
        }

        public void SetRespiroDelVento(int level)
        {
            int levelDiff = level - playerStoredSettings.activeStorage[3].level;

            for (int j = 3; j < 6; j++)
            {
                for (int i = 0; i < levelDiff; i++)
                {
                    playerStoredSettings.activeStorage[j] = IncreaseActAbilityLevel(playerStoredSettings.activeStorage[j]);

                }
            }
        }
        #endregion

        public void SetPrimSecondPhase1(int level, originalName abilityName)
        {
            actPlayerAbility currentlevel = playerStoredSettings.activeStorage.Find(x => x.oname == abilityName);

            int levelDiff = level - currentlevel.level;

            for (int j = 0; j < playerStoredSettings.activeStorage.Count; j++)
            {
                if (playerStoredSettings.activeStorage[j].oname == abilityName)
                {
                    for (int i = 0; i < levelDiff; i++)
                    {
                        playerStoredSettings.activeStorage[j] = IncreaseActAbilityLevel(playerStoredSettings.activeStorage[j]);
                    }
                }

            }
        }


        public void SetActivePrim(originalName abilityName, weaponType weaponName)
        {
            actPlayerAbility abilityModified = playerStoredSettings.activeStorage.Find(x => x.oname == abilityName && x.weapon == weaponName);
            int abilityIndex = playerStoredSettings.activeStorage.FindIndex(x => x.oname == abilityName && x.weapon == weaponName);

            abilityModified.active = true;
            abilityModified.discovered = true;
            playerStoredSettings.activeStorage[abilityIndex] = abilityModified;

            for (int i = 0; i < playerStoredSettings.activeStorage.Count; i++)
            {
                if (i != abilityIndex && playerStoredSettings.activeStorage[i].category == type.Primary)
                {
                    abilityModified = playerStoredSettings.activeStorage[i];
                    abilityModified.active = false;

                    if (abilityName == playerStoredSettings.activeStorage[i].oname)
                        abilityModified.discovered = true;

                    playerStoredSettings.activeStorage[i] = abilityModified;
                }
            }
        }

        public void SetActiveSec(originalName abilityName, weaponType weaponName)
        {
            actPlayerAbility abilityModified = playerStoredSettings.activeStorage.Find(x => x.oname == abilityName && x.weapon == weaponName);
            int abilityIndex = playerStoredSettings.activeStorage.FindIndex(x => x.oname == abilityName && x.weapon == weaponName);

            abilityModified.active = true;
            abilityModified.discovered = true;
            playerStoredSettings.activeStorage[abilityIndex] = abilityModified;

            for (int i = 0; i < playerStoredSettings.activeStorage.Count; i++)
            {
                if (i != abilityIndex && playerStoredSettings.activeStorage[i].category == type.Secondary)
                {
                    abilityModified = playerStoredSettings.activeStorage[i];
                    abilityModified.active = false;

                    if (abilityName == playerStoredSettings.activeStorage[i].oname)
                        abilityModified.discovered = true;

                    playerStoredSettings.activeStorage[i] = abilityModified;
                }

            }
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

        public void OptionSettingsSaving()
        {
            Ui_Manager.instance.TakingOptionSettings(out difficultySet, out audioIsOn, out audioVolumeSet);
        }

        #endregion





    }
}