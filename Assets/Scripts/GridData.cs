using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GridData : ScriptableObject
{
    [SerializeField]
    private List<CellData> _cells;


    public List<CellData> Cells { get => _cells; set => _cells = value; }

    public static GridData CreateInstance(int x, int y)
    {
        GridData instance = ScriptableObject.CreateInstance<GridData>();
        instance.Cells = new List<CellData>();

        for(int posX = 0; posX < x; posX++)
        {
            for(int posY= 0; posY < y; posY++)
            {
                instance.Cells.Add(new CellData(Vector4.zero, new Vector2(posX, posY), null));
            }
        }

        return instance;
    }
}
