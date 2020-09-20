using System;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    private TileMapCell[,] m_Cells = new TileMapCell[4, 4];

    public static TileMap Instance { get; private set; } = null;

    void Awake()
    {
        Instance = this;
        FindAllCells();
    }

    private void Start()
    {
        CreateNextShapeSet();
    }

    private void FindAllCells()
    {
        int n = 0;
        foreach (Transform child in transform)
        {
            int row = n / 4;
            int col = n % 4;
            n++;

            m_Cells[row, col] = child.GetComponent<TileMapCell>();
            m_Cells[row, col].Position = new Vector2Int(row, col);
        }
    }

    private bool IsPositionValid(int row, int col)
    {
        return 0 <= row && row < 4 && 0 <= col && col < 4;
    }

    public List<Shape> GetClosestNeighbors(Shape shape)
    {
        List<Shape> neighbors = new List<Shape>();

        if (shape.Position != null)
        {
            for (int dRow = -1; dRow <= 1; dRow++)
            {
                for (int dCol = -1; dCol <= 1; dCol++)
                {
                    if ((dRow == 0 || dCol == 0) && (dRow != 0 || dCol != 0))
                    {
                        int row = shape.Position.x + dRow;
                        int col = shape.Position.y + dCol;

                        if (IsPositionValid(row, col))
                            if (!m_Cells[row, col].IsEmpty)
                                neighbors.Add(m_Cells[row, col].shape);
                    }
                }
            }
        }

        return neighbors;
    }

    public List<Shape> GetNeighbors(Shape shape)
    {
        List<Shape> neighbors = new List<Shape>();

        if (shape.Position != null)
        {
            for (int dRow = -1; dRow <= 1; dRow++)
            {
                for (int dCol = -1; dCol <= 1; dCol++)
                {
                    if (dRow != 0 || dCol != 0)
                    {
                        int row = shape.Position.x + dRow;
                        int col = shape.Position.y + dCol;

                        if (IsPositionValid(row, col))
                            neighbors.Add(m_Cells[row, col].shape);
                    }
                }
            }
        }

        return neighbors;
    }

    public bool DropShape(Shape shape)
    {
        // get underlying tile
        TileMapCell cell = GetTileUnderMouse();
        if (cell != null)
        {
            if (cell.DropHere(shape)) // drop successful
            {
                ShapesProducer.Instance.FreeShape(shape);
                CheckoutShapeTriggers();
                CreateNextShapeSet();
                return true;
            }
            else return false;
        }
        else return false;
    }

    // ckecks all types of shapes interactions upon placing them on tile map
    private void CheckoutShapeTriggers()
    {
        TriggerableContext context = new TriggerableContext();

        while (!context.Completed)
        {
            context.Completed = true;
            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                    if (!m_Cells[row, col].IsEmpty)
                        m_Cells[row, col].shape.Trigger(context);
        }

        // check for infinity explosions
        if (context.WillExplode)
        {
            foreach (TileMapCell cell in m_Cells)
                cell.RemoveShape();
        }
        else
        {
            // create Absorption graph and execute it
            AbsorptionGraph absorptionGraph = AbsorptionGraph.FromContext(context);
            absorptionGraph.Execute();
        }

        // Debug.Log("Triggers executed!");
    }

    private void CreateNextShapeSet()
    {
        if (!ShapesProducer.Instance.HasNotPlacedShapes)
        {
            ShapesProducer.Instance.SpawnNewShapes();
        }
    }

    private TileMapCell GetTileUnderMouse()
    {
        foreach (TileMapCell cell in m_Cells)
            if (cell.IsMouseOver)
                return cell;

        return null;
    }

    public TileMapCell GetCellWithShape(Shape shape)
    {
        foreach (TileMapCell cell in m_Cells)
            if (cell.shape == shape)
                return cell;
        return null;
    }
}