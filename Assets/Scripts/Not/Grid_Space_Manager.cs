using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Spacchiamo
{
    public class Grid_Space_Manager : MonoBehaviour
    {

        Cell_Interaction[,] cellsLinking;
        RectTransform parent;
        GridLayoutGroup grid;

        void Awake()
        {
            parent = gameObject.GetComponent<RectTransform>();
            grid = gameObject.GetComponent<GridLayoutGroup>();
        }


        void Start()
        {



        }


        public void PrepareGridSpace(int currentRows, int currentColumns)
        {

            GameObject cellTempReference;


            // Setting the cell size accordingly to rows and columns
            grid.cellSize = new Vector2(parent.rect.width / currentColumns, parent.rect.height / currentRows);


            //Creating the Cells and their references in a Matrix
            cellsLinking = new Cell_Interaction[currentRows, currentColumns];
            cellTempReference = Resources.Load<GameObject>("Cell");

            for (int i = 0; i < currentRows; i++)
            {
                for (int j = 0; j < currentColumns; j++)
                {
                    cellTempReference = Instantiate(cellTempReference);
                    cellTempReference.transform.SetParent(this.transform, false);
                    cellTempReference.name = "Cell " + i + " , " + j;

                    // Saving Cell reference
                    cellsLinking[i, j] = cellTempReference.GetComponent<Cell_Interaction>();
                }
            }




        }
    }
}