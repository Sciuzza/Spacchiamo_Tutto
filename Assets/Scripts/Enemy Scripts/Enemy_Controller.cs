﻿using UnityEngine;
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


        #region Getting aiLink and OwnSprite Reference

        public EnemyAI aiLink;
        public Sprite visible;

        void Awake()
        {
            aiLink = this.GetComponent<EnemyAI>();
            visible = this.GetComponent<SpriteRenderer>().sprite;
        }
        #endregion
        



        #region Taking Player Abilities Effects
        public void TakingPlayerAbilityEffects(float damageTaken, int knockBackTaken)
        {
            isAggroed = true;

            if(enemyCurrentSetting.behaviour != behaviour.fearMonster)
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

        public void InitializingOwnPatrol()
        {
           aiLink.patrolArea.AddRange(Grid_Manager.instance.FindingPatrolArea(aiLink.xEnemy, aiLink.yEnemy, enemyCurrentSetting.patrolArea, enemyCurrentSetting.patrolRange));
        }

        public void InitializingOwnAggro()
        {

        }

        #endregion


        public void GhostAlphaChanging(float alpha)
        {
            Color ghostAlpha = GetComponent<SpriteRenderer>().color;
            ghostAlpha.a = alpha;
            GetComponent<SpriteRenderer>().color = ghostAlpha;
        }

        public bool IsFlipped()
        {
            return this.GetComponent<SpriteRenderer>().flipX;
        }

        public void FlippingEnemy()
        {
            SpriteRenderer flipSprite = this.GetComponent<SpriteRenderer>();

            if (flipSprite.flipX)
                flipSprite.flipX = false;
            else
                flipSprite.flipX = true;

        }

    }
}


