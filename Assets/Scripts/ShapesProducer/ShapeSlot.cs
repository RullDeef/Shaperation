using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSlot : MonoBehaviour
{
    public Shape Shape { get; private set; } = null;

    public bool IsEmpty => Shape == null;

    public void SpawnRandomShape()
    {
        if (IsEmpty)
        {
            Shape = ShapesRepository.Instance.CreateRandom();
            Shape.transform.position = transform.position;
        }
        else
        {
            Debug.Log("Trying to create new random shape. Shape slot is not empty!");
        }
    }

    public void RemoveShape()
    {
        Shape = null;
    }
}
