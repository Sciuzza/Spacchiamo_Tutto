using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Spacchiamo
{
    public class Grid_Manager : MonoBehaviour
    {

        Scene_Manager sceneManagerLinking;
        public Cell_Interaction[,] cellReferences;

        // Use this for initialization
        void Awake()
        {
            sceneManagerLinking = GameObject.Find("Scene Manager").GetComponent<Scene_Manager>();
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void PreparingGridSpace(int rows, int columns)
        {

            cellReferences = new Cell_Interaction[rows, columns];
            GameObject cellTempReference = Resources.Load<GameObject>("Cell");


            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    cellTempReference = Instantiate(cellTempReference);
                    cellReferences[i, j] = cellTempReference.GetComponent<Cell_Interaction>();

                    cellTempReference.name = "Cell " + i + " , " + j;
                    cellTempReference.transform.position = new Vector3(j - columns / 2, i - rows / 2, 1);

                    cellReferences[i, j].cell_i = i;
                    cellReferences[i, j].cell_j = j;
                }
            }

        }

        public Transform ReturningStartPosition()
        {
            /*
            if (level.buildIndex == 0)
            {
                if (entrance == 0)
                    return cellReferences[0, 1].transform;
                else if (entrance == 1)
                    return cellReferences[0, 1].transform;
                else if (entrance == 1)
                    return cellReferences[0, 1].transform;
                else
                    return cellReferences[0, 1].transform;
            }
            else
            */


            return cellReferences[0, 1].transform;
        }

        public Cell_Interaction GetPlayerPosition()
        {
            return cellReferences[0, 1];
        }

        public Cell_Interaction CheckingUpCell(Cell_Interaction playerCell) {

            if (playerCell.cell_i + 1 < cellReferences.GetLength(0))
                return cellReferences[playerCell.cell_i + 1, playerCell.cell_j];
            else
                return null;

        }

        public Cell_Interaction CheckingDownCell(Cell_Interaction playerCell)
        {

            if (playerCell.cell_i - 1 >= 0)
                return cellReferences[playerCell.cell_i - 1, playerCell.cell_j];
            else
                return null;

        }

        public Cell_Interaction CheckingLeftCell(Cell_Interaction playerCell)
        {

            if (playerCell.cell_j - 1 >= 0)
                return cellReferences[playerCell.cell_i, playerCell.cell_j - 1];
            else
                return null;

        }

        public Cell_Interaction CheckingRightCell(Cell_Interaction playerCell)
        {

            if (playerCell.cell_j + 1 < cellReferences.GetLength(1))
                return cellReferences[playerCell.cell_i, playerCell.cell_j + 1];
            else
                return null;

        }
    }
}