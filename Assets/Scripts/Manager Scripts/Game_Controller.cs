using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Spacchiamo
{

    public enum type { Primary, Secondary, Passive };
    public enum originalName { Sventata, Impatto, Tempesta, RespiroDelVento, Maledizione, Rigenerazione, Lume, Alchimista };
    public enum weaponType { ArmaBianca, Catalizzatore, ArmaRanged };


    public struct ability
    {
        public type category;
        public originalName oname;
        public string customName;
        public weaponType weapon;
        public int level;
        public float damage;
        public int range;
        public int cooldown;
        public int areaEffect;
        public int knockBack;
        public float dot;
        public int stunTime;
    }


    public class Game_Controller : MonoBehaviour
    {


        // Game Phases

        public enum GAME_PHASE : byte { init, playerTurn, npcEnemyTurn };

        public GAME_PHASE currentPhase = GAME_PHASE.playerTurn;


        List<ability> playerAbilities = new List<ability>();
        List<ability> Enemy1Abilities = new List<ability>();
        List<ability> Enemy2Abilities = new List<ability>();
        List<ability> Enemy3Abilities = new List<ability>();



        // Camera and Player References
        Camera_Movement cameraLink;
        GameObject PlayerTemp;

        [HideInInspector]
        public static Game_Controller instance = null;


        void Awake()
        {

            #region SingleTone

            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            #endregion



        }

        void Start()
        {

            //Getting Camera Reference 
            cameraLink = GameObject.Find("Main Camera").GetComponent<Camera_Movement>();

            //Initalizing Level Grid Space, needs to be scene based
            //Grid_Manager.instance.PreparingGridSpace();

            Grid_Manager.instance.PreparingOptimizedGridSpace();


            //Initializing Light
            Grid_Manager.instance.GettingLight(PlayerTemp.GetComponent<PMovement>().GettingXPlayer(), PlayerTemp.GetComponent<PMovement>().GettingyPlayer());

            //Initializing PatrolArea
            Enemies_Manager.instance.PatrolArea();

            //Initializing Camera on Player
            cameraLink.target = PlayerTemp;

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

        public void GivingPlayerRef(GameObject Player)
        {
            PlayerTemp = Player;
        }

        public GameObject TakingPlayerRef()
        {
            return PlayerTemp;
        }
    }
}