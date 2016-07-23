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
        public bool discovered;
        public bool active;
    }

    [System.Serializable]
    public struct passAbilities
    {
        public regAbility regeneration;
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


    public enum GAME_PHASE : byte { init, playerTurn, npcEnemyTurn, animation, dialogue};



    public class Game_Controller : MonoBehaviour
    {

        public int expPlayerGained = 0;
        public int unspentPlayerAbilityPoints = 0;
        public int playerLevel = 0;

        // obvious LOL
        public GAME_PHASE currentPhase = GAME_PHASE.init;
        public GAME_PHASE previousPhase = GAME_PHASE.playerTurn;

        // to be passed to Enemy Manager
        GameObject[] enemy1Array, enemy2Array, enemy3Array, enemy4Array, enemy5Array, enemy6Array, enemy7Array;

        // to be passed to Grid Manager
        GameObject[] faloList;

        //Ui References to be Passed
        Text fear, turnCount;
        Slider fearBar;
        UILifePanelScript lifePanelScript;

        // to be passed to Player Controller
        List<actPlayerAbility> playerAbilities = new List<actPlayerAbility>();
        regAbility playerRegAbility = new regAbility();

        // to give Camera the player as target
        Camera_Movement cameraLink;

        [HideInInspector]
        public GameObject playerLink;

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

            #endregion



        }

        void Start()
        {
            #region Taking All References
            // Finding the necessary References to start the initialization sequence
            playerLink = GameObject.FindGameObjectWithTag("Player");
            cameraLink = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Movement>();
            faloList = GameObject.FindGameObjectsWithTag("Falo");
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

            //Ui initialization
            Ui_Manager.instance.TakingReferences(fear, turnCount, fearBar, lifePanelScript);
            Ui_Manager.instance.UiInitialization();

            //Scene Manager initilization
            Scene_Manager.instance.SceneManagerInitialization();

            #region Grid Initialization
            // Grid Initialization
            Grid_Manager.instance.GivingPlayerRef(playerLink);
            Grid_Manager.instance.PreparingOptimizedGridSpace();
            Grid_Manager.instance.LinkingFaloMechanic(faloList); 
            #endregion

            #region Player Scene Initialization
            //Player Initialization
            Grid_Manager.instance.GettingLight(playerLink.GetComponent<playerActions>().GettingXPlayer(), playerLink.GetComponent<playerActions>().GettingyPlayer());
            playerActions playerPosition = playerLink.GetComponent<playerActions>();
            Grid_Manager.instance.SwitchingOccupiedStatus(playerPosition.GettingXPlayer(), playerPosition.GettingyPlayer());
            playerPosition.whereToGo = Grid_Manager.instance.GetCellTransform(playerPosition.GettingXPlayer(), playerPosition.GettingyPlayer());
            cameraLink.transform.position = playerLink.transform.position - new Vector3(0, 0, 10);
            cameraLink.target = playerLink;

            // Player Active Ability transfer conditions 
            if (playerAbilities.Count == 0)
            {
                TakingDesActAbilities();
                playerLink.GetComponent<Player_Controller>().actAbilities = playerAbilities;

            }
            else
                playerLink.GetComponent<Player_Controller>().Abilities.AddRange(playerAbilities);

            playerLink.GetComponent<Player_Controller>().expGained = expPlayerGained;
            playerLink.GetComponent<Player_Controller>().unspentAbilityPoints = unspentPlayerAbilityPoints;
            playerLink.GetComponent<Player_Controller>().playerLevel = playerLevel;

            // Player Passive Ability transfer conditions 

            pOriginalName searchingPassive = pOriginalName.NotFound;
            searchingPassive = CheckingCurrentPassive();

            if (searchingPassive == pOriginalName.Rigenerazione)
            {
                playerLink.GetComponent<Player_Controller>().RegPassive = playerRegAbility;
            }
            else
            {
                if (Designer_Tweaks.instance.passiveTesting == pOriginalName.Rigenerazione)
                {
                    playerRegAbility = AbiRepository.instance.PassRepostr.regeneration;
                    playerRegAbility.active = true;
                    playerRegAbility.discovered = true;
                    for (int i = 1; i < Designer_Tweaks.instance.passiveLevel; i++)
                        playerRegAbility = IncreaseRegLevel(playerRegAbility);
                    playerLink.GetComponent<Player_Controller>().RegPassive = playerRegAbility;
                }
            }
            #endregion

            #region Enemy Scene Initialization
            // Enemies Initialization
            AbiRepository.instance.SetEnemyLevels(1, 1, 1, 1, 1);
            Enemies_Manager.instance.SetEnemyManagerStructs();

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
           

            currentPhase = GAME_PHASE.playerTurn; 
            #endregion

        }


        void OnLevelWasLoaded(int level)
        {
            #region Taking All References
            // Finding the necessary References to start the initialization sequence
            playerLink = GameObject.FindGameObjectWithTag("Player");
            cameraLink = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Movement>();
            faloList = GameObject.FindGameObjectsWithTag("Falo");
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

            //Ui initialization
            Ui_Manager.instance.TakingReferences(fear, turnCount, fearBar, lifePanelScript);
            Ui_Manager.instance.UiInitialization();

            //Scene Manager initilization
            Scene_Manager.instance.SceneManagerInitialization();

            #region Grid Initialization
            // Grid Initialization
            Grid_Manager.instance.GivingPlayerRef(playerLink);
            Grid_Manager.instance.PreparingOptimizedGridSpace();
            Grid_Manager.instance.LinkingFaloMechanic(faloList);
            #endregion

            #region Player Scene Initialization
            //Player Initialization
            Grid_Manager.instance.GettingLight(playerLink.GetComponent<playerActions>().GettingXPlayer(), playerLink.GetComponent<playerActions>().GettingyPlayer());
            playerActions playerPosition = playerLink.GetComponent<playerActions>();
            Grid_Manager.instance.SwitchingOccupiedStatus(playerPosition.GettingXPlayer(), playerPosition.GettingyPlayer());
            playerPosition.whereToGo = Grid_Manager.instance.GetCellTransform(playerPosition.GettingXPlayer(), playerPosition.GettingyPlayer());
            cameraLink.transform.position = playerLink.transform.position - new Vector3(0, 0, 10);
            cameraLink.target = playerLink;

            // Player Active Ability transfer conditions 
            if (playerAbilities.Count == 0)
            {
                TakingDesActAbilities();
                playerLink.GetComponent<Player_Controller>().actAbilities = playerAbilities;

            }
            else
                playerLink.GetComponent<Player_Controller>().Abilities.AddRange(playerAbilities);


            playerLink.GetComponent<Player_Controller>().expGained = expPlayerGained;
            playerLink.GetComponent<Player_Controller>().unspentAbilityPoints = unspentPlayerAbilityPoints;
            playerLink.GetComponent<Player_Controller>().playerLevel = playerLevel;

            // Player Passive Ability transfer conditions 

            pOriginalName searchingPassive = pOriginalName.NotFound;
            searchingPassive = CheckingCurrentPassive();

            if (searchingPassive == pOriginalName.Rigenerazione)
            {
                playerLink.GetComponent<Player_Controller>().RegPassive = playerRegAbility;
            }
            else
            {
                if (Designer_Tweaks.instance.passiveTesting == pOriginalName.Rigenerazione)
                {
                    playerRegAbility = AbiRepository.instance.PassRepostr.regeneration;
                    playerRegAbility.active = true;
                    playerRegAbility.discovered = true;
                    for (int i = 1; i < Designer_Tweaks.instance.passiveLevel; i++)
                        playerRegAbility = IncreaseRegLevel(playerRegAbility);
                    playerLink.GetComponent<Player_Controller>().RegPassive = playerRegAbility;
                }
            }
            #endregion

            #region Enemy Scene Initialization
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


            currentPhase = GAME_PHASE.playerTurn;
            #endregion

        }

        public GameObject TakingPlayerRef()
        {
            return playerLink;
        }


        #region Player Initialization Methods

        private void TakingDesActAbilities()
        {
            actPlayerAbility currentAbility1 = new actPlayerAbility();

            currentAbility1 = AbiRepository.instance.ARepository.Find(x => x.oname == Designer_Tweaks.instance.primaryTesting && x.weapon == Designer_Tweaks.instance.primaryWeapon);
            currentAbility1.active = true;
            currentAbility1.discovered = true;

            for (int i = 1; i < Designer_Tweaks.instance.primaryLevel; i++)
                currentAbility1 = IncreaseActAbilityLevel(currentAbility1);



            actPlayerAbility currentAbility2 = new actPlayerAbility();

            currentAbility2 = AbiRepository.instance.ARepository.Find(x => x.oname == Designer_Tweaks.instance.seconTesting && x.weapon == Designer_Tweaks.instance.seconWeapon);
            currentAbility2.active = true;
            currentAbility2.discovered = true;

            for (int i = 1; i < Designer_Tweaks.instance.seconLevel; i++)
                currentAbility2 = IncreaseActAbilityLevel(currentAbility2);



            playerAbilities.Add(currentAbility1);
            playerAbilities.Add(currentAbility2);

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

        private pOriginalName CheckingCurrentPassive()
        {
            if (playerRegAbility.active)
                return pOriginalName.Rigenerazione;
            else
                return pOriginalName.NotFound;
        }

        public regAbility IncreaseRegLevel(regAbility increase)
        {

            increase.level++;
            increase.regPower += increase.rpIncPerLevel;
            increase.cooldown -= increase.cooldownDecPerLevel;


            return increase;
        } 
        #endregion

    }
}