﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Grid_Manager : MonoBehaviour
{


    public Cell_Interaction[,] cellReferences;

    // Use this for initialization
    void Start()
    {

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
                cellTempReference.transform.position = new Vector3(j - columns / 2, i - rows / 2, 0);

                cellReferences[i, j].cell_i = i;
                cellReferences[i, j].cell_j = j;
            }
        }

    }

    public Transform ReturningStartPosition(Scene level, int entrance)
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

}
