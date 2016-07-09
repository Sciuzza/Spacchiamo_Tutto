using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Spacchiamo
{
    public class Enemies_Manager : MonoBehaviour
    {

        List<GameObject> enemyReferences = new List<GameObject>();
        GameObject enemyTemp;

        [HideInInspector]
        public static Enemies_Manager instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
       

        }

        // Update is called once per frame
        void Update()
        {
            if (Game_Controller.instance.currentPhase == Game_Controller.GAME_PHASE.npcEnemyTurn)
            {
                if (AreEnemiesInPosition())
                    Game_Controller.instance.ChangePhase(Game_Controller.instance.currentPhase);
            }
        }

        public void PreparingEnemies()
        {
            for (int i = 1; i <= Designer_Tweaks.instance.level1EnemiesQuantity; i++)
            {
                switch (Random.Range(1, 4))
                {
                    case 1:
                        enemyTemp = Resources.Load<GameObject>("Enemy1");
                        enemyTemp = Instantiate(enemyTemp);
                        enemyReferences.Add(enemyTemp);
                        break;
                    case 2:
                        enemyTemp = Resources.Load<GameObject>("Enemy2");
                        enemyTemp = Instantiate(enemyTemp);
                        enemyReferences.Add(enemyTemp);
                        break;
                    default:
                        enemyTemp = Resources.Load<GameObject>("Enemy3");
                        enemyTemp = Instantiate(enemyTemp);
                        enemyReferences.Add(enemyTemp);
                        break;
                }



            }
        }

        private bool AreEnemiesInPosition()
        {
            bool inPosition = true;

            for (int i = 0; i < enemyReferences.Count && inPosition; i++)
            {              
                    if (!enemyReferences[i].GetComponent<Enemy_Patrolling>().move_done)
                        inPosition = false;
                
            }

            return inPosition;
        }


        public void CheckingAggro()
        {
            for (int i = 0; i < enemyReferences.Count; i++)
            {
                if (Grid_Manager.instance.IsEnemyInAggroCell(enemyReferences[i].GetComponent<Enemy_Patrolling>().GettingRow(), enemyReferences[i].GetComponent<Enemy_Patrolling>().GettingColumn()))
                    enemyReferences[i].GetComponent<Enemy_Controller>().isAggroed = true;
            }
        }
    }
}
