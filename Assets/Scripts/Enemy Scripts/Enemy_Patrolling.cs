using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Spacchiamo
{
    public class Enemy_Patrolling : MonoBehaviour
    {

        Vector2 distance = new Vector2(-100, -100), direction;

        public bool isMoving = false;
        public bool move_done = false;
        public int whereI, whereJ;
        public int whereIComBack, whereJComBack;
        public Transform whereToGo = null;

        public bool isRangeChecked = false;

        Ability1 abilityLink;

        public List<Cell_Interaction> patrolArea;

        public List<int> moveDirection;

        Enemy_Controller eControllerLink;

        public List<Transform> possibleMoves;

        void Awake()
        {
            eControllerLink = this.GetComponent<Enemy_Controller>();
            moveDirection = new List<int>();
            moveDirection.Add(0);
            moveDirection.Add(1);
            moveDirection.Add(2);
            moveDirection.Add(3);

            abilityLink = this.GetComponent<Ability1>();

        }

        void Update()
        {
            if (Game_Controller.instance.currentPhase == Game_Controller.GAME_PHASE.npcEnemyTurn)
            {
                if (!eControllerLink.isAggroed)
                {
                    if (!eControllerLink.isComingBack)
                        Patrolling();
                    else
                        ComingBack();
                }
                else
                {
                    if (!isRangeChecked)
                    {
                        if (CheckingRange())
                            Attacking();
                    }
                    else
                        Following();
                }
            }
            else if (Game_Controller.instance.currentPhase == Game_Controller.GAME_PHASE.playerTurn)
            {
                move_done = false;
                isRangeChecked = false;
                
            }
        }


        public void SettingWhereI(int row)
        {
            whereI = row;
            whereIComBack = row;
        }

        public void SettingWhereJ(int column)
        {
            whereJ = column;
            whereJComBack = column;
        }

        public int GettingRow()
        {
            return whereI;
        }

        public int GettingColumn()
        {
            return whereJ;
        }



        public void InitalizingPatrolArea(List<Cell_Interaction> area)
        {
            for (int i = 0; i < area.Count; i++)
                patrolArea.Add(area[i]);
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
                if (moveDirection.Count != 0)
                {
                    int chosenDirection = Random.Range(0, moveDirection.Count); // 0 is up, 1 is down, 2 is left, 3 is right

                    switch (moveDirection[chosenDirection])
                    {
                        case 0:
                            whereToGo = Grid_Manager.instance.CheckingUpCell(whereI, whereJ, patrolArea);
                            if (whereToGo != null)
                            {
                                whereI++;
                                isMoving = true;
                            }
                            else
                            {
                                moveDirection.RemoveAt(chosenDirection);
                            }
                            break;
                        case 1:
                            whereToGo = Grid_Manager.instance.CheckingDownCell(whereI, whereJ, patrolArea);
                            if (whereToGo != null)
                            {
                                whereI--;
                                isMoving = true;
                            }
                            else
                            {
                                moveDirection.RemoveAt(chosenDirection);
                            }
                            break;
                        case 2:
                            whereToGo = Grid_Manager.instance.CheckingLeftCell(whereI, whereJ, patrolArea);
                            if (whereToGo != null)
                            {
                                whereJ--;
                                isMoving = true;
                            }
                            else
                            {
                                moveDirection.RemoveAt(chosenDirection);
                            }
                            break;
                        case 3:
                            whereToGo = Grid_Manager.instance.CheckingRightCell(whereI, whereJ, patrolArea);
                            if (whereToGo != null)
                            {
                                whereJ++;
                                isMoving = true;
                            }
                            else
                            {
                                moveDirection.RemoveAt(chosenDirection);
                            }
                            break;
                    }
                }
                else
                {
                    whereToGo = this.transform;
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
                possibleMoves.Clear();
                possibleMoves.TrimExcess();
                possibleMoves = new List<Transform>();

                possibleMoves = Grid_Manager.instance.RetrievingPossibleMovements(whereI, whereJ);

                Grid_Manager.instance.SwitchingOccupiedStatus(whereI, whereJ);
                whereToGo = Grid_Manager.instance.FindFastestRoute(possibleMoves, out whereI, out whereJ);
                isMoving = true;

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

                possibleMoves = Grid_Manager.instance.RetrievingPossibleMovements(whereI, whereJ);

                Grid_Manager.instance.SwitchingOccupiedStatus(whereI, whereJ);
                whereToGo = Grid_Manager.instance.FindFastestBackRoute(possibleMoves, whereIComBack, whereJComBack, out whereI, out whereJ);
                isMoving = true;

            }
            else if (isMoving)
                TranslatingPosition();
        

    }

        private void Attacking()
        {
            if (!move_done)
            {
                Grid_Manager.instance.MakeDamageToPlayer(abilityLink.damage);
                move_done = true;
            }
            
        }


        private void TranslatingPosition()
        {
            this.distance = whereToGo.position - this.transform.position;
            this.direction = this.distance.normalized;
            this.transform.position = (Vector2)this.transform.position + this.direction * Time.deltaTime * Designer_Tweaks.instance.moveSpeed * (1 + Designer_Tweaks.instance.moveSpeed / 9.5f);

            if (this.distance.sqrMagnitude < 0.01f)
            {
                this.transform.position = new Vector3(whereToGo.position.x, whereToGo.position.y, 0);
                ResettingMoveDirection();
                move_done = true;
                isMoving = false;

                if (whereI == whereIComBack && whereJ == whereJComBack && eControllerLink.isComingBack)
                    eControllerLink.isComingBack = false;

            }
        }


        private bool CheckingRange()
        {
            if (!abilityLink.isInCooldown && Grid_Manager.instance.CalcEnemyPlayDist(whereI, whereJ) == abilityLink.range)
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
    }
}