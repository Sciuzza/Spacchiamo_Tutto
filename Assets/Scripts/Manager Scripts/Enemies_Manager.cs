﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Spacchiamo
{
    public class Enemies_Manager : MonoBehaviour
    {

        List<GameObject> enemyReferences = new List<GameObject>();

        public enemySetting enemy1 = new enemySetting();
        public enemySetting enemy2 = new enemySetting();
        public enemySetting enemy3 = new enemySetting();
        public enemySetting enemy4 = new enemySetting();
        public enemySetting enemy5 = new enemySetting();
        public enemySetting enemy6 = new enemySetting();
        public enemySetting enemy7 = new enemySetting();

        Sprite ghost;

        private GameObject playerTemp;
        public bool fearPhaseChecked;
        public float spawnChance;

        #region SingleTone

        [HideInInspector]
        public static Enemies_Manager instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            ghost = Resources.Load<Sprite>("semiVis");
        }
        #endregion


        #region Phase Condition Methods
        void Update()
        {
            if (Scene_Manager.instance.GetCurrentSceneIndex() >= 6)
            {
                // Necessary to understand when switch to player turn again
                if (Game_Controller.instance.currentPhase == GAME_PHASE.npcEnemyTurn)
                {
                    if (AreEnemiesInPosition())
                    {
                        CheckingAggro();
                        fearPhaseChecked = false;
                        Game_Controller.instance.currentPhase = GAME_PHASE.playerTurn;
                    }
                }
                if (Game_Controller.instance.currentPhase == GAME_PHASE.knockAni && Game_Controller.instance.previousPhase == GAME_PHASE.playerTurn)
                {
                    if (AreEnemiesInPositionByKnockBack())
                    {
                        Game_Controller.instance.previousPhase = GAME_PHASE.knockAni;
                        Game_Controller.instance.currentPhase = GAME_PHASE.npcEnemyTurn;
                    }
                }
                if (Game_Controller.instance.currentPhase == GAME_PHASE.playerTurn && !fearPhaseChecked)
                {
                    spawnChance = Random.Range(0f, 1f);

                    if (playerTemp.GetComponent<Player_Controller>().CurSet.fear1Activated)
                    {
                        if (spawnChance <= playerTemp.GetComponent<Player_Controller>().CurSet.fear1Percent)
                            SpawnFear1Monster();
                    }
                    else if (playerTemp.GetComponent<Player_Controller>().CurSet.fear2Activated)
                    {
                        if (spawnChance <= playerTemp.GetComponent<Player_Controller>().CurSet.fear2Percent)
                            SpawnFear2Monster();
                    }
                    fearPhaseChecked = true;
                }

            }
        }

        private bool AreEnemiesInPosition()
        {
            bool inPosition = true;

            for (int i = 0; i < enemyReferences.Count && inPosition; i++)
            {
                if (!enemyReferences[i].GetComponent<EnemyAI>().move_done)
                    inPosition = false;

            }

            return inPosition;
        }

        private bool AreEnemiesInPositionByKnockBack()
        {
            bool inPosition = true;

            for (int i = 0; i < enemyReferences.Count && inPosition; i++)
            {
                if (enemyReferences[i].GetComponent<EnemyAI>().isKnockBacked)
                    inPosition = false;

            }

            return inPosition;
        }
        #endregion


        #region Aggro Methods

        public void CheckingAggro()
        {
            int xEnemy, yEnemy, xPlayer, yPlayer;

            Grid_Manager.instance.RetrievePlayerCoords(out xPlayer, out yPlayer);

            for (int i = 0; i < enemyReferences.Count; i++)
            {
                xEnemy = enemyReferences[i].GetComponent<EnemyAI>().xEnemy;
                yEnemy = enemyReferences[i].GetComponent<EnemyAI>().yEnemy;

                if (Grid_Manager.instance.IsCellReceivingLight(xPlayer, yPlayer) && enemyReferences[i].GetComponent<Enemy_Controller>().isAggroed)
                {

                    enemyReferences[i].GetComponent<Enemy_Controller>().isAggroed = false;
                    enemyReferences[i].GetComponent<Enemy_Controller>().SetAggroInvisible();

                    if (enemyReferences[i].GetComponent<Enemy_Controller>().enemyCurrentSetting.behaviour == behaviour.kamikaze)
                    {
                        enemyReferences[i].GetComponent<Animator>().SetBool("Aggro", false);
                    }


                    if (enemyReferences[i].GetComponent<Enemy_Controller>().enemyCurrentSetting.aggroStyle == aggroStyle.following)
                    {
                        enemyReferences[i].GetComponent<Enemy_Controller>().isComingBack = true;
                        enemyReferences[i].GetComponent<Enemy_Controller>().isIgnoringAggro = true;
                        enemyReferences[i].GetComponent<Enemy_Controller>().aggroIgnoringCounter = 0;
                    }

                }
                else if (!Grid_Manager.instance.IsCellReceivingLight(xPlayer, yPlayer) && !enemyReferences[i].GetComponent<Enemy_Controller>().isAggroed 
                    && !enemyReferences[i].GetComponent<Enemy_Controller>().isIgnoringAggro 
                    && Grid_Manager.instance.RetrieveManhDistfromAtoB(xEnemy, yEnemy, xPlayer, yPlayer) <= enemyReferences[i].GetComponent<Enemy_Controller>().enemyCurrentSetting.aggroRange)
                {
                    enemyReferences[i].GetComponent<Enemy_Controller>().isAggroed = true;
                    enemyReferences[i].GetComponent<Enemy_Controller>().SetAggroVisible();
                    enemyReferences[i].GetComponent<Enemy_Controller>().isComingBack = false;

                    if (enemyReferences[i].GetComponent<Enemy_Controller>().enemyCurrentSetting.behaviour == behaviour.kamikaze)
                    {
                        enemyReferences[i].GetComponent<Animator>().SetBool("Aggro", true);
                    }

                }
                
            }
        }

      
        #endregion


        #region Fighting Methods
        public void AttackingEnemies(List<Cell_Interaction> cellsInvolved, float damage, int knockback)
        {
            int x, y;
            GameObject enemyAttacked;

            for (int i = 0; i < cellsInvolved.Count; i++)
            {

                x = cellsInvolved[i].xCell;
                y = cellsInvolved[i].yCell;

                enemyAttacked = enemyReferences.Find(z => z.GetComponent<EnemyAI>().xEnemy == x && z.GetComponent<EnemyAI>().yEnemy == y);

                if (enemyAttacked != null)
                    enemyAttacked.GetComponent<Enemy_Controller>().TakingPlayerAbilityEffects(damage, knockback);
            }

        }

        public void DestroyEnemy(int xEnemy, int yEnemy)
        {
            //GameObject enemyToDestroy = enemyReferences.Find(x => x.transform.position == Grid_Manager.instance.GetCellTransform(xEnemy, yEnemy).position - new Vector3(0, 0, 1));
            int enemyToDestroy = enemyReferences.FindIndex(z => z.GetComponent<EnemyAI>().xEnemy == xEnemy && z.GetComponent<EnemyAI>().yEnemy == yEnemy);
            GameObject enemy = enemyReferences[enemyToDestroy];
            enemyReferences.RemoveAt(enemyToDestroy);
            Grid_Manager.instance.RemovingAtIndexAStarCells(enemyToDestroy);
            Grid_Manager.instance.SwitchingOccupiedStatus(xEnemy, yEnemy);
            playerTemp.GetComponent<Player_Controller>().GainingExp(enemy.GetComponent<Enemy_Controller>().enemyCurrentSetting.experience);
            playerTemp.GetComponent<Player_Controller>().CheckingCurrentLevel();
            Destroy(enemy);
        }
        #endregion


        #region Initilization Methods

        public void SetEnemyManagerStructs()
        {
            enemy1 = AbiRepository.instance.enemyRepo[0];
            enemy2 = AbiRepository.instance.enemyRepo[1];
            enemy3 = AbiRepository.instance.enemyRepo[2];
            enemy4 = AbiRepository.instance.enemyRepo[3];
            enemy5 = AbiRepository.instance.enemyRepo[4];
            enemy6 = AbiRepository.instance.enemyRepo[5];
            enemy7 = AbiRepository.instance.enemyRepo[6];
        }

        public void ClearEnemyReferences()
        {
            enemyReferences.Clear();
            enemyReferences.TrimExcess();
        }

        public void PassingEnemyList(GameObject[] enemies)
        {
            enemyReferences.AddRange(enemies);

            for (int i = 0; i < enemyReferences.Count; i++)
            {
                if (enemyReferences[i].tag == "Enemy1")
                    enemyReferences[i].GetComponent<Enemy_Controller>().InitializeEnemyController(enemy1);
                else if (enemyReferences[i].tag == "Enemy2")
                    enemyReferences[i].GetComponent<Enemy_Controller>().InitializeEnemyController(enemy2);
                else if (enemyReferences[i].tag == "Enemy3")
                    enemyReferences[i].GetComponent<Enemy_Controller>().InitializeEnemyController(enemy3);
                else if (enemyReferences[i].tag == "Enemy4")
                    enemyReferences[i].GetComponent<Enemy_Controller>().InitializeEnemyController(enemy4);
                else if (enemyReferences[i].tag == "Enemy5")
                    enemyReferences[i].GetComponent<Enemy_Controller>().InitializeEnemyController(enemy5);
                else if (enemyReferences[i].tag == "Enemy6")
                    enemyReferences[i].GetComponent<Enemy_Controller>().InitializeEnemyController(enemy6);
                else if (enemyReferences[i].tag == "Enemy7")
                    enemyReferences[i].GetComponent<Enemy_Controller>().InitializeEnemyController(enemy7);
            }
        }

        public void GivingPlayerRef(GameObject player)
        {
            playerTemp = player;
        }

        #endregion

        #region Methods Needed for an enemy to be initialized
        public void ImplementingEachEnemySettings(int startIndex)
        {
            for (int i = startIndex; i < enemyReferences.Count; i++)
            {
                enemyReferences[i].GetComponent<Enemy_Controller>().InitializingOwnPatrol();
            }
        }

        public void SettingOccupiedInitialStatus(int startIndex)
        {
            EnemyAI enemyPosition;

            for (int i = startIndex; i < enemyReferences.Count; i++)
            {
                enemyPosition = enemyReferences[i].GetComponent<EnemyAI>();
                Grid_Manager.instance.SwitchingOccupiedStatus(enemyPosition.GettingXEnemy(), enemyPosition.GettingYEnemy());
            }

        }

        public void SettingEnemyVisibility(int startIndex)
        {
            int xEnemy, yEnemy;

            for (int i = startIndex; i < enemyReferences.Count; i++)
            {
                xEnemy = enemyReferences[i].GetComponent<EnemyAI>().xEnemy;
                yEnemy = enemyReferences[i].GetComponent<EnemyAI>().yEnemy;

                if (Grid_Manager.instance.GettingAlpha(xEnemy, yEnemy) <= 0.3f)
                {
                    enemyReferences[i].GetComponent<SpriteRenderer>().sprite = ghost;

                    if (enemyReferences[i].GetComponent<Enemy_Controller>().enemyCurrentSetting.behaviour == behaviour.kamikaze)
                        enemyReferences[i].GetComponent<Animator>().SetBool("Visible", false);
                    else if (enemyReferences[i].GetComponent<Animator>() != null)
                    {
                        enemyReferences[i].GetComponent<Animator>().SetBool("Visible", false);
                    }
                }
                else
                {
                    enemyReferences[i].GetComponent<SpriteRenderer>().sprite = enemyReferences[i].GetComponent<Enemy_Controller>().visible;

                    if (enemyReferences[i].GetComponent<Enemy_Controller>().enemyCurrentSetting.behaviour == behaviour.kamikaze)
                        enemyReferences[i].GetComponent<Animator>().SetBool("Visible", true);
                    else if (enemyReferences[i].GetComponent<Animator>() != null)
                    {
                        enemyReferences[i].GetComponent<Animator>().SetBool("Visible", true);
                    }
                }
            }
        }

        public void SettingSortingOrder(int startIndex)
        {
            for (int i = startIndex; i < enemyReferences.Count; i++)
                enemyReferences[i].GetComponent<SpriteRenderer>().sortingOrder = Designer_Tweaks.instance.Level1YWidth - enemyReferences[i].GetComponent<EnemyAI>().yEnemy;
        }

        public void InitializeWhereToGo(int startIndex)
        {
            for (int i = startIndex; i < enemyReferences.Count; i++)
                enemyReferences[i].GetComponent<EnemyAI>().whereToGo = Grid_Manager.instance.GetCellTransform(enemyReferences[i].GetComponent<EnemyAI>().xEnemy, enemyReferences[i].GetComponent<EnemyAI>().yEnemy);
        }

        public void InitilizeLifeFeedBack(int startIndex)
        {
            for (int i = startIndex; i < enemyReferences.Count; i++)
            {
                enemyReferences[i].GetComponent<Enemy_Controller>().SettingOwnLifeFeed();
            }
        }

        public void InitializeAggroFeedBack(int startIndex)
        {
            for (int i = startIndex; i < enemyReferences.Count; i++)
            {
                enemyReferences[i].GetComponent<Enemy_Controller>().SettingOwnAggroFeed();
            }
        } 
        #endregion

       









        private void SpawnFear1Monster()
        {
            

            int xToSpawn, yToSpawn, randomCell;
            GameObject specialObjects = GameObject.FindGameObjectWithTag("Special Objects");

            
            List<Cell_Interaction> possibleSpawnPos = Grid_Manager.instance.RetrievePossibleSpawnPos(playerTemp.GetComponent<playerActions>().xPlayer, playerTemp.GetComponent<playerActions>().yPlayer);
            GameObject enemySpawned = Resources.Load<GameObject>("TilePrefabs\\Enemy1");
            enemySpawned = Instantiate(enemySpawned);

            randomCell = (int)Random.Range(0, possibleSpawnPos.Count);

            xToSpawn = possibleSpawnPos[randomCell].xCell;
            yToSpawn = possibleSpawnPos[randomCell].yCell;

            enemySpawned.transform.position = new Vector3(xToSpawn + 0.5f, yToSpawn + 0.5f, 0);
            enemySpawned.transform.SetParent(specialObjects.transform);
            enemySpawned.name = "O-Enemy1(" + xToSpawn + "," + yToSpawn + ")";
            enemySpawned.tag = "Enemy1";
            enemySpawned.GetComponent<EnemyAI>().xEnemy = xToSpawn;
            enemySpawned.GetComponent<EnemyAI>().yEnemy = yToSpawn;
            enemySpawned.GetComponent<EnemyAI>().xComeBack = xToSpawn;
            enemySpawned.GetComponent<EnemyAI>().yComeBack = yToSpawn;


            enemyReferences.Add(enemySpawned);
            enemyReferences[enemyReferences.Count - 1].GetComponent<Enemy_Controller>().InitializeEnemyController(enemy1);
            ImplementingEachEnemySettings(enemyReferences.Count - 1);
            SettingOccupiedInitialStatus(enemyReferences.Count - 1);
            SettingEnemyVisibility(enemyReferences.Count - 1);
            SettingSortingOrder(enemyReferences.Count - 1);
            InitializeWhereToGo(enemyReferences.Count - 1);
            InitilizeLifeFeedBack(enemyReferences.Count - 1);
            InitializeAggroFeedBack(enemyReferences.Count - 1);

            //enemySpawned.GetComponent<EnemyAI>().whereToGo = possibleSpawnPos[randomCell].transform;
           // enemySpawned.GetComponent<SpriteRenderer>().sprite = ghost;

            
            
            //enemyReferences[enemyReferences.Count - 1].GetComponent<Enemy_Controller>().InitializingOwnPatrol();
            //Grid_Manager.instance.SwitchingOccupiedStatus(xToSpawn, yToSpawn);
            enemySpawned.GetComponent<Enemy_Controller>().isAggroed = true;
            enemySpawned.GetComponent<Enemy_Controller>().SetAggroVisible();

            Grid_Manager.instance.AddingElementsAStarCells(1);

        }

        private void SpawnFear2Monster()
        {
            
            int xToSpawn, yToSpawn, randomCell;
            GameObject specialObjects = GameObject.FindGameObjectWithTag("Special Objects");


            List<Cell_Interaction> possibleSpawnPos = Grid_Manager.instance.RetrievePossibleSpawnPos(playerTemp.GetComponent<playerActions>().xPlayer, playerTemp.GetComponent<playerActions>().yPlayer);
            GameObject enemySpawned = Resources.Load<GameObject>("TilePrefabs\\Enemy6");
            enemySpawned = Instantiate(enemySpawned);

            randomCell = (int)Random.Range(0, possibleSpawnPos.Count);

            xToSpawn = possibleSpawnPos[randomCell].xCell;
            yToSpawn = possibleSpawnPos[randomCell].yCell;

            enemySpawned.transform.position = new Vector3(xToSpawn + 0.5f, yToSpawn + 0.5f, 0);
            enemySpawned.transform.SetParent(specialObjects.transform);
            enemySpawned.name = "O-Enemy6(" + xToSpawn + "," + yToSpawn + ")";
            enemySpawned.tag = "Enemy6";
            enemySpawned.GetComponent<EnemyAI>().xEnemy = xToSpawn;
            enemySpawned.GetComponent<EnemyAI>().yEnemy = yToSpawn;
            enemySpawned.GetComponent<EnemyAI>().xComeBack = xToSpawn;
            enemySpawned.GetComponent<EnemyAI>().yComeBack = yToSpawn;

            enemyReferences.Add(enemySpawned);
            enemyReferences[enemyReferences.Count - 1].GetComponent<Enemy_Controller>().InitializeEnemyController(enemy1);
            ImplementingEachEnemySettings(enemyReferences.Count - 1);
            SettingOccupiedInitialStatus(enemyReferences.Count - 1);
            SettingEnemyVisibility(enemyReferences.Count - 1);
            SettingSortingOrder(enemyReferences.Count - 1);
            InitializeWhereToGo(enemyReferences.Count - 1);
            InitilizeLifeFeedBack(enemyReferences.Count - 1);
            InitializeAggroFeedBack(enemyReferences.Count - 1);

            //enemySpawned.GetComponent<EnemyAI>().whereToGo = possibleSpawnPos[randomCell].transform;
            // enemySpawned.GetComponent<SpriteRenderer>().sprite = ghost;



            //enemyReferences[enemyReferences.Count - 1].GetComponent<Enemy_Controller>().InitializingOwnPatrol();
            //Grid_Manager.instance.SwitchingOccupiedStatus(xToSpawn, yToSpawn);
            enemySpawned.GetComponent<Enemy_Controller>().isAggroed = true;
            enemySpawned.GetComponent<Enemy_Controller>().SetAggroVisible();

            Grid_Manager.instance.AddingElementsAStarCells(1);

        }

        public int RetrieveEnemiesNumber()
        {
            return enemyReferences.Count;
        }

        public int RetrieveOwnEnemyIndex(GameObject enemy)
        {
            return enemyReferences.FindIndex(z => z == enemy);
        }


    

    }
}


