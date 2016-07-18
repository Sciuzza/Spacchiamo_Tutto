using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Spacchiamo
{
    public class Enemies_Manager : MonoBehaviour
    {

        List<GameObject> enemyReferences = new List<GameObject>();
        

        


        [HideInInspector]
        public static Enemies_Manager instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        

        void Update()
        {
            // Necessary to understand when switch to player turn again
            if (Game_Controller.instance.currentPhase == GAME_PHASE.npcEnemyTurn)
            {
                if (AreEnemiesInPosition())
                    Game_Controller.instance.ChangePhase(Game_Controller.instance.currentPhase);
            }
            if (Game_Controller.instance.currentPhase == GAME_PHASE.animation && Game_Controller.instance.previousPhase == GAME_PHASE.playerTurn)
            {
                if (AreEnemiesInPositionByKnockBack())
                {
                    Game_Controller.instance.previousPhase = GAME_PHASE.animation;
                    Game_Controller.instance.currentPhase = GAME_PHASE.npcEnemyTurn;
                }
            }
        }

        // Depends on moveDone boolean of each enemy 
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

        public void CheckingAggro()
        {
            for (int i = 0; i < enemyReferences.Count; i++)
            {
                if (Grid_Manager.instance.IsEnemyInAggroCell(enemyReferences[i].GetComponent<EnemyAI>().GettingXEnemy(), enemyReferences[i].GetComponent<EnemyAI>().GettingYEnemy()))
                {
                    if (!enemyReferences[i].GetComponent<Enemy_Controller>().isIgnoringAggro)
                    {
                        enemyReferences[i].GetComponent<Enemy_Controller>().isAggroed = true;
                        enemyReferences[i].GetComponent<Enemy_Controller>().isComingBack = false;
                    }
                }

                if (enemyReferences[i].GetComponent<Enemy_Controller>().isIgnoringAggro)
                {
                    if (enemyReferences[i].GetComponent<Enemy_Controller>().aggroIgnoringCounter == 2)
                        enemyReferences[i].GetComponent<Enemy_Controller>().isIgnoringAggro = false;
                    else
                        enemyReferences[i].GetComponent<Enemy_Controller>().aggroIgnoringCounter++;
                }
            }
        }

        public void ClearAggro()
        {
            for (int i = 0; i < enemyReferences.Count; i++)
            {
                if (enemyReferences[i].GetComponent<Enemy_Controller>().isAggroed)
                {
                    enemyReferences[i].GetComponent<Enemy_Controller>().isAggroed = false;
                    enemyReferences[i].GetComponent<Enemy_Controller>().isComingBack = true;
                    enemyReferences[i].GetComponent<Enemy_Controller>().isIgnoringAggro = true;
                }
            }
        }

        public void AttackingEnemies(List<Cell_Interaction> cellsInvolved, float damage, int knockback)
        {
            int x, y;
            GameObject enemyAttacked;

            for (int i = 0; i < cellsInvolved.Count; i++) {

                x = cellsInvolved[i].xCell;
                y = cellsInvolved[i].yCell;

                enemyAttacked = enemyReferences.Find(z => z.GetComponent<EnemyAI>().xEnemy == x && z.GetComponent<EnemyAI>().yEnemy == y);

                if (enemyAttacked != null)
                    enemyAttacked.GetComponent<Enemy_Controller>().TakingPlayerAbilityEffects(damage, knockback);
              }

        }

        public void DestroyEnemy(int xEnemy, int yEnemy)
        {
            GameObject enemyToDestroy = enemyReferences.Find(x => x.transform.position == Grid_Manager.instance.GetCellTransform(xEnemy, yEnemy).position - new Vector3(0, 0, 1));
            enemyReferences.Remove(enemyToDestroy);
            Grid_Manager.instance.SwitchingOccupiedStatus(xEnemy, yEnemy);
            Destroy(enemyToDestroy);
        }

        

        public void PatrolArea()
        {
            for (int i = 0; i < enemyReferences.Count; i++)
            {
                EnemyAI patrolLink = enemyReferences[i].GetComponent<EnemyAI>();
                patrolLink.InitalizingPatrolArea(Grid_Manager.instance.FindingPatrolArea(patrolLink.GettingXEnemy(), patrolLink.GettingYEnemy()));

            }
        }


        public void SettingOccupiedInitialStatus()
        {
            EnemyAI enemyPosition;

            for (int i = 0; i < enemyReferences.Count; i++)
            {
                enemyPosition = enemyReferences[i].GetComponent<EnemyAI>();
                Grid_Manager.instance.SwitchingOccupiedStatus(enemyPosition.GettingXEnemy(), enemyPosition.GettingYEnemy());
            }

        }

        public void PassingEnemyList(GameObject[] enemies)
        {
            enemyReferences.AddRange(enemies);
        }


    }
}

 
           