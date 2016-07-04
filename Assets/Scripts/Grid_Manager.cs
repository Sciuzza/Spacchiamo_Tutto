using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Spacchiamo
{
    public class Grid_Manager : MonoBehaviour
    {


        private Cell_Interaction[,] cellReferences;
       

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
            GameObject cellTemp = Resources.Load<GameObject>("Cell");
            GameObject mapTemp = Resources.Load<GameObject>("Map");
            mapTemp = Instantiate(mapTemp);


            for (int i = 0; i < cellReferences.GetLength(0); i++)
            {
                for (int j = 0; j < cellReferences.GetLength(1); j++)
                {
                    cellTemp = Instantiate(cellTemp);
                    cellReferences[i, j] = cellTemp.GetComponent<Cell_Interaction>();

                    cellTemp.name = "Cell " + i + " , " + j;
                    cellTemp.transform.position = new Vector3(j - cellReferences.GetLength(1) / 2, i - cellReferences.GetLength(0) / 2, 1);

                    cellReferences[i, j].cell_i = i;
                    cellReferences[i, j].cell_j = j;

                    cellTemp.transform.SetParent(mapTemp.transform);

                    // Fog Of War
                    ChangingAlpha(0.0f, cellTemp);
                }
            }

        }

        // Method to retrieve the transform position necessary for the player to be placed into
        public Transform SettingPlayerPosition(int row, int column)
        {
            return cellReferences[row, column].transform;
        }

        
        // Methods necessary to control if a moving object can effectively move

        public Transform CheckingUpCell(int row, int column)
        {

            if (row + 1 < cellReferences.GetLength(0))
                return cellReferences[row + 1, column].transform;
            else
                return null;

        }

        public Transform CheckingDownCell(int row, int column)
        {

            if (row - 1 >= 0)
                return cellReferences[row - 1, column].transform;
            else
                return null;

        }

        public Transform CheckingLeftCell(int row, int column)
        {

            if (column - 1 >= 0)
                return cellReferences[row, column - 1].transform;
            else
                return null;

        }

        public Transform CheckingRightCell(int row, int column)
        {

            if (column + 1 < cellReferences.GetLength(1))
                return cellReferences[row, column + 1].transform;
            else
                return null;

        }

        
        //Method to Retrieve Light Effect around the player

        public void GettingLight(int row, int column)
        {
            
            float currentDistance;

            for (int i = 0; i < cellReferences.GetLength(0); i++)
            {
                for (int j = 0; j < cellReferences.GetLength(1); j++)
                {

                    currentDistance = Mathf.Abs(cellReferences[i, j].transform.position.x - cellReferences[row, column].transform.position.x) +
                        Mathf.Abs(cellReferences[i, j].transform.position.y - cellReferences[row, column].transform.position.y);

                    if (Designer_Tweaks.instance.manhDistance > currentDistance)
                    {
                        ChangingAlpha(1.0f, cellReferences[i, j].gameObject);
                    }
                    else if (Designer_Tweaks.instance.manhDistance == currentDistance)
                    {
                        ChangingAlpha(0.8f, cellReferences[i, j].gameObject);
                    }
                    else
                    {
                        if (GettingAlpha(cellReferences[i, j].gameObject) != 0.0f)
                            ChangingAlpha(0.5f, cellReferences[i, j].gameObject);
                        else
                            ChangingAlpha(0.0f, cellReferences[i, j].gameObject);
                    }
                }
            }

        }

        
        // Methods to manage cell alpha

        private void ChangingAlpha(float alphaLevel, GameObject cell)
        {
            Color cellColor = cell.GetComponent<SpriteRenderer>().color;
            cellColor.a = alphaLevel;
            cell.GetComponent<SpriteRenderer>().color = cellColor;
        }
       
        private float GettingAlpha(GameObject cell)
        {
            return cell.GetComponent<SpriteRenderer>().color.a;
        }
    }
}