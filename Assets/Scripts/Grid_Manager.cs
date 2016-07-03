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
                }
            }

        }
      

        public Transform SettingPlayerPosition(int row, int column)
        {
            return cellReferences[row, column].transform;
        }


        public Transform CheckingUpCell(int row, int column) {

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
    }
}