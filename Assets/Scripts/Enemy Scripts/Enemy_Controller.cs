using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Spacchiamo
{
    public class Enemy_Controller : MonoBehaviour
    {
        #region Old Settings System

        

        public List<actEnemyAbility> actAbilities = new List<actEnemyAbility>();
        #endregion



        #region New Settings System

        public bool isAggroed = false;
        public bool isComingBack = false;
        public bool isIgnoringAggro = false;
        public int aggroIgnoringCounter = 0;



        public enemySetting enemyCurrentSetting = new enemySetting(); 
        #endregion


        #region GettingAiLink

        EnemyAI aiLink;

        void Awake()
        {
            aiLink = this.GetComponent<EnemyAI>();
        }
        #endregion




        #region Taking Player Abilities Effects
        public void TakingPlayerAbilityEffects(float damageTaken, int knockBackTaken)
        {
            isAggroed = true;
            enemyCurrentSetting.life -= damageTaken;

            if (enemyCurrentSetting.life <= 0)
                Enemies_Manager.instance.DestroyEnemy(aiLink.xEnemy, aiLink.yEnemy);
            else
            {

                if (knockBackTaken >= 1)
                    TakingKnockBack(knockBackTaken);
            }

        }

        private void TakingKnockBack(int knockBackTaken)
        {
            int relativePos;
            int i;
            relativePos = Grid_Manager.instance.CheckingRelativePosition(aiLink.xEnemy, aiLink.yEnemy);

            if (relativePos == 0)
            {
                for (i = 0; i < knockBackTaken; i++)
                {
                    if (!Grid_Manager.instance.CheckingRightCellExp(aiLink.xEnemy + i, aiLink.yEnemy))
                        break;
                }
                if (i != 0)
                {
                    Grid_Manager.instance.SwitchingOccupiedStatus(aiLink.xEnemy, aiLink.yEnemy);
                    aiLink.xEnemy += i;
                    Grid_Manager.instance.SwitchingOccupiedStatus(aiLink.xEnemy, aiLink.yEnemy);
                    aiLink.whereToGo = Grid_Manager.instance.GetCellTransform(aiLink.xEnemy, aiLink.yEnemy);
                    aiLink.isMoving = true;
                    aiLink.isKnockBacked = true;
                }
            }
            else if (relativePos == 1)
            {
                for (i = 0; i < knockBackTaken; i++)
                {
                    if (!Grid_Manager.instance.CheckingLeftCellExp(aiLink.xEnemy - i, aiLink.yEnemy))
                        break;
                }
                if (i != 0)
                {
                    Grid_Manager.instance.SwitchingOccupiedStatus(aiLink.xEnemy, aiLink.yEnemy);
                    aiLink.xEnemy -= i;
                    Grid_Manager.instance.SwitchingOccupiedStatus(aiLink.xEnemy, aiLink.yEnemy);
                    aiLink.whereToGo = Grid_Manager.instance.GetCellTransform(aiLink.xEnemy, aiLink.yEnemy);
                    aiLink.isMoving = true;
                    aiLink.isKnockBacked = true;
                }
            }
            else if (relativePos == 2)
            {
                for (i = 0; i < knockBackTaken; i++)
                {
                    if (!Grid_Manager.instance.CheckingDownCellExp(aiLink.xEnemy, aiLink.yEnemy - i))
                        break;
                }
                if (i != 0)
                {
                    Grid_Manager.instance.SwitchingOccupiedStatus(aiLink.xEnemy, aiLink.yEnemy);
                    aiLink.yEnemy -= i;
                    Grid_Manager.instance.SwitchingOccupiedStatus(aiLink.xEnemy, aiLink.yEnemy);
                    aiLink.whereToGo = Grid_Manager.instance.GetCellTransform(aiLink.xEnemy, aiLink.yEnemy);
                    aiLink.isMoving = true;
                    aiLink.isKnockBacked = true;
                }
            }
            else if (relativePos == 3)
            {
                for (i = 0; i < knockBackTaken; i++)
                {
                    if (!Grid_Manager.instance.CheckingUpCellExp(aiLink.xEnemy, aiLink.yEnemy + i))
                        break;
                }
                if (i != 0)
                {
                    Grid_Manager.instance.SwitchingOccupiedStatus(aiLink.xEnemy, aiLink.yEnemy);
                    aiLink.yEnemy += i;
                    Grid_Manager.instance.SwitchingOccupiedStatus(aiLink.xEnemy, aiLink.yEnemy);
                    aiLink.whereToGo = Grid_Manager.instance.GetCellTransform(aiLink.xEnemy, aiLink.yEnemy);
                    aiLink.isMoving = true;
                    aiLink.isKnockBacked = true;
                }
            }
        }
        #endregion



        #region Initialization Method
        public void InitializeEnemyController(enemySetting passedCurrentSetting)
        {
            enemyCurrentSetting = passedCurrentSetting;
        } 
        #endregion

    }
}


