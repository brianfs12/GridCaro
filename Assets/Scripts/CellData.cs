using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CellData
{
    [SerializeField]
    private Color _color;
    [SerializeField]
    private Vector2 _position;
    [SerializeField]
    private GameObject _gridObject;


    public CellData(Color color, Vector2 position, GameObject gridObject)
    {
        _color = color;
        _position = position;
        _gridObject = gridObject;
    }

    public Vector2 Position { get => _position; set => _position = value; }
    public Color Color { get => _color; set => _color = value; }
    public GameObject GridObject { get => _gridObject; set => _gridObject = value; }
}
