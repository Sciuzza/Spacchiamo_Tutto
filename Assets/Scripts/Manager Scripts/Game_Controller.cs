using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Spacchiamo
{

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

 
    public enum GAME_PHASE : byte { init, playerTurn, npcEnemyTurn };



    public class Game_Controller : MonoBehaviour
    {

        

        // obvious LOL
        public GAME_PHASE currentPhase = GAME_PHASE.playerTurn;

        // to be passed to Enemy Manager
        GameObject[] enemyArray;

        // to be passed to Grid Manager
        GameObject[] faloList;
        
        // to be passed to Player Controller
        List<actPlayerAbility> playerAbilities = new List<actPlayerAbility>();
        regAbility playerRegAbility = new regAbility();

        // to give Camera the player as target
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
            // Finding the necessary References to start the initialization sequence
            playerLink = GameObject.FindGameObjectWithTag("Player");
            cameraLink = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Movement>();
            faloList = GameObject.FindGameObjectsWithTag("Falo");
            enemyArray = GameObject.FindGameObjectsWithTag("Enemy");


            // Grid Initialization
            Grid_Manager.instance.GivingPlayerRef(playerLink);
            Grid_Manager.instance.PreparingOptimizedGridSpace();
            Grid_Manager.instance.LinkingFaloMechanic(faloList);

            //Player Initialization
            Grid_Manager.instance.GettingLight(playerLink.GetComponent<playerActions>().GettingXPlayer(), playerLink.GetComponent<playerActions>().GettingyPlayer());
            playerActions playerPosition = playerLink.GetComponent<playerActions>();
            Grid_Manager.instance.SwitchingOccupiedStatus(playerPosition.GettingXPlayer(), playerPosition.GettingyPlayer());
            cameraLink.target = playerLink;

            // Player Active Ability transfer conditions 
            if (playerAbilities.Count == 0)
            {
                TakingDesActAbilities();
                playerLink.GetComponent<Player_Controller>().actAbilities = playerAbilities;
                
            }
            else
                playerLink.GetComponent<Player_Controller>().Abilities.AddRange(playerAbilities);



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

            // Enemies Initialization
            Enemies_Manager.instance.PassingEnemyList(enemyArray);
            Enemies_Manager.instance.SettingOccupiedInitialStatus();
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

    }
}