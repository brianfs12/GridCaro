using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    //Logica de data para decirle que va a controllar el jugador

    [SerializeField]
    private GridData gridData;
    [SerializeField]
    private int rows;
    [SerializeField]
    private int columns;

    [SerializeField]
    private List<CellData> currentCellData;

    public void CreateGrid() 
    {
        Debug.Log("asda");
        if(gridData != null)
        {
            for(int x=0;x< rows;x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    CellData cellData = gridData.Cells.Find(item => item.Position == new Vector2(x, y));
                    if(cellData != null)
                    {
                        if(cellData.GridObject != null)
                        {
                            Transform tObject = Instantiate(cellData.GridObject, transform, true).transform;
                            tObject.localPosition = new Vector3(x, 0, y);
                        }
                    }
                }
            }
        }
    }
    public int Rows { get => rows; set { rows = value; } }
    public int Columns { get => columns; set { columns = value; } }

    public GridData GridData { get => gridData; set => gridData = value; }
    public List<CellData> CurrentCellData { get => currentCellData; set => currentCellData = value; }

}
