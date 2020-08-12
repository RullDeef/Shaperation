using System.Collections.Generic;
using UnityEngine;

public interface IShape
{
    Vector2Int Position { get; }

    List<Shape> ClosestNeighbors { get; }
    List<Shape> Neighbors { get; }
}