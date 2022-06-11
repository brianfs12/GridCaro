using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(GridSystem))]
public class GridSystemEditor : Editor
{
    //Vista, interface, skin de como se daran las ordenes
    private GridSystem gridSystem;
    private VisualTreeAsset visualTreeAsset;
    private StyleSheet styleSheet;
    private VisualElement root;

    private ObjectField gridData;
    private IntegerField rows;
    private IntegerField columns;

    private ScrollView grid;

    private Color currentColor;
    private GameObject currentGridObject;

    private List<ToolElement> toolElements;
    private Button _createGridButton;
    private void OnEnable()
    {
        gridSystem = (GridSystem)target;
        visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/GridTool/GridSystem.uxml");
        styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/GridTool/GridSystem.uss");
        root = new VisualElement();
        visualTreeAsset.CloneTree(root);
        root.styleSheets.Add(styleSheet);

        //Search the variables. Like a gameobject find
        gridData = root.Q<ObjectField>("GridData");
        rows = root.Q<IntegerField>("Rows");
        columns = root.Q<IntegerField>("Columns");
        grid = root.Q<ScrollView>("grid");
        _createGridButton = root.Q<Button>("btnSini");
        gridData.objectType = typeof(GridData);

        gridData.BindProperty(serializedObject.FindProperty("gridData"));
        rows.BindProperty(serializedObject.FindProperty("rows"));
        columns.BindProperty(serializedObject.FindProperty("columns"));
        _createGridButton.clicked += () => gridSystem.CreateGrid();
        currentColor = Color.green;

        toolElements = new List<ToolElement>();
        root.Query("toolsContainer").Children<ToolElement>().ForEach(t =>
        {
            toolElements.Add(t);
            t.RegisterCallback<ClickEvent>(e =>
            {
                currentColor = t.colorAttr;

                currentGridObject = AssetDatabase.LoadAssetAtPath<GameObject>(t.gridobjectAttr);
            });
        });
        currentColor = toolElements[0].colorAttr;
        currentGridObject = AssetDatabase.LoadAssetAtPath<GameObject>(toolElements[0].gridobjectAttr);
        gridSystem.CurrentCellData = new List<CellData>();
        /*
        for (int x = 0; x < gridSystem.Rows; x++)
        {
            for (int y = 0; y < gridSystem.Columns; y++)
            {
                gridSystem.CurrentCellData.Add(new CellData(new Color(0, 0, 0, 0), new Vector2(x, y), null));

            }
        }*/

    }
        

public override VisualElement CreateInspectorGUI()
    {
        for(int x = 0; x < gridSystem.Rows; x++)
        {
            VisualElement row = new VisualElement();
            row.AddToClassList("rows");
            grid.contentContainer.Add(row);
            //crear rows y agregar a el scroll iew
            for (int y = 0; y < gridSystem.Columns; y++)
            {
                //crear columnas y agregar a la row
                CellElement column = new CellElement();
                column.AddToClassList("columns");
                row.Add(column);
                column.posxAttr = x;
                column.posyAttr = y;


                if (gridSystem.GridData != null)
                {
                    CellData cellData = gridSystem.GridData.Cells.Find(item => item.Position == new Vector2(x, y));
                    if (cellData != null)
                    {
                        column.style.backgroundColor = cellData.Color;
                    }

                }

                column.RegisterCallback<ClickEvent>(e =>
                {
                    if (gridSystem.GridData == null)
                    {
                        string path = EditorUtility.SaveFilePanelInProject("Create GridData Object", "GridData", "asset", "GridData created");
                        if(path != null)
                        {
                            GridData griddata = GridData.CreateInstance(gridSystem.Rows, gridSystem.Columns);
                            gridData.value = griddata;
                            gridSystem.GridData = griddata;

                            AssetDatabase.CreateAsset(griddata, path);
                            CellData cellData = griddata.Cells.Find(item => item.Position == new Vector2(column.posxAttr, column.posyAttr));
                            cellData.Color = currentColor;
                            cellData.GridObject = currentGridObject;
                        }
                    }
                    else
                    {
                      
                     
                        CellData cellData = gridSystem.GridData.Cells.Find(item => item.Position == new Vector2(column.posxAttr, column.posyAttr));
                        cellData.Color = currentColor;
                        cellData.GridObject = currentGridObject;
                    }

                    column.colorAttr = currentColor;
                    column.style.backgroundColor = currentColor;

                    EditorUtility.SetDirty(gridSystem.GridData);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                });
                
            }
        }

        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        return root;
    }
}
