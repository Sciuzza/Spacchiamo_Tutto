using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Spacchiamo
{
    public class EnemyAI : MonoBehaviour
    {

        Vector2 distance = new Vector2(-100, -100), direction;

        public bool isMoving = false;
        public bool move_done = false;
        public bool isRangeChecked = false;

        public bool booleanResetted = false;

        public bool isKnockBacked = false;

        private bool ghostMechanic;
        public int kamiCounter = 0;


        public int xEnemy, yEnemy;
        public int xComeBack, yComeBack;
        public Transform whereToGo = null;

        private int patChoiceCounter = 0;
        private int moveRateCounter = 0;

        public List<Cell_Interaction> patrolArea = new List<Cell_Interaction>();

        public List<int> moveDirection;

        Enemy_Controller eControllerLink;

        public List<Transform> possibleMoves;


        public List<Cell_Interaction> openNodeList, closedNodeList;

        void Awake()
        {
            eControllerLink = this.GetComponent<Enemy_Controller>();
            moveDirection = new List<int>();
            moveDirection.Add(0);
            moveDirection.Add(1);
            moveDirection.Add(2);
            moveDirection.Add(3);

           

            xEnemy =  Mathf.FloorToInt(this.transform.position.x);
            yEnemy =  Mathf.FloorToInt(this.transform.position.y);
            xComeBack = xEnemy;
            yComeBack = yEnemy;
            
        }

        

        void Update()
        {
            if (Game_Controller.instance.currentPhase == GAME_PHASE.npcEnemyTurn)
            {

                if (!eControllerLink.isAggroed)
                {
                    if (!eControllerLink.isComingBack)
                        Patrolling();
                    else
                        ComingBackAStar();
                }
                else
                {
                    if (eControllerLink.enemyCurrentSetting.behaviour != behaviour.kamikaze)
                    {
                        if (!isRangeChecked)
                        {
                            if (CheckingRange())
                                Attacking();
                        }
                        else
                            FollowingAStar();
                    }
                    else
                    {
                        if (kamiCounter < eControllerLink.enemyCurrentSetting.ability.cooldown)
                        {
                            FollowingAStar();
                        }
                        else
                            KamiExplosion();
                    }
                }
            }
            else if (Game_Controller.instance.currentPhase == GAME_PHASE.knockAni && isKnockBacked)
            {
                TranslatingPosition();
            }
            else if (Game_Controller.instance.currentPhase == GAME_PHASE.playerTurn && !booleanResetted)
            {
                move_done = false;
                isRangeChecked = false;

                if (eControllerLink.enemyCurrentSetting.behaviour == behaviour.kamikaze)
                {
                    if (eControllerLink.isAggroed)
                        kamiCounter++;
                    else
                        kamiCounter = 0;
                }

                if (eControllerLink.aggroIgnoringCounter == 2)
                {
                    eControllerLink.aggroIgnoringCounter = 0;
                    eControllerLink.isIgnoringAggro = false;
                }
                else
                    eControllerLink.aggroIgnoringCounter++;

                booleanResetted = true;

            }
        }


      

        public int GettingXEnemy()
        {
            return xEnemy;
        }

        public int GettingYEnemy()
        {
            return yEnemy;
        }



        private void ResettingMoveDirection()
        {
            moveDirection.Clear();
            moveDirection.TrimExcess();
            moveDirection.Add(0);
            moveDirection.Add(1);
            moveDirection.Add(2);
            moveDirection.Add(3);
        }



        //Possible Enemy Actions

        private void Patrolling()
        {
            if (!isMoving && !move_done)
            {
                if (moveDirection.Count != 0 && moveRateCounter == 0 && eControllerLink.enemyCurrentSetting.patrolStyle != patrolStyle.standingStill)
                {
                    
                    int chosenDirection;
                    if (eControllerLink.enemyCurrentSetting.patrolStyle == patrolStyle.randomic)
                        chosenDirection = Random.Range(0, moveDirection.Count); // 0 is up, 1 is down, 2 is left, 3 is right
                    else if (eControllerLink.enemyCurrentSetting.patrolStyle == patrolStyle.precUp && patChoiceCounter == 0)
                    {
                        chosenDirection = 0;
                    }
                    else if (eControllerLink.enemyCurrentSetting.patrolStyle == patrolStyle.precDown && patChoiceCounter == 0)
                    {
                        chosenDirection = 1;
                    }
                    else if (eControllerLink.enemyCurrentSetting.patrolStyle == patrolStyle.precLeft && patChoiceCounter == 0)
                    {
                        chosenDirection = 2;
                    }
                    else if (eControllerLink.enemyCurrentSetting.patrolStyle == patrolStyle.precLeft && patChoiceCounter == 0)
                    {
                        chosenDirection = 3;
                    }
                    else
                        chosenDirection = Random.Range(0, moveDirection.Count);

                    switch (moveDirection[chosenDirection])
                    {
                        case 0:
                            whereToGo = Grid_Manager.instance.CheckingUpCell(xEnemy, yEnemy, patrolArea);
                            if (whereToGo != null)
                            {
                                yEnemy++;
                                booleanResetted = false;
                                isMoving = true;
                                patChoiceCounter = 0;
                                moveRateCounter++;
                            }
                            else
                            {
                                moveDirection.RemoveAt(chosenDirection);
                                patChoiceCounter++;
                            }
                            break;
                        case 1:
                            whereToGo = Grid_Manager.instance.CheckingDownCell(xEnemy, yEnemy, patrolArea);
                            if (whereToGo != null)
                            {
                                yEnemy--;
                                booleanResetted = false;
                                isMoving = true;
                                patChoiceCounter = 0;
                                moveRateCounter++;
                            }
                            else
                            {
                                moveDirection.RemoveAt(chosenDirection);
                                patChoiceCounter++;
                            }
                            break;
                        case 2:
                            whereToGo = Grid_Manager.instance.CheckingLeftCell(xEnemy, yEnemy, patrolArea);
                            if (whereToGo != null)
                            {
                                xEnemy--;
                                booleanResetted = false;
                                isMoving = true;
                                patChoiceCounter = 0;
                                moveRateCounter++;
                                if (!eControllerLink.IsFlipped())
                                    eControllerLink.FlippingEnemy();
                            }
                            else
                            {
                                moveDirection.RemoveAt(chosenDirection);
                                patChoiceCounter++;
                            }
                            break;
                        case 3:
                            whereToGo = Grid_Manager.instance.CheckingRightCell(xEnemy, yEnemy, patrolArea);
                            if (whereToGo != null)
                            {
                                xEnemy++;
                                booleanResetted = false;
                                isMoving = true;
                                patChoiceCounter = 0;
                                moveRateCounter++;
                                if (eControllerLink.IsFlipped())
                                    eControllerLink.FlippingEnemy();
                            }
                            else
                            {
                                moveDirection.RemoveAt(chosenDirection);
                                patChoiceCounter++;
                            }
                            break;
                    }
                }
                else
                {
                    moveRateCounter++;
                    whereToGo = this.transform;
                    booleanResetted = false;
                    isMoving = true;
                }

            }
            else if (isMoving)
            {
                TranslatingPosition();              
            }
        }

        private void Following() {

            if (!isMoving && !move_done)
            {
                if (eControllerLink.enemyCurrentSetting.aggroStyle == aggroStyle.following)
                {
                    GhostMechanic();

                    possibleMoves.Clear();
                    possibleMoves.TrimExcess();
                    possibleMoves = new List<Transform>();


                    possibleMoves = Grid_Manager.instance.RetrievingPossibleMovements(xEnemy, yEnemy);
                    Grid_Manager.instance.SwitchingOccupiedStatus(xEnemy, yEnemy);
                    whereToGo = Grid_Manager.instance.FindFastestRoute(possibleMoves, out xEnemy, out yEnemy);
                    Grid_Manager.instance.SwitchingOccupiedStatus(xEnemy, yEnemy);

                    booleanResetted = false;
                    isMoving = true;
                }
                else
                {
                    GhostMechanic();
                    booleanResetted = false;
                    isMoving = true;
                }

            }
            else if (isMoving)
                TranslatingPosition();
        }

        private void FollowingAStar()
        {

            if (!isMoving && !move_done)
            {
                if (eControllerLink.enemyCurrentSetting.aggroStyle == aggroStyle.following)
                {
                    GhostMechanic();
                    int xPlayer, yPlayer;
                    int ownIndex = Enemies_Manager.instance.RetrieveOwnEnemyIndex(this.gameObject);


                    openNodeList = new List<Cell_Interaction>();
                    closedNodeList = new List<Cell_Interaction>();

                    Grid_Manager.instance.RetrievePlayerCoords(out xPlayer, out yPlayer);
                    Grid_Manager.instance.SwitchingOccupiedStatus(xEnemy, yEnemy);
                    Grid_Manager.instance.AStarAlgoExp(xEnemy, yEnemy, xEnemy, yEnemy, xPlayer, yPlayer, ownIndex, openNodeList, closedNodeList, out xEnemy, out yEnemy);
                    whereToGo = Grid_Manager.instance.GetCellTransform(xEnemy, yEnemy);
                    Grid_Manager.instance.SwitchingOccupiedStatus(xEnemy, yEnemy);

                    if (xEnemy - xPlayer > 0 && !eControllerLink.IsFlipped())
                        eControllerLink.FlippingEnemy();
                    else if (xEnemy - xPlayer <= 0 && eControllerLink.IsFlipped())
                        eControllerLink.FlippingEnemy();
                    

                    booleanResetted = false;
                    isMoving = true;
                }
                else
                {
                    GhostMechanic();
                    booleanResetted = false;
                    isMoving = true;
                }
            }
            else if (isMoving)
                TranslatingPosition();
        }

        private void ComingBack()
        {

            if (!isMoving && !move_done)
            {

                possibleMoves.Clear();
                possibleMoves.TrimExcess();
                possibleMoves = new List<Transform>();

                possibleMoves = Grid_Manager.instance.RetrievingPossibleMovements(xEnemy, yEnemy);

                Grid_Manager.instance.SwitchingOccupiedStatus(xEnemy, yEnemy);
                whereToGo = Grid_Manager.instance.FindFastestBackRoute(possibleMoves, xComeBack, yComeBack, out xEnemy, out yEnemy);
                Grid_Manager.instance.SwitchingOccupiedStatus(xEnemy, yEnemy);


                if (xEnemy - xComeBack > 0 && !eControllerLink.IsFlipped())
                    eControllerLink.FlippingEnemy();
                else if (xEnemy - xComeBack <= 0 && eControllerLink.IsFlipped())
                    eControllerLink.FlippingEnemy();

                booleanResetted = false;
                isMoving = true;

            }
            else if (isMoving)
                TranslatingPosition();
        

    }

        private void ComingBackAStar()
        {
            if (!isMoving && !move_done)
            {
                int ownIndex = Enemies_Manager.instance.RetrieveOwnEnemyIndex(this.gameObject);


                openNodeList = new List<Cell_Interaction>();
                closedNodeList = new List<Cell_Interaction>();


                Grid_Manager.instance.SwitchingOccupiedStatus(xEnemy, yEnemy);
                Grid_Manager.instance.AStarAlgoExpComingBack(xEnemy, yEnemy, xEnemy, yEnemy, xComeBack, yComeBack, ownIndex, openNodeList, closedNodeList, out xEnemy, out yEnemy);
                whereToGo = Grid_Manager.instance.GetCellTransform(xEnemy, yEnemy);
                Grid_Manager.instance.SwitchingOccupiedStatus(xEnemy, yEnemy);

                if (xEnemy - xComeBack > 0 && !eControllerLink.IsFlipped())
                    eControllerLink.FlippingEnemy();
                else if (xEnemy - xComeBack <= 0 && eControllerLink.IsFlipped())
                    eControllerLink.FlippingEnemy();


                booleanResetted = false;
                isMoving = true;
            }
            else if (isMoving)
                TranslatingPosition();
    }

        private void Attacking()
        {
            if (!move_done)
            {
                if (eControllerLink.enemyCurrentSetting.behaviour != behaviour.fearMonster)
                {
                    GhostMechanic();

                    int xPlayer, yPlayer;
                    Grid_Manager.instance.RetrievePlayerCoords(out xPlayer, out yPlayer);

                    if (xEnemy - xPlayer > 0 && !eControllerLink.IsFlipped())
                        eControllerLink.FlippingEnemy();
                    else if (xEnemy - xPlayer <= 0 && eControllerLink.IsFlipped())
                        eControllerLink.FlippingEnemy();

                    Grid_Manager.instance.MakeDamageToPlayer(eControllerLink.enemyCurrentSetting.ability.damage);
                    booleanResetted = false;
                    move_done = true;
                }
                else
                {
                    Grid_Manager.instance.MakeDamageToPlayer(eControllerLink.enemyCurrentSetting.ability.damage);
                    Enemies_Manager.instance.DestroyEnemy(xEnemy, yEnemy);
                }
            }           
        }

        private void KamiExplosion()
        {
            int xPlayer, yPlayer;

            Grid_Manager.instance.RetrievePlayerCoords(out xPlayer, out yPlayer);

            if (Grid_Manager.instance.RetrieveManhDistfromAtoB(xEnemy, yEnemy, xPlayer, yPlayer) <= eControllerLink.enemyCurrentSetting.ability.range)
                Grid_Manager.instance.MakeDamageToPlayer(eControllerLink.enemyCurrentSetting.ability.damage);

            //move_done = true;
            Enemies_Manager.instance.DestroyEnemy(xEnemy, yEnemy);
        }

        private void TranslatingPosition()
        {
            this.distance = whereToGo.position - this.transform.position;
            this.direction = this.distance.normalized;
            this.transform.position = (Vector2)this.transform.position + this.direction * Time.deltaTime * Designer_Tweaks.instance.generalMoveSpeed * (1 + Designer_Tweaks.instance.generalMoveSpeed / 9.5f);

            if (this.distance.sqrMagnitude < 0.01f)
            {
                this.transform.position = new Vector3(whereToGo.position.x, whereToGo.position.y, 0);
                this.GetComponent<SpriteRenderer>().sortingOrder = Designer_Tweaks.instance.Level1YWidth - yEnemy;
                eControllerLink.aggroRef.GetComponent<SpriteRenderer>().sortingOrder = Designer_Tweaks.instance.Level1YWidth - yEnemy;
                eControllerLink.SettingLifeSortingOrder();

                ResettingMoveDirection();
                isMoving = false;

                if (xEnemy == xComeBack && yEnemy == yComeBack && eControllerLink.isComingBack)
                    eControllerLink.isComingBack = false;

                if (isKnockBacked)
                    isKnockBacked = false;
                else
                    move_done = true;

                if (moveRateCounter == eControllerLink.enemyCurrentSetting.moveRate)
                    moveRateCounter = 0;

            }
        }


        private bool CheckingRange()
        {
            if (Grid_Manager.instance.CalcEnemyPlayDist(xEnemy, yEnemy) <= eControllerLink.enemyCurrentSetting.ability.range)
            {
                isRangeChecked = true;
                return true;
            }
            else
            {
                isRangeChecked = true;
                return false;
            }
        }


        private void GhostMechanic()
        {
            if (eControllerLink.enemyCurrentSetting.behaviour == behaviour.ghost && !ghostMechanic)
            {
                eControllerLink.GhostAlphaChanging(0);
                ghostMechanic = true;
            }
            else
            {
                eControllerLink.GhostAlphaChanging(1);
                ghostMechanic = false;
            }
        }

        
    }
}