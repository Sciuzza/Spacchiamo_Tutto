using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Spacchiamo
{
    public class Enemy_Patrolling : MonoBehaviour
    {

        Vector2 distance = new Vector2(-100, -100), direction;

        private bool isMoving = false, move_done = false;
        private int whereI, whereJ;
        private Transform whereToGo = null;

        public List<Cell_Interaction> patrolArea;

        public List<int> moveDirection;

        Enemy_Controller eControllerLink;

        void Awake()
        {

            eControllerLink = this.GetComponent<Enemy_Controller>();
            moveDirection = new List<int>();
            moveDirection.Add(0);
            moveDirection.Add(1);
            moveDirection.Add(2);
            moveDirection.Add(3);
        }

        void Update()
        {
            if (Game_Controller.instance.currentPhase == Game_Controller.GAME_PHASE.npcEnemyTurn)
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


                    this.distance = whereToGo.position - this.transform.position;
                    this.direction = this.distance.normalized;
                    this.transform.position = (Vector2)this.transform.position + this.direction * Time.deltaTime * Designer_Tweaks.instance.moveSpeed * (1 + Designer_Tweaks.instance.moveSpeed / 9.5f);

                    if (this.distance.sqrMagnitude < 0.01f)
                    {
                        this.transform.position = new Vector3(whereToGo.position.x, whereToGo.position.y, 0);
                        whereToGo = null;
                        ResettingMoveDirection();
                        Game_Controller.instance.AddingAMove();
                        move_done = true;
                        isMoving = false;

                    }

                }
            }
            else if (Game_Controller.instance.currentPhase == Game_Controller.GAME_PHASE.playerTurn)
                move_done = false;
        }


        public void SettingWhereI(int row)
        {
            whereI = row;
        }

        public void SettingWhereJ(int column)
        {
            whereJ = column;
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
    }
}