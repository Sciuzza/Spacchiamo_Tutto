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


        #region Life Feedback Variables

        public List<GameObject> lives = new List<GameObject>();
        public GameObject aggroFeed;

        public GameObject tempRefFullLife, tempRefHalfLife;

        public bool firstTimeAttacked = false;

        #endregion

        public GameObject aggroRef;

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


            enemyCurrentSetting.life -= damageTaken;



            if (enemyCurrentSetting.life <= 0)
                Enemies_Manager.instance.DestroyEnemy(aiLink.xEnemy, aiLink.yEnemy);
            else
            {

                if (!firstTimeAttacked)
                {
                    MakeLifeVisible();
                    firstTimeAttacked = true;
                }

                Destroylives();
                SettingOwnLifeFeed(enemyCurrentSetting.life);


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

        public void SettingOwnLifeFeed()
        {

            float livesCount = enemyCurrentSetting.life / 2;
            tempRefHalfLife = Resources.Load<GameObject>("Half Life");
            tempRefFullLife = Resources.Load<GameObject>("Enemy Life");



            do
            {

                if (livesCount == 0.5f)
                {

                    lives.Add(Instantiate(tempRefHalfLife));

                    lives[lives.Count - 1].transform.SetParent(this.transform);

                    lives[lives.Count - 1].transform.localPosition = new Vector3(-0.5f + (0.40f * (lives.Count - 1)), 1.4f, 0);

                    lives[lives.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = this.GetComponent<SpriteRenderer>().sortingOrder;
                    lifeAlphaChanging(0.0f, lives[lives.Count - 1].GetComponent<SpriteRenderer>());

                    livesCount -= 0.5f;
                }
                else
                {

                    lives.Add(Instantiate(tempRefFullLife));

                    lives[lives.Count - 1].transform.SetParent(this.transform);

                    lives[lives.Count - 1].transform.localPosition = new Vector3(-0.5f + (0.40f * (lives.Count - 1)), 1.4f, 0);

                    lives[lives.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = this.GetComponent<SpriteRenderer>().sortingOrder;
                    lifeAlphaChanging(0.0f, lives[lives.Count - 1].GetComponent<SpriteRenderer>());

                    livesCount--;
                }
            } while (livesCount >= 0.5f);


        }

        public void Destroylives()
        {
            for (int i = 0; i < lives.Count; i++)
            {
                Destroy(lives[i]);
            }
            lives.RemoveAll(x => x);
            lives.Clear();
            lives.TrimExcess();
        }

        public void SettingOwnLifeFeed(float lifeLeft)
        {
            float livesCount = lifeLeft / 2;
            tempRefHalfLife = Resources.Load<GameObject>("Half Life");
            tempRefFullLife = Resources.Load<GameObject>("Enemy Life");



            do
            {

                if (livesCount == 0.5f)
                {

                    lives.Add(Instantiate(tempRefHalfLife));

                    lives[lives.Count - 1].transform.SetParent(this.transform);

                    lives[lives.Count - 1].transform.localPosition = new Vector3(-0.5f + (0.40f * (lives.Count - 1)), 1.4f, 0);

                    lives[lives.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = this.GetComponent<SpriteRenderer>().sortingOrder;


                    livesCount -= 0.5f;
                }
                else
                {

                    lives.Add(Instantiate(tempRefFullLife));

                    lives[lives.Count - 1].transform.SetParent(this.transform);

                    lives[lives.Count - 1].transform.localPosition = new Vector3(-0.5f + (0.40f * (lives.Count - 1)), 1.4f, 0);

                    lives[lives.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = this.GetComponent<SpriteRenderer>().sortingOrder;


                    livesCount--;
                }
            } while (livesCount >= 0.5f);
        }

        public void SettingLifeSortingOrder()
        {
            for (int i = 0; i < lives.Count; i++)
            {
                lives[i].GetComponent<SpriteRenderer>().sortingOrder = this.GetComponent<SpriteRenderer>().sortingOrder;
            }
        }


        public void SettingOwnAggroFeed()
        {
            GameObject aggroObject = Resources.Load<GameObject>("Aggro");

            aggroRef = (Instantiate(aggroObject));

            aggroRef.transform.SetParent(this.transform);
            aggroRef.transform.localPosition = new Vector3(+0.5f, 1, 0);
            aggroRef.GetComponent<SpriteRenderer>().sortingOrder = this.GetComponent<SpriteRenderer>().sortingOrder;
            lifeAlphaChanging(0.0f, aggroRef.GetComponent<SpriteRenderer>());
        }

        public void SetAggroVisible()
        {
            lifeAlphaChanging(1.0f, aggroRef.GetComponent<SpriteRenderer>());
        }

        public void SetAggroInvisible()
        {
            lifeAlphaChanging(0.0f, aggroRef.GetComponent<SpriteRenderer>());
        }

        #endregion


        public void GhostAlphaChanging(float alpha)
        {
            Color ghostAlpha = GetComponent<SpriteRenderer>().color;
            ghostAlpha.a = alpha;
            GetComponent<SpriteRenderer>().color = ghostAlpha;
        }


        public void MakeLifeVisible()
        {
            for (int i = 0; i < lives.Count; i++)
                lifeAlphaChanging(1, lives[i].GetComponent<SpriteRenderer>());
        }

        public void lifeAlphaChanging(float alpha, SpriteRenderer lifeAlpha)
        {
            Color lifeAlphaChange = GetComponent<SpriteRenderer>().color;
            lifeAlphaChange.a = alpha;
            lifeAlpha.color = lifeAlphaChange;
        }

        public bool IsFlipped()
        {
            return this.GetComponent<SpriteRenderer>().flipX;
        }

        public void FlippingEnemy()
        {
            SpriteRenderer flipSprite = this.GetComponent<SpriteRenderer>();

            if (flipSprite.flipX)
            {
                flipSprite.flipX = false;
                SetAggroOnRight();
            }
            else
            {
                flipSprite.flipX = true;
                SetAggroOnLeft();
            }
        }

        public void SetAggroOnLeft()
        {
            aggroRef.transform.localPosition = new Vector3(-0.5f, 1, 0);
        }

        public void SetAggroOnRight()
        {
            aggroRef.transform.localPosition = new Vector3(+0.5f, 1, 0);
        }
    }
}


