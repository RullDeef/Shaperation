using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TileMapCell : MonoBehaviour
{
    [SerializeField]
    public Shape shape { get; private set; } = null;

    public Vector2Int Position = new Vector2Int(0, 0);

    public bool IsEmpty => shape == null;
    public bool IsMouseOver { get; private set; } = false;

    public bool DropHere(Shape shape)
    {
        if (IsEmpty)
        {
            this.shape = shape;
            
            // shift it a bit forward
            Vector3 newPosition = transform.position;
            newPosition.z -= 0.5f;
            this.shape.transform.position = newPosition;

            // disable shape dragging behavior
            shape.CanBeDragged = false;

            // setup correct position field
            shape.Position = Position;
            
            return true;
        }
        else
            return false;
    }

    private void OnMouseOver()
    {
        IsMouseOver = true;
        
        // for debugging only
        Quaternion rot = transform.rotation;
        // rot.eulerAngles = new Vector3(0.0f, 0.0f, 10.0f);
        transform.rotation = rot;
    }

    private void OnMouseExit()
    {
        IsMouseOver = false;

        // for debugging only
        Quaternion rot = transform.rotation;
        // rot.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        transform.rotation = rot;
    }

    public void RemoveShape()
    {
        if (!IsEmpty)
        {
            shape.Destroy();
            shape = null;
            // Debug.Log("Shape destroyed!");
        }
    }
}
