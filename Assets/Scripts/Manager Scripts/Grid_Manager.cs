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


        public GameObject tilesHolder;



        List<Sprite> wallList = new List<Sprite>();
        Sprite[] wallSprites;


        

        [HideInInspector]
        public static Grid_Manager instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);


            wallSprites = Resources.LoadAll<Sprite>("Tilesets/Bordo_inferiore_tiles_pietra");
            wallList.AddRange(wallSprites);

            
        }


    

        public void PreparingOptimizedGridSpace()
        {
            if(tilesHolder == null)
               tilesHolder = GameObject.FindGameObjectWithTag("TilesLayer");

            TileLoader tileLoaderTemp = tilesHolder.GetComponent<TileLoader>();
            List<TileData> tileReferences = tileLoaderTemp.LoadAllTilesInScene("Tile");

            cellReferences = new Cell_Interaction[Designer_Tweaks.instance.level1XWidth, Designer_Tweaks.instance.level1yWidth];


            GameObject cellTemp = Resources.Load<GameObject>("Cell");
            GameObject mapTemp = Resources.Load<GameObject>("Map");
            mapTemp = Instantiate(mapTemp);

            int x, y;

            Debug.Log(tileReferences.Count);

            for (int i = 0; i < tileReferences.Count; i++)
            {

                GameObject currentCell = Instantiate(cellTemp);

                x = tileReferences[i].cell_x;
                y = tileReferences[i].cell_y;

                cellReferences[x, y] = currentCell.GetComponent<Cell_Interaction>();

                currentCell.name = "Cell " + x + " , " + y;
                currentCell.transform.position = new Vector3(x + 0.5f, y + 0.5f, 1);

                cellReferences[x, y].yCell = y;
                cellReferences[x, y].xCell = x;

                currentCell.transform.SetParent(mapTemp.transform);



                cellReferences[x, y].tileCell = tileReferences[i].go;


                SpriteRenderer tileType = cellReferences[x, y].tileCell.GetComponent<SpriteRenderer>();



                if (wallList.Find(z => z.name == tileType.sprite.name) != null)
                    cellReferences[x, y].SettingWall();






                // Fog Of War
                ChangingAlpha(0.0f, currentCell);


            }

            
            Debug.Log(Time.realtimeSinceStartup);
        }

        public void LinkingFaloMechanic(GameObject[] faloList)
        {
            int x, y;

            for (int i = 0; i < faloList.GetLength(0); i++)
            {
                x = Mathf.FloorToInt(faloList[i].transform.position.x);
                y = Mathf.FloorToInt(faloList[i].transform.position.y);

                cellReferences[x, y].SettingFalo();
                cellReferences[x, y].faloAlpha = faloList[i].GetComponent<SpriteRenderer>();
            }
        }


        // Methods for the Movement System

        public Transform CheckingUpCell(int x, int y)
        {

            if (y + 1 < cellReferences.GetLength(1))
            {

                if (cellReferences[x, y + 1] != null && !cellReferences[x, y + 1].isOccupied)
                {
                    SwitchingOccupiedStatus(x, y);
                    SwitchingOccupiedStatus(x, y + 1);
                    return cellReferences[x, y + 1].transform;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public Transform CheckingDownCell(int x, int y)
        {

            if (y - 1 >= 0)
            {
                if (cellReferences[x, y - 1] != null && !cellReferences[x, y - 1].isOccupied)
                {
                    SwitchingOccupiedStatus(x, y);
                    SwitchingOccupiedStatus(x, y - 1);
                    return cellReferences[x, y - 1].transform;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public Transform CheckingLeftCell(int x, int y)
        {

            if (x - 1 >= 0)
            {
                if (cellReferences[x - 1, y] != null && !cellReferences[x - 1, y].isOccupied)
                {
                    SwitchingOccupiedStatus(x, y);
                    SwitchingOccupiedStatus(x - 1, y);
                    return cellReferences[x - 1, y].transform;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public Transform CheckingRightCell(int x, int y)
        {

            if (x + 1 < cellReferences.GetLength(0))
            {
                if (cellReferences[x + 1, y] != null && !cellReferences[x + 1, y].isOccupied)
                {
                    SwitchingOccupiedStatus(x, y);
                    SwitchingOccupiedStatus(x + 1, y);
                    return cellReferences[x + 1, y].transform;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public Transform CheckingUpCell(int x, int y, List<Cell_Interaction> patrolArea)
        {

            if (y + 1 < cellReferences.GetLength(1))
            {
                if (cellReferences[x, y + 1] != null && !cellReferences[x, y + 1].isOccupied && patrolArea.Contains(cellReferences[x, y + 1]))
                {
                    SwitchingOccupiedStatus(x, y);
                    SwitchingOccupiedStatus(x, y + 1);
                    return cellReferences[x, y + 1].transform;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public Transform CheckingDownCell(int x, int y, List<Cell_Interaction> patrolArea)
        {

            if (y - 1 >= 0)
            {
                if (cellReferences[x, y - 1] != null && !cellReferences[x, y - 1].isOccupied && patrolArea.Contains(cellReferences[x, y - 1]))
                {
                    SwitchingOccupiedStatus(x, y);
                    SwitchingOccupiedStatus(x, y - 1);
                    return cellReferences[x, y - 1].transform;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public Transform CheckingLeftCell(int x, int y, List<Cell_Interaction> patrolArea)
        {

            if (x - 1 >= 0)
            {
                if (cellReferences[x - 1, y] != null && !cellReferences[x - 1, y].isOccupied && patrolArea.Contains(cellReferences[x - 1, y]))
                {
                    SwitchingOccupiedStatus(x, y);
                    SwitchingOccupiedStatus(x - 1, y);
                    return cellReferences[x - 1, y].transform;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public Transform CheckingRightCell(int x, int y, List<Cell_Interaction> patrolArea)
        {

            if (x + 1 < cellReferences.GetLength(0))
            {
                if (cellReferences[x + 1, y] != null && !cellReferences[x + 1, y].isOccupied && patrolArea.Contains(cellReferences[x + 1, y]))
                {
                    SwitchingOccupiedStatus(x, y);
                    SwitchingOccupiedStatus(x + 1, y);
                    return cellReferences[x + 1, y].transform;
                }
                else
                    return null;
            }
            else
                return null;

        }

        private bool CheckingUpCellExp(int xEnemy, int yEnemy)
        {
            if (yEnemy + 1 < cellReferences.GetLength(1))
            {
                if (cellReferences[xEnemy, yEnemy + 1] != null && !cellReferences[xEnemy, yEnemy + 1].isOccupied)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private bool CheckingDownCellExp(int xEnemy, int yEnemy)
        {
            if (yEnemy - 1 > 0)
            {
                if (cellReferences[xEnemy, yEnemy - 1] != null && !cellReferences[xEnemy, yEnemy - 1].isOccupied)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private bool CheckingLeftCellExp(int xEnemy, int yEnemy)
        {
            if (xEnemy - 1 > 0)
            {
                if (cellReferences[xEnemy - 1, yEnemy] != null && !cellReferences[xEnemy - 1, yEnemy].isOccupied)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private bool CheckingRightCellExp(int xEnemy, int yEnemy)
        {
            if (xEnemy + 1 < cellReferences.GetLength(0))
            {
                if (cellReferences[xEnemy + 1, yEnemy] != null && !cellReferences[xEnemy + 1, yEnemy].isOccupied)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }


        public Transform GetCellTransform(int row, int column)
        {
            return cellReferences[row, column].transform;
        }

        public void SwitchingOccupiedStatus(int x, int y)
        {
            if (!cellReferences[x, y].isOccupied)
                cellReferences[x, y].isOccupied = true;
            else
                cellReferences[x, y].isOccupied = false;
        }


        //Methods to Retrieve Light Effect around the player and around Falò 

        public void GettingLight(int xPlayer, int yPlayer)
        {

            float currentDistance;

            for (int y = 0; y < cellReferences.GetLength(1); y++)
            {
                for (int x = 0; x < cellReferences.GetLength(0); x++)
                {
                    if (cellReferences[x, y] != null)
                    {
                        currentDistance = Mathf.Abs(cellReferences[x, y].transform.position.x - cellReferences[xPlayer, yPlayer].transform.position.x) +
                            Mathf.Abs(cellReferences[x, y].transform.position.y - cellReferences[xPlayer, yPlayer].transform.position.y);



                        if (Designer_Tweaks.instance.playerLightM > currentDistance)
                        {
                            if (cellReferences[x, y].lightSource && !cellReferences[x, y].lightSourceDiscovered)
                            {
                                cellReferences[x, y].lightSourceDiscovered = true;
                                GettingLightObject(x, y);
                            }
                            else if (!cellReferences[x, y].isReceivingLight)
                            {
                                cellReferences[x, y].aggroCell = true;
                                ChangingAlpha(1.0f, cellReferences[x, y].gameObject);
                                //  ChangingAlpha(1.0f, cellReferences[x, y].tileCell);
                            }
                        }
                        else if (Designer_Tweaks.instance.playerLightM == currentDistance)
                        {
                            if (!cellReferences[x, y].isReceivingLight)
                            {
                                cellReferences[x, y].aggroCell = false;
                                ChangingAlpha(0.8f, cellReferences[x, y].gameObject);
                                //  ChangingAlpha(0.8f, cellReferences[x, y].tileCell);
                            }
                        }
                        else
                        {
                            if (!cellReferences[x, y].isReceivingLight)
                            {
                                cellReferences[x, y].aggroCell = false;

                                if (GettingAlpha(cellReferences[x, y].gameObject) != 0.0f)
                                {
                                    ChangingAlpha(0.5f, cellReferences[x, y].gameObject);
                                    // ChangingAlpha(0.5f, cellReferences[x, y].tileCell);
                                }
                                else
                                {
                                    ChangingAlpha(0.0f, cellReferences[x, y].gameObject);
                                    // ChangingAlpha(0.0f, cellReferences[x, y].tileCell);
                                }
                            }
                        }
                    }
                }
            }

        }

        private void GettingLightObject(int xFalo, int yFalo)
        {

            float currentDistance;

            for (int y = 0; y < cellReferences.GetLength(1); y++)
            {
                for (int x = 0; x < cellReferences.GetLength(0); x++)
                {
                    if (cellReferences[x, y] != null)
                    {
                        currentDistance = Mathf.Abs(cellReferences[x, y].transform.position.x - cellReferences[xFalo, yFalo].transform.position.x) +
                        Mathf.Abs(cellReferences[x, y].transform.position.y - cellReferences[xFalo, yFalo].transform.position.y);

                        if (Designer_Tweaks.instance.faloLigthM > currentDistance)
                        {
                            cellReferences[x, y].isReceivingLight = true;
                            ChangingAlpha(1.0f, cellReferences[x, y].gameObject);
                            // ChangingAlpha(1.0f, cellReferences[x, y].tileCell);
                        }
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

            Cell_Interaction matchAlpha = cell.GetComponent<Cell_Interaction>();

            matchAlpha.tileCell.GetComponent<SpriteRenderer>().color = cellColor;

            if (matchAlpha.faloAlpha != null)
                matchAlpha.faloAlpha.color = cellColor;

        }

        private float GettingAlpha(GameObject cell)
        {
            return cell.GetComponent<SpriteRenderer>().color.a;
        }

        // Method to retrive if the cell where player is standing on is receiving light by falo or not

        public bool IsCellReceivingLight(int xCell, int yCell)
        {
            return cellReferences[xCell, yCell].isReceivingLight;
        }




        //Method for the enemy Moves
        public List<Cell_Interaction> FindingPatrolArea(int xEnemy, int yEnemy)
        {
            float currentDistance;
            List<Cell_Interaction> areaFound = new List<Cell_Interaction>();

            for (int y = 0; y < cellReferences.GetLength(1); y++)
            {
                for (int x = 0; x < cellReferences.GetLength(0); x++)
                {
                    if (cellReferences[x, y] != null)
                    {
                        currentDistance = Mathf.Abs(cellReferences[x, y].transform.position.x - cellReferences[xEnemy, yEnemy].transform.position.x) +
                        Mathf.Abs(cellReferences[x, y].transform.position.y - cellReferences[xEnemy, yEnemy].transform.position.y);



                        if (Designer_Tweaks.instance.patrolAreaEnemyM > currentDistance)
                        {
                            areaFound.Add(cellReferences[x, y]);
                        }

                    }
                }
            }

            return areaFound;
        }


        public bool IsEnemyInAggroCell(int xEnemy, int yEnemy)
        {

            return cellReferences[xEnemy, yEnemy].aggroCell;
        }


        public List<Transform> RetrievingPossibleMovements(int xEnemy, int yEnemy)
        {
            List<Transform> moves = new List<Transform>();

            moves.Add(cellReferences[xEnemy, yEnemy].transform);

            if (CheckingUpCellExp(xEnemy, yEnemy))
                moves.Add(cellReferences[xEnemy, yEnemy + 1].transform);
            if (CheckingDownCellExp(xEnemy, yEnemy))
                moves.Add(cellReferences[xEnemy, yEnemy - 1].transform);
            if (CheckingLeftCellExp(xEnemy, yEnemy))
                moves.Add(cellReferences[xEnemy - 1, yEnemy].transform);
            if (CheckingRightCellExp(xEnemy, yEnemy))
                moves.Add(cellReferences[xEnemy + 1, yEnemy].transform);

            return moves;
        }


        // Method to follow player on Aggro
        public Transform FindFastestRoute(List<Transform> moves, out int xEnemy, out int yEnemy)
        {
            GameObject playerTemp = Game_Controller.instance.playerLink;

            float min = Mathf.Abs(moves[0].position.x - playerTemp.transform.position.x) + Mathf.Abs(moves[0].position.y - playerTemp.transform.position.y);

            int posFound = 0;

            if (moves.Count > 1)
            {
                for (int i = 1; i < moves.Count; i++)
                {
                    int current = Mathf.RoundToInt(Mathf.Abs(moves[i].position.x - playerTemp.transform.position.x) + Mathf.Abs(moves[i].position.y - playerTemp.transform.position.y));

                    if (current <= min)
                    {
                        min = current;
                        posFound = i;
                    }
                }
            }



            xEnemy = moves[posFound].gameObject.GetComponent<Cell_Interaction>().xCell;
            yEnemy = moves[posFound].gameObject.GetComponent<Cell_Interaction>().yCell;
            

            return moves[posFound];

        }


        //Method to come back on starting position when aggro is wiped
        public Transform FindFastestBackRoute(List<Transform> moves, int xComeBack, int yComeBack, out int xEnemy, out int yEnemy)
        {
            float min = Mathf.Abs(moves[0].position.x - cellReferences[xComeBack, yComeBack].transform.position.x) + Mathf.Abs(moves[0].position.y -
                cellReferences[xComeBack, yComeBack].transform.position.y);

            int posFound = 0;

            if (moves.Count > 1)
            {
                for (int i = 1; i < moves.Count; i++)
                {
                    int current = Mathf.RoundToInt(Mathf.Abs(moves[i].position.x - cellReferences[xComeBack, yComeBack].transform.position.x)
                        + Mathf.Abs(moves[i].position.y - cellReferences[xComeBack, yComeBack].transform.position.y));

                    if (current <= min)
                    {
                        min = current;
                        posFound = i;
                    }
                }
            }



            xEnemy = moves[posFound].gameObject.GetComponent<Cell_Interaction>().xCell;
            yEnemy = moves[posFound].gameObject.GetComponent<Cell_Interaction>().yCell;
            

            return moves[posFound];
        }



        public int CalcEnemyPlayDist(int xEnemy, int yEnemy)
        {
            GameObject playerTemp = Game_Controller.instance.playerLink;
            Vector3 playerPos = playerTemp.GetComponent<playerActions>().whereToGo.position;

            return Mathf.RoundToInt(Mathf.Abs(playerPos.x - cellReferences[xEnemy, yEnemy].gameObject.transform.position.x) +
                Mathf.Abs(playerPos.y - cellReferences[xEnemy, yEnemy].gameObject.transform.position.y));
        }

        public void MakeDamageToPlayer(int damage)
        {
            GameObject playerTemp = Game_Controller.instance.playerLink;
            playerTemp.GetComponent<Player_Controller>().Life -= damage;
        }



        // Methods for the Player Attack
        public void HighlightingAttackRange(int xPlayer, int yPlayer, int range)
        {
            float currentDistance;
            
            SpriteRenderer tileHighlight;

            for (int y = 0; y < cellReferences.GetLength(1); y++)
            {
                for (int x = 0; x < cellReferences.GetLength(0); x++)
                {
                    if (cellReferences[x, y] != null)
                    {
                        currentDistance = Mathf.Abs(cellReferences[x, y].transform.position.x - cellReferences[xPlayer, yPlayer].transform.position.x) +
                            Mathf.Abs(cellReferences[x, y].transform.position.y - cellReferences[xPlayer, yPlayer].transform.position.y);

                        tileHighlight = cellReferences[x, y].tileCell.GetComponent<SpriteRenderer>();

                        if (range >= currentDistance && wallList.Find(z => z.name == tileHighlight.sprite.name) == null && currentDistance != 0)
                        {

                            cellReferences[x, y].GetComponent<SpriteRenderer>().color = Color.yellow;
                            tileHighlight.color = Color.yellow;

                        }
                    }
                }
            }

        }

        public void DelightingAttackRange(int xPlayer, int yPlayer, int range)
        {
            float currentDistance;
            

            for (int y = 0; y < cellReferences.GetLength(1); y++)
            {
                for (int x = 0; x < cellReferences.GetLength(0); x++)
                {
                    if (cellReferences[x, y] != null)
                    {
                        currentDistance = Mathf.Abs(cellReferences[x, y].transform.position.x - cellReferences[xPlayer, yPlayer].transform.position.x) +
                        Mathf.Abs(cellReferences[x, y].transform.position.y - cellReferences[xPlayer, yPlayer].transform.position.y);



                        if (range >= currentDistance)
                        {

                            cellReferences[x, y].GetComponent<SpriteRenderer>().color = Color.white;
                            cellReferences[x, y].tileCell.GetComponent<SpriteRenderer>().color = Color.white;
                        }
                    }
                }
            }

        }









    }
}


