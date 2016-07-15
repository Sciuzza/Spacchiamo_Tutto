using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Spacchiamo
{

    public enum type { Primary, Secondary};
    public enum originalName {Impeto, RespiroDelVento};
    public enum weaponType {ArmaBianca, Catalizzatore, ArmaRanged};

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
    }

    [System.Serializable]
    public struct regAbility
    {
        public const string name = "Rigenerazione";
        public int level;
        public int maxLevel;
        public float regPower;
        public float rpIncPerLevel;
        public int cooldown;
        public int cooldownDecPerLevel;
    }

    public enum GAME_PHASE : byte { init, playerTurn, npcEnemyTurn };
    


    public class Game_Controller : MonoBehaviour
    {


        // Game Phases
        public GAME_PHASE currentPhase = GAME_PHASE.playerTurn;

        GameObject[] enemyArray;
        GameObject[] faloList;

        List<actPlayerAbility> playerAbilities = new List<actPlayerAbility>();


        

        // Camera and Player References
        Camera_Movement cameraLink;

        [HideInInspector]
        public GameObject playerLink;

        [HideInInspector]
        public static Game_Controller instance = null;


        void Awake()
        {

            #region SingleTone

            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(this.gameObject);
            
            #endregion



        }

        void Start()
        {

            playerLink = GameObject.FindGameObjectWithTag("Player");
            cameraLink = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Movement>();
            faloList = GameObject.FindGameObjectsWithTag("Falo");
            enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
            

            // Grid Initialization
            Grid_Manager.instance.PreparingOptimizedGridSpace();
            Grid_Manager.instance.LinkingFaloMechanic(faloList);

            //Initializing Light
            Grid_Manager.instance.GettingLight(playerLink.GetComponent<playerActions>().GettingXPlayer(), playerLink.GetComponent<playerActions>().GettingyPlayer());

            

            playerActions playerPosition = playerLink.GetComponent<playerActions>();
            Grid_Manager.instance.SwitchingOccupiedStatus(playerPosition.GettingXPlayer(), playerPosition.GettingyPlayer());



         

            //Initializing Camera on Player
            cameraLink.target = playerLink;

            // Initializing abilities for testing purpose
            if (playerAbilities.Count == 0)
                InitializingRandomPlayerAbilities();

            //Initializing Ability List on Player
            playerLink.GetComponent<Player_Controller>().Abilities = playerAbilities;

            Enemies_Manager.instance.PassingEnemyList(enemyArray);

            //Initialing Occupied Status for Enemy and Player
            Enemies_Manager.instance.SettingOccupiedInitialStatus();

            //Initializing PatrolArea
             Enemies_Manager.instance.PatrolArea();

        }

        public void ChangePhase(GAME_PHASE passedPhase)
        {
            Enemies_Manager.instance.CheckingAggro();

            switch (passedPhase)
            {
                case GAME_PHASE.playerTurn:
                    currentPhase = GAME_PHASE.npcEnemyTurn;
                    break;
                case GAME_PHASE.npcEnemyTurn:
                    currentPhase = GAME_PHASE.playerTurn;
                    break;
                default:
                    break;
            }

        }


        public GameObject TakingPlayerRef()
        {
            return playerLink;
        }

        private void InitializingRandomPlayerAbilities()
        {
            actPlayerAbility currentAbility1 = new actPlayerAbility();

            currentAbility1.damage = 1;
            currentAbility1.cooldown = 1;
            currentAbility1.range = 1;


            actPlayerAbility currentAbility2 = new actPlayerAbility();

            currentAbility2.damage = 1;
            currentAbility2.cooldown = 1;
            currentAbility2.range = 3;

            playerAbilities.Add(currentAbility1);
            playerAbilities.Add(currentAbility2);

        }
    }
}