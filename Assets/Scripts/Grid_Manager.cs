﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Spacchiamo
{
    public class Grid_Manager : MonoBehaviour
    {


        private Cell_Interaction[,] cellReferences;
        private List<Cell_Interaction> leftPositions;

        [HideInInspector]
        public static Grid_Manager instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        // Used to Create the Logic Grid in the begin
        public void PreparingGridSpace()
        {

            cellReferences = new Cell_Interaction[Designer_Tweaks.instance.level1Rows, Designer_Tweaks.instance.level1Columns];
            leftPositions = new List<Cell_Interaction>();

            GameObject cellTemp = Resources.Load<GameObject>("Cell");
            GameObject mapTemp = Resources.Load<GameObject>("Map");
            mapTemp = Instantiate(mapTemp);
            int layerCount = 8;

            for (int i = 0; i < cellReferences.GetLength(0); i++)
            {
                layerCount++;

                for (int j = 0; j < cellReferences.GetLength(1); j++)
                {
                    cellTemp = Instantiate(cellTemp);
                    cellReferences[i, j] = cellTemp.GetComponent<Cell_Interaction>();

                    cellTemp.name = "Cell " + i + " , " + j;
                    cellTemp.transform.position = new Vector3((j - cellReferences.GetLength(1) / 2) + 0.5f, (i - cellReferences.GetLength(0) / 2) + 0.5f, 1);

                    cellReferences[i, j].cell_i = i;
                    cellReferences[i, j].cell_j = j;

                    cellTemp.transform.SetParent(mapTemp.transform);

                    cellTemp.layer = layerCount;

                    // Fog Of War
                    ChangingAlpha(0.0f, cellTemp);
                }
            }

        }

        // Method to retrieve the transform position necessary for the player to be placed into
        public Transform SettingPlayerPosition(int row, int column)
        {
            SwitchingOccupiedStatus(row, column);
            return cellReferences[row, column].transform;
        }

        public Cell_Interaction SettingEnemyPosition()
        {
            int chosenPosition = Random.Range(0, leftPositions.Count);
            SwitchingOccupiedStatus(leftPositions[chosenPosition].cell_i, leftPositions[chosenPosition].cell_j);
            return leftPositions[chosenPosition];

        }


        // Methods necessary to control if a moving object can effectively move and to Update Occupied Status

        public Transform CheckingUpCell(int row, int column)
        {

            if (row + 1 < cellReferences.GetLength(0))
            {
                if (!cellReferences[row + 1, column].isOccupied)
                {
                    SwitchingOccupiedStatus(row, column);
                    SwitchingOccupiedStatus(row + 1, column);
                    return cellReferences[row + 1, column].transform;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public Transform CheckingDownCell(int row, int column)
        {

            if (row - 1 >= 0)
            {
                if (!cellReferences[row - 1, column].isOccupied)
                {
                    SwitchingOccupiedStatus(row, column);
                    SwitchingOccupiedStatus(row - 1, column);
                    return cellReferences[row - 1, column].transform;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public Transform CheckingLeftCell(int row, int column)
        {

            if (column - 1 >= 0)
            {
                if (!cellReferences[row, column - 1].isOccupied)
                {
                    SwitchingOccupiedStatus(row, column);
                    SwitchingOccupiedStatus(row, column - 1);
                    return cellReferences[row, column - 1].transform;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public Transform CheckingRightCell(int row, int column)
        {

            if (column + 1 < cellReferences.GetLength(1))
            {
                if (!cellReferences[row, column + 1].isOccupied)
                {
                    SwitchingOccupiedStatus(row, column);
                    SwitchingOccupiedStatus(row, column + 1);
                    return cellReferences[row, column + 1].transform;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public Transform CheckingUpCell(int row, int column, List<Cell_Interaction> patrolArea)
        {

            if (row + 1 < cellReferences.GetLength(0))
            {
                if (!cellReferences[row + 1, column].isOccupied && patrolArea.Contains(cellReferences[row + 1, column]))
                {
                    SwitchingOccupiedStatus(row, column);
                    SwitchingOccupiedStatus(row + 1, column);
                    return cellReferences[row + 1, column].transform;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public Transform CheckingDownCell(int row, int column, List<Cell_Interaction> patrolArea)
        {

            if (row - 1 >= 0)
            {
                if (!cellReferences[row - 1, column].isOccupied && patrolArea.Contains(cellReferences[row - 1, column]))
                {
                    SwitchingOccupiedStatus(row, column);
                    SwitchingOccupiedStatus(row - 1, column);
                    return cellReferences[row - 1, column].transform;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public Transform CheckingLeftCell(int row, int column, List<Cell_Interaction> patrolArea)
        {

            if (column - 1 >= 0)
            {
                if (!cellReferences[row, column - 1].isOccupied && patrolArea.Contains(cellReferences[row, column - 1]))
                {
                    SwitchingOccupiedStatus(row, column);
                    SwitchingOccupiedStatus(row, column - 1);
                    return cellReferences[row, column - 1].transform;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public Transform CheckingRightCell(int row, int column, List<Cell_Interaction> patrolArea)
        {

            if (column + 1 < cellReferences.GetLength(1))
            {
                if (!cellReferences[row, column + 1].isOccupied && patrolArea.Contains(cellReferences[row, column + 1]))
                {
                    SwitchingOccupiedStatus(row, column);
                    SwitchingOccupiedStatus(row, column + 1);
                    return cellReferences[row, column + 1].transform;
                }
                else
                    return null;
            }
            else
                return null;

        }

        private void SwitchingOccupiedStatus(int row, int column)
        {
            if (!cellReferences[row, column].isOccupied)
                cellReferences[row, column].isOccupied = true;
            else
                cellReferences[row, column].isOccupied = false;
        }


        //Methods to Retrieve Light Effect around the player and around Falò 

        public void GettingLight(int row, int column)
        {

            float currentDistance;

            for (int i = 0; i < cellReferences.GetLength(0); i++)
            {
                for (int j = 0; j < cellReferences.GetLength(1); j++)
                {

                    currentDistance = Mathf.Abs(cellReferences[i, j].transform.position.x - cellReferences[row, column].transform.position.x) +
                        Mathf.Abs(cellReferences[i, j].transform.position.y - cellReferences[row, column].transform.position.y);



                    if (Designer_Tweaks.instance.manhDistancePlayer > currentDistance)
                    {
                        if (cellReferences[i, j].lightSource && !cellReferences[i, j].lightSourceDiscovered)
                        {
                            cellReferences[i, j].lightSourceDiscovered = true;
                            GettingLightObject(i, j);
                        }
                        else if (!cellReferences[i, j].isReceivingLight)
                            ChangingAlpha(1.0f, cellReferences[i, j].gameObject);
                    }
                    else if (Designer_Tweaks.instance.manhDistancePlayer == currentDistance)
                    {
                        if (!cellReferences[i, j].isReceivingLight)
                            ChangingAlpha(0.8f, cellReferences[i, j].gameObject);
                    }
                    else
                    {
                        if (!cellReferences[i, j].isReceivingLight)
                        {
                            if (GettingAlpha(cellReferences[i, j].gameObject) != 0.0f)
                                ChangingAlpha(0.5f, cellReferences[i, j].gameObject);
                            else
                                ChangingAlpha(0.0f, cellReferences[i, j].gameObject);
                        }
                    }

                }
            }

        }

        private void GettingLightObject(int row, int column)
        {

            float currentDistance;

            for (int i = 0; i < cellReferences.GetLength(0); i++)
            {
                for (int j = 0; j < cellReferences.GetLength(1); j++)
                {

                    currentDistance = Mathf.Abs(cellReferences[i, j].transform.position.x - cellReferences[row, column].transform.position.x) +
                        Mathf.Abs(cellReferences[i, j].transform.position.y - cellReferences[row, column].transform.position.y);

                    if (Designer_Tweaks.instance.manhDistanceFalo > currentDistance)
                    {
                        cellReferences[i, j].isReceivingLight = true;
                        ChangingAlpha(1.0f, cellReferences[i, j].gameObject);
                    }
                }
            }

        }

        // Methods to manage cell alpha

        public void ChangingAlpha(float alphaLevel, GameObject cell)
        {
            Color cellColor = cell.GetComponent<SpriteRenderer>().color;
            cellColor.a = alphaLevel;
            cell.GetComponent<SpriteRenderer>().color = cellColor;
        }

        private float GettingAlpha(GameObject cell)
        {
            return cell.GetComponent<SpriteRenderer>().color.a;
        }

        // Method to retrive if the cell where player is standing on is receiving light by falo or not

        public bool IsCellReceivingLight(int row, int column)
        {
            return cellReferences[row, column].isReceivingLight;
        }


        public int AskingRowsNumber()
        {
            return cellReferences.GetLength(0);
        }

        public int AskingColumnsNumber()
        {
            return cellReferences.GetLength(1);
        }

        public void AddingPosition(Cell_Interaction posToAdd)
        {
            if (!leftPositions.Contains(posToAdd))
                leftPositions.Add(posToAdd);
        }

        public void RemovingPosition(Cell_Interaction posToRemove)
        {
            if (leftPositions.Contains(posToRemove))
                leftPositions.Remove(posToRemove);
        }


        public List<Cell_Interaction> FindingPatrolArea(int row, int column)
        {
            float currentDistance;
            List<Cell_Interaction> areaFound = new List<Cell_Interaction>();

            for (int i = 0; i < cellReferences.GetLength(0); i++)
            {
                for (int j = 0; j < cellReferences.GetLength(1); j++)
                {

                    currentDistance = Mathf.Abs(cellReferences[i, j].transform.position.x - cellReferences[row, column].transform.position.x) +
                        Mathf.Abs(cellReferences[i, j].transform.position.y - cellReferences[row, column].transform.position.y);



                    if (Designer_Tweaks.instance.patrolAreaEnemy1 > currentDistance)
                    {
                        areaFound.Add(cellReferences[i, j]);
                    }


                }
            }

            return areaFound;
        }
    }
}