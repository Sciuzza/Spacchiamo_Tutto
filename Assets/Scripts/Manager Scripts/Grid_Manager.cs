using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Spacchiamo
{
    public class Grid_Manager : MonoBehaviour
    {


        private Cell_Interaction[,] cellReferences;
        private List<Cell_Interaction> leftPositions;

        public GameObject playerTemp;

        TileLoader tileLoaderTemp;

        [HideInInspector]
        public static Grid_Manager instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        // Used to Create the Logic Grid in the begin, needs to be scene based
        public void PreparingGridSpace()
        {

            cellReferences = new Cell_Interaction[Designer_Tweaks.instance.level1XWidth, Designer_Tweaks.instance.level1yWidth];
            leftPositions = new List<Cell_Interaction>();

            GameObject cellTemp = Resources.Load<GameObject>("Cell");
            GameObject mapTemp = Resources.Load<GameObject>("Map");
            mapTemp = Instantiate(mapTemp);
            

            for (int y = 0; y < cellReferences.GetLength(1); y++)
            {
               

                for (int x = 0; x < cellReferences.GetLength(0); x++)
                {

                    if (GameObject.Find("Tile(" + (x) + "," + (y) + ")") != null)
                    {

                        cellTemp = Instantiate(cellTemp);
                        cellReferences[x, y] = cellTemp.GetComponent<Cell_Interaction>();

                        cellTemp.name = "Cell " + x + " , " + y;
                        cellTemp.transform.position = new Vector3(x + 0.5f, y + 0.5f, 1);

                        cellReferences[x, y].yCell = y;
                        cellReferences[x, y].xCell = x;

                        cellTemp.transform.SetParent(mapTemp.transform);



                        cellReferences[x, y].tileCell = GameObject.Find("Tile(" + (x) + "," + (y) + ")");

                        
                        SpriteRenderer tileType = cellReferences[x, y].tileCell.GetComponent<SpriteRenderer>();



                        if (tileType.sprite.name.Contains("Bordo"))
                        {
                            cellReferences[x, y].SettingWall();
                        }
                        else
                            cellReferences[x, y].CellFree();
                      
                         
                        
                        // Fog Of War
                        ChangingAlpha(0.0f, cellTemp);
                        ChangingAlpha(0.0f, cellReferences[x, y].tileCell);
                        
                    }
                }
            }
            Debug.Log(Time.realtimeSinceStartup);
        }

        public void PreparingOptimizedGridSpace()
        {
            //tileLoaderTemp = GameObject.Find("TilesLayerHolder").GetComponent<TileLoader>();
            List<TileData> tileReferences = tileLoaderTemp.LoadAllTilesInScene("Tile");

            cellReferences = new Cell_Interaction[Designer_Tweaks.instance.level1XWidth, Designer_Tweaks.instance.level1yWidth];


            GameObject cellTemp = Resources.Load<GameObject>("Cell");
            GameObject mapTemp = Resources.Load<GameObject>("Map");
            mapTemp = Instantiate(mapTemp);

            int x, y;

            Debug.Log(tileReferences.Count);

            for (int i = 0; i < tileReferences.Count; i++)
            {
                cellTemp = Instantiate(cellTemp);

                x = tileReferences[i].cell_x;
                y = tileReferences[i].cell_y;

                cellReferences[x, y] = cellTemp.GetComponent<Cell_Interaction>();

                cellTemp.name = "Cell " + x + " , " + y;
                cellTemp.transform.position = new Vector3(x + 0.5f, y + 0.5f, 1);

                cellReferences[x, y].yCell = y;
                cellReferences[x, y].xCell = x;

                cellTemp.transform.SetParent(mapTemp.transform);



                cellReferences[x, y].tileCell = tileReferences[i].go;


                SpriteRenderer tileType = cellReferences[x, y].tileCell.GetComponent<SpriteRenderer>();



                if (tileType.sprite.name.Contains("Bordo"))
                {
                    cellReferences[x, y].SettingWall();
                }
                else
                    cellReferences[x, y].CellFree();



                // Fog Of War
                ChangingAlpha(0.0f, cellTemp);
                ChangingAlpha(0.0f, cellReferences[x, y].tileCell);

            }
            Debug.Log(Time.realtimeSinceStartup);
        }

        public void GivingTileLoaderRef(GameObject tl)
        {
            tileLoaderTemp = tl.GetComponent<TileLoader>();
        }

      
        // Methods necessary to control if a moving object can effectively move and to Update Occupied Status

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




        public void SwitchingOccupiedStatus(int row, int column)
        {
            if (!cellReferences[row, column].isOccupied)
                cellReferences[row, column].isOccupied = true;
            else
                cellReferences[row, column].isOccupied = false;
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



                        if (Designer_Tweaks.instance.manhDistancePlayer > currentDistance)
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
                                ChangingAlpha(1.0f, cellReferences[x, y].tileCell);
                            }
                        }
                        else if (Designer_Tweaks.instance.manhDistancePlayer == currentDistance)
                        {
                            if (!cellReferences[x, y].isReceivingLight)
                            {
                                cellReferences[x, y].aggroCell = false;
                                ChangingAlpha(0.8f, cellReferences[x, y].gameObject);
                                ChangingAlpha(0.8f, cellReferences[x, y].tileCell);
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
                                    ChangingAlpha(0.5f, cellReferences[x, y].tileCell);
                                }
                                else {
                                    ChangingAlpha(0.0f, cellReferences[x, y].gameObject);
                                    ChangingAlpha(0.0f, cellReferences[x, y].tileCell);
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

                        if (Designer_Tweaks.instance.manhDistanceFalo > currentDistance)
                        {
                            cellReferences[x, y].isReceivingLight = true;
                            ChangingAlpha(1.0f, cellReferences[x, y].gameObject);
                            ChangingAlpha(1.0f, cellReferences[x, y].tileCell);
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


        public int AskingXWidth()
        {
            return cellReferences.GetLength(0);
        }

        public int AskingYWidth()
        {
            return cellReferences.GetLength(1);
        }

        /*
        public void AddingPosition(Cell_Interaction posToAdd)
        {
            if (!leftPositions.Contains(posToAdd))
                leftPositions.Add(posToAdd);
        }
        */
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



                        if (Designer_Tweaks.instance.patrolAreaEnemy1 > currentDistance)
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

        public Transform FindFastestRoute(List<Transform> moves, out int whereI, out int whereJ)
        {

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



            whereI = moves[posFound].gameObject.GetComponent<Cell_Interaction>().yCell;
            whereJ = moves[posFound].gameObject.GetComponent<Cell_Interaction>().xCell;
            SwitchingOccupiedStatus(whereI, whereJ);

            return moves[posFound];

        }

        public Transform FindFastestBackRoute(List<Transform> moves, int whereIComeBack, int whereJComeBack, out int whereI, out int whereJ)
        {
            float min = Mathf.Abs(moves[0].position.x - cellReferences[whereIComeBack, whereJComeBack].transform.position.x) + Mathf.Abs(moves[0].position.y -
                cellReferences[whereIComeBack, whereJComeBack].transform.position.y);

            int posFound = 0;

            if (moves.Count > 1)
            {
                for (int i = 1; i < moves.Count; i++)
                {
                    int current = Mathf.RoundToInt(Mathf.Abs(moves[i].position.x - cellReferences[whereIComeBack, whereJComeBack].transform.position.x)
                        + Mathf.Abs(moves[i].position.y - cellReferences[whereIComeBack, whereJComeBack].transform.position.y));

                    if (current <= min)
                    {
                        min = current;
                        posFound = i;
                    }
                }
            }



            whereI = moves[posFound].gameObject.GetComponent<Cell_Interaction>().yCell;
            whereJ = moves[posFound].gameObject.GetComponent<Cell_Interaction>().xCell;
            SwitchingOccupiedStatus(whereI, whereJ);

            return moves[posFound];
        }

        public int CalcEnemyPlayDist(int row, int column)
        {

            Vector3 playerPos = playerTemp.GetComponent<PMovement>().whereToGo.position;

            return Mathf.RoundToInt(Mathf.Abs(playerPos.x - cellReferences[row, column].gameObject.transform.position.x) +
                Mathf.Abs(playerPos.y - cellReferences[row, column].gameObject.transform.position.y));
        }

        public void MakeDamageToPlayer(int damage)
        {
            playerTemp.GetComponent<Player_Controller>().Life -= damage;
        }


        public void HighlightingAttackRange(int xPlayer, int yPlayer)
        {
            float currentDistance;

            for (int y = 0; y < cellReferences.GetLength(1); y++)
            {
                for (int x = 0; x < cellReferences.GetLength(0); x++)
                {
                    if (cellReferences[y, x] != null)
                    {
                        currentDistance = Mathf.Abs(cellReferences[y, x].transform.position.x - cellReferences[xPlayer, yPlayer].transform.position.x) +
                            Mathf.Abs(cellReferences[y, x].transform.position.y - cellReferences[xPlayer, yPlayer].transform.position.y);



                        if (playerTemp.GetComponent<Ability1>().range == currentDistance)
                        {

                            cellReferences[y, x].GetComponent<SpriteRenderer>().color = Color.yellow;

                        }
                    }
                }
            }

        }

        public void DelightingAttackRange(int xPlayer, int yPlayer)
        {
            float currentDistance;

            for (int y = 0; y < cellReferences.GetLength(1); y++)
            {
                for (int x = 0; x < cellReferences.GetLength(0); x++)
                {
                    if (cellReferences[y, x] != null)
                    {
                        currentDistance = Mathf.Abs(cellReferences[y, x].transform.position.x - cellReferences[xPlayer, yPlayer].transform.position.x) +
                        Mathf.Abs(cellReferences[y, x].transform.position.y - cellReferences[xPlayer, yPlayer].transform.position.y);



                        if (playerTemp.GetComponent<Ability1>().range == currentDistance)
                        {

                            cellReferences[y, x].GetComponent<SpriteRenderer>().color = Color.white;

                        }
                    }
                }
            }

        }



        public Transform GetCellTransform(int row, int column)
        {
            return cellReferences[row, column].transform;
        } 



        

    }
}


/*
                    if (cellReferences[i, j].tileCell == null)
                        cellReferences[i, j].SettingInviWall();
                    else
                    {
                        SpriteRenderer tileType = cellReferences[i, j].tileCell.GetComponent<SpriteRenderer>();
                        Sprite tileTypeCheck = Resources.Load<Sprite>("bordo_inferiore_tiles_pietra");

                        if (tileType.sprite == tileTypeCheck)
                            cellReferences[i, j].SettingWall();
                        else
                        
                            cellReferences[i, j].TemporaryRandom();
                    }
                    */
