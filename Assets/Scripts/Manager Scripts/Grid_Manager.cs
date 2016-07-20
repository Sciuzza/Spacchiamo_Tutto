using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

namespace Spacchiamo
{
    public class Grid_Manager : MonoBehaviour
    {


        private Cell_Interaction[,] cellReferences;
        public List<Cell_Interaction> cellsAttacked = new List<Cell_Interaction>();


        public GameObject tilesHolder;

        public List<Cell_Interaction> openNodeList = new List<Cell_Interaction>();
        public List<Cell_Interaction> closedNodeList = new List<Cell_Interaction>();
        public int distance;

        List<Sprite> wallList = new List<Sprite>();
        Sprite[] wallSprites;

        GameObject playerTemp;


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
            if (tilesHolder == null)
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

                cellReferences[x, y].GivingPlayerRef(playerTemp);

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

        public bool CheckingUpCellExp(int xEnemy, int yEnemy)
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

        public bool CheckingDownCellExp(int xEnemy, int yEnemy)
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

        public bool CheckingLeftCellExp(int xEnemy, int yEnemy)
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

        public bool CheckingRightCellExp(int xEnemy, int yEnemy)
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


        public Transform GetCellTransform(int x, int y)
        {
            return cellReferences[x, y].transform;
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

                        cellReferences[x, y].inRange = false;

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
                                cellReferences[x, y].GetComponent<SpriteRenderer>().color = Color.white;
                                ChangingAlpha(1.0f, cellReferences[x, y].gameObject);
                            }
                        }
                        else if (Designer_Tweaks.instance.playerLightM == currentDistance)
                        {
                            if (!cellReferences[x, y].isReceivingLight)
                            {
                                cellReferences[x, y].aggroCell = false;
                                cellReferences[x, y].GetComponent<SpriteRenderer>().color = Color.white;
                                ChangingAlpha(0.8f, cellReferences[x, y].gameObject);
                            }
                        }
                        else
                        {
                            if (!cellReferences[x, y].isReceivingLight)
                            {
                                cellReferences[x, y].aggroCell = false;

                                if (GettingAlpha(cellReferences[x, y].gameObject) != 0.0f)
                                {
                                    cellReferences[x, y].GetComponent<SpriteRenderer>().color = Color.white;
                                    ChangingAlpha(0.5f, cellReferences[x, y].gameObject);
                                }
                                else
                                {
                                    cellReferences[x, y].GetComponent<SpriteRenderer>().color = Color.white;
                                    ChangingAlpha(0.0f, cellReferences[x, y].gameObject);
                                }
                            }
                        }
                    }
                }
            }

        }

        public void GettingLightObject(int xFalo, int yFalo)
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
                            cellReferences[x, y].GetComponent<SpriteRenderer>().color = Color.white;
                            ChangingAlpha(1.0f, cellReferences[x, y].gameObject);
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

            if (matchAlpha.faloAlpha != null && !playerTemp.GetComponent<Player_Controller>().attackSelection)
                matchAlpha.faloAlpha.color = cellColor;

        }

        public float GettingAlpha(GameObject cell)
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
            playerTemp = Game_Controller.instance.playerLink;

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


        // A Star ALgorithm
        public void AStarAlgorithm(int xStart, int yStart, int xMoving, int yMoving, int xTarget, int yTarget, out int xEnd, out int yEnd)
        {
            openNodeList = new List<Cell_Interaction>();
            closedNodeList = new List<Cell_Interaction>();

            openNodeList.Add(cellReferences[xStart, yStart]);
            cellReferences[xStart, yStart].gValue = 0;
            cellReferences[xStart, yStart].fValue = 0;

            Cell_Interaction currentNode;
            

            do
            {
               
                
                currentNode = openNodeList.Find(y => y.fValue == openNodeList.Min(x => x.fValue));

                closedNodeList.Add(currentNode);

                distance = RetrieveManhDistfromAtoB(currentNode.xCell, currentNode.yCell, xTarget, yTarget);

                if (distance == 1 || openNodeList.Count == 0)
                    continue;

                openNodeList.Remove(currentNode);


                xMoving = currentNode.xCell;
                yMoving = currentNode.yCell;


                if (CheckingUpCellExp(xMoving, yMoving))
                {
                    if (!closedNodeList.Contains(cellReferences[xMoving, yMoving + 1]))
                    {

                        if (!openNodeList.Contains(cellReferences[xMoving, yMoving + 1]))
                        {
                            openNodeList.Add(cellReferences[xMoving, yMoving + 1]);
                            cellReferences[xMoving, yMoving + 1].parentNode = currentNode;
                            cellReferences[xMoving, yMoving + 1].hValue = RetrieveManhDistfromAtoB(xMoving, yMoving + 1, xTarget, yTarget);
                            cellReferences[xMoving, yMoving + 1].gValue = cellReferences[xMoving, yMoving + 1].parentNode.gValue + 1;
                            cellReferences[xMoving, yMoving + 1].fValue = cellReferences[xMoving, yMoving + 1].hValue + cellReferences[xMoving, yMoving + 1].gValue;
                        }
                        else if (cellReferences[xMoving, yMoving + 1].gValue > cellReferences[xMoving, yMoving].gValue + 1)
                        {
                            cellReferences[xMoving, yMoving + 1].parentNode = currentNode;
                            cellReferences[xMoving, yMoving + 1].gValue = cellReferences[xMoving, yMoving + 1].parentNode.gValue + 1;
                            cellReferences[xMoving, yMoving + 1].fValue = cellReferences[xMoving, yMoving + 1].hValue + cellReferences[xMoving, yMoving + 1].gValue;

                        }
                    }
                }

                if (CheckingDownCellExp(xMoving, yMoving))
                {
                    if (!closedNodeList.Contains(cellReferences[xMoving, yMoving - 1]))
                    {

                        if (!openNodeList.Contains(cellReferences[xMoving, yMoving - 1]))
                        {
                            openNodeList.Add(cellReferences[xMoving, yMoving - 1]);
                            cellReferences[xMoving, yMoving - 1].parentNode = currentNode;
                            cellReferences[xMoving, yMoving - 1].hValue = RetrieveManhDistfromAtoB(xMoving, yMoving - 1, xTarget, yTarget);
                            cellReferences[xMoving, yMoving - 1].gValue = cellReferences[xMoving, yMoving - 1].parentNode.gValue + 1;
                            cellReferences[xMoving, yMoving - 1].fValue = cellReferences[xMoving, yMoving - 1].hValue + cellReferences[xMoving, yMoving - 1].gValue;
                        }
                        else if (cellReferences[xMoving, yMoving - 1].gValue > cellReferences[xMoving, yMoving].gValue + 1)
                        {
                            cellReferences[xMoving, yMoving - 1].parentNode = currentNode;
                            cellReferences[xMoving, yMoving - 1].gValue = cellReferences[xMoving, yMoving - 1].parentNode.gValue + 1;
                            cellReferences[xMoving, yMoving - 1].fValue = cellReferences[xMoving, yMoving - 1].hValue + cellReferences[xMoving, yMoving - 1].gValue;

                        }
                    }
                }

                if (CheckingRightCellExp(xMoving, yMoving))
                {
                    if (!closedNodeList.Contains(cellReferences[xMoving + 1, yMoving]))
                    {

                        if (!openNodeList.Contains(cellReferences[xMoving + 1, yMoving]))
                        {
                            openNodeList.Add(cellReferences[xMoving + 1, yMoving]);
                            cellReferences[xMoving + 1, yMoving].parentNode = currentNode;
                            cellReferences[xMoving + 1, yMoving].hValue = RetrieveManhDistfromAtoB(xMoving + 1, yMoving, xTarget, yTarget);
                            cellReferences[xMoving + 1, yMoving].gValue = cellReferences[xMoving + 1, yMoving].parentNode.gValue + 1;
                            cellReferences[xMoving + 1, yMoving].fValue = cellReferences[xMoving + 1, yMoving].hValue + cellReferences[xMoving + 1, yMoving].gValue;
                        }
                        else if (cellReferences[xMoving + 1, yMoving].gValue > cellReferences[xMoving, yMoving].gValue + 1)
                        {
                            cellReferences[xMoving + 1, yMoving].parentNode = currentNode;
                            cellReferences[xMoving + 1, yMoving].gValue = cellReferences[xMoving + 1, yMoving].parentNode.gValue + 1;
                            cellReferences[xMoving + 1, yMoving].fValue = cellReferences[xMoving + 1, yMoving].hValue + cellReferences[xMoving + 1, yMoving].gValue;

                        }
                    }
                }

                if (CheckingLeftCellExp(xMoving, yMoving))
                {
                    if (!closedNodeList.Contains(cellReferences[xMoving - 1, yMoving]))
                    {

                        if (!openNodeList.Contains(cellReferences[xMoving - 1, yMoving]))
                        {
                            openNodeList.Add(cellReferences[xMoving - 1, yMoving]);
                            cellReferences[xMoving - 1, yMoving].parentNode = currentNode;
                            cellReferences[xMoving - 1, yMoving].hValue = RetrieveManhDistfromAtoB(xMoving - 1, yMoving, xTarget, yTarget);
                            cellReferences[xMoving - 1, yMoving].gValue = cellReferences[xMoving - 1, yMoving].parentNode.gValue + 1;
                            cellReferences[xMoving - 1, yMoving].fValue = cellReferences[xMoving - 1, yMoving].hValue + cellReferences[xMoving - 1, yMoving].gValue;
                        }
                        else if (cellReferences[xMoving - 1, yMoving].gValue > cellReferences[xMoving, yMoving].gValue + 1)
                        {
                            cellReferences[xMoving - 1, yMoving].parentNode = currentNode;
                            cellReferences[xMoving - 1, yMoving].gValue = cellReferences[xMoving - 1, yMoving].parentNode.gValue + 1;
                            cellReferences[xMoving - 1, yMoving].fValue = cellReferences[xMoving - 1, yMoving].hValue + cellReferences[xMoving - 1, yMoving].gValue;

                        }
                    }
                }

            } while (openNodeList.Count != 0 && distance > 1);

            if (openNodeList.Count == 0)
            {
                xEnd = xStart;
                yEnd = yStart;
            }
            else
            {
                Cell_Interaction cellToMove = closedNodeList.Find(x => x.parentNode == cellReferences[xStart, yStart]);
                xEnd = cellToMove.xCell;
                yEnd = cellToMove.yCell;
            }
        }

        public int RetrieveManhDistfromAtoB(int xA, int yA, int xB, int yB)
        {
            return Mathf.RoundToInt(Mathf.Abs(xA - xB) + Mathf.Abs(yA - yB));
        }

        public void RetrievePlayerCoords(out int xPlayer, out int yPlayer)
        {
            xPlayer = playerTemp.GetComponent<playerActions>().xPlayer;
            yPlayer = playerTemp.GetComponent<playerActions>().yPlayer;
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

            int xPlayer = playerTemp.GetComponent<playerActions>().xPlayer;
            int yPlayer = playerTemp.GetComponent<playerActions>().yPlayer;


            return Mathf.RoundToInt(Mathf.Abs(cellReferences[xPlayer, yPlayer].transform.position.x - cellReferences[xEnemy, yEnemy].transform.position.x) +
                Mathf.Abs(cellReferences[xPlayer, yPlayer].transform.position.y - cellReferences[xEnemy, yEnemy].gameObject.transform.position.y));
        }

        public void MakeDamageToPlayer(int damage)
        {
            GameObject playerTemp = Game_Controller.instance.playerLink;
            playerTemp.GetComponent<Player_Controller>().Life -= damage;
        }



        // Methods for the cell HighLighting
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
                            cellReferences[x, y].inRange = true;
                            float tempAlpha = GettingAlpha(cellReferences[x, y].gameObject);
                            cellReferences[x, y].oriColor = cellReferences[x, y].GetComponent<SpriteRenderer>().color;

                            cellReferences[x, y].GetComponent<SpriteRenderer>().color = Color.yellow;
                            ChangingAlpha(tempAlpha, cellReferences[x, y].gameObject);

                            tileHighlight.color = cellReferences[x, y].GetComponent<SpriteRenderer>().color;

                        }
                    }
                }
            }

        }

        public void HighlightingKnockRange(int xPlayer, int yPlayer, int range)
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
                            if (y == yPlayer || x == xPlayer)
                            {
                                cellReferences[x, y].inRange = true;

                                float tempAlpha = GettingAlpha(cellReferences[x, y].gameObject);
                                cellReferences[x, y].oriColor = cellReferences[x, y].GetComponent<SpriteRenderer>().color;

                                cellReferences[x, y].GetComponent<SpriteRenderer>().color = Color.yellow;
                                ChangingAlpha(tempAlpha, cellReferences[x, y].gameObject);

                                tileHighlight.color = cellReferences[x, y].GetComponent<SpriteRenderer>().color;
                            }
                        }
                    }
                }
            }
        }

        public void HighlightingAreaOfEffect(int xCell, int yCell, int area)
        {
            float currentDistance;

            SpriteRenderer tileHighlight;

            for (int y = 0; y < cellReferences.GetLength(1); y++)
            {
                for (int x = 0; x < cellReferences.GetLength(0); x++)
                {
                    if (cellReferences[x, y] != null)
                    {
                        currentDistance = Mathf.Abs(cellReferences[x, y].transform.position.x - cellReferences[xCell, yCell].transform.position.x) +
                            Mathf.Abs(cellReferences[x, y].transform.position.y - cellReferences[xCell, yCell].transform.position.y);

                        tileHighlight = cellReferences[x, y].tileCell.GetComponent<SpriteRenderer>();

                        if (area >= currentDistance && wallList.Find(z => z.name == tileHighlight.sprite.name) == null)
                        {
                            float tempAlpha = GettingAlpha(cellReferences[x, y].gameObject);
                            cellReferences[x, y].stdHighColor = cellReferences[x, y].GetComponent<SpriteRenderer>().color;

                            cellReferences[x, y].GetComponent<SpriteRenderer>().color = Color.red;
                            ChangingAlpha(tempAlpha, cellReferences[x, y].gameObject);

                            tileHighlight.color = cellReferences[x, y].GetComponent<SpriteRenderer>().color;

                            cellsAttacked.Add(cellReferences[x, y]);

                        }
                    }
                }
            }
        }

        public void DelightingAreaOfEffect(int xCell, int yCell, int area)
        {
            float currentDistance;

            SpriteRenderer tileHighlight;

            for (int y = 0; y < cellReferences.GetLength(1); y++)
            {
                for (int x = 0; x < cellReferences.GetLength(0); x++)
                {
                    if (cellReferences[x, y] != null)
                    {
                        currentDistance = Mathf.Abs(cellReferences[x, y].transform.position.x - cellReferences[xCell, yCell].transform.position.x) +
                            Mathf.Abs(cellReferences[x, y].transform.position.y - cellReferences[xCell, yCell].transform.position.y);

                        tileHighlight = cellReferences[x, y].tileCell.GetComponent<SpriteRenderer>();

                        if (area >= currentDistance && wallList.Find(z => z.name == tileHighlight.sprite.name) == null)
                        {

                            cellReferences[x, y].GetComponent<SpriteRenderer>().color = cellReferences[x, y].stdHighColor;
                            tileHighlight.color = cellReferences[x, y].stdHighColor;

                            cellsAttacked.Remove(cellReferences[x, y]);

                        }
                    }
                }
            }
        }


        public void GivingPlayerRef(GameObject player)
        {
            playerTemp = player;
        }

        public List<Cell_Interaction> GettingCellsAttacked()
        {
            return cellsAttacked;
        }

        public int CheckingRelativePosition(int xEnemy, int yEnemy)
        {
            // 0 = player is on the left, 1 = player is on the right, 2 = player is on top, 3 = player is on bot , -1 in theory can't remain -1

            playerActions playerPos = playerTemp.GetComponent<playerActions>();

            if (playerPos.xPlayer < xEnemy && playerPos.yPlayer == yEnemy)
                return 0;
            else if (playerPos.xPlayer > xEnemy && playerPos.yPlayer == yEnemy)
                return 1;
            else if (playerPos.xPlayer == xEnemy && playerPos.yPlayer > yEnemy)
                return 2;
            else if (playerPos.xPlayer == xEnemy && playerPos.yPlayer < yEnemy)
                return 3;
            else
                return -1;

        }
    }
}


