using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Shape : MonoBehaviour, IShape, IDraggable, ITriggerable
{
    private void Awake()
    {
        InitDraggable();
    }

    private void Update()
    {
        UpdateDraggable();
    }

    #region IShape

    public Vector2Int Position { get; set; }

    public List<Shape> ClosestNeighbors => TileMap.Instance.GetClosestNeighbors(this);
    public List<Shape> Neighbors => TileMap.Instance.GetNeighbors(this);

    #endregion

    #region IDraggable

    public bool IsDragging { get; private set; }
    public bool CanBeDragged { get; set; } = true;
    private bool m_MouseEntered = false;
    private Vector3 m_DraggingStartPosition;
    private Vector3 m_DraggingCurrentPosition;
    private Plane m_DraggablePlane = new Plane(Vector3.forward, Vector3.zero);

    private void InitDraggable()
    {
        IsDragging = false;
    }

    private void OnMouseEnter()
    {
        m_MouseEntered = true;
    }

    private void OnMouseExit()
    {
        m_MouseEntered = false;
    }

    private void UpdateDraggable()
    {
        if (!CanBeDragged)
            return;
        
        if (Input.GetMouseButtonDown(0) && m_MouseEntered)
        {
            IsDragging = true;
            gameObject.layer = 2;
            m_DraggingStartPosition = transform.position;
            m_DraggingCurrentPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0) && IsDragging)
        {
            if (!TileMap.Instance.DropShape(this))
                transform.position = m_DraggingStartPosition;

            IsDragging = false;
            gameObject.layer = 0;
        }
        else if (IsDragging)
        {
            UpdateDraggablePosition();
            m_DraggingCurrentPosition = Input.mousePosition;
        }

        // destroy upon right click
        // if (Input.GetMouseButtonDown(1) && m_MouseEntered)
        // {
        //     Destroy(gameObject);
        // }
    }

    private void UpdateDraggablePosition()
    {
        Vector3 oldPos = m_DraggingCurrentPosition;
        Vector3 newPos = Input.mousePosition;

        Ray newRay = Camera.main.ScreenPointToRay(newPos);
        Ray oldRay = Camera.main.ScreenPointToRay(oldPos);

        if (m_DraggablePlane.Raycast(oldRay, out float oldEnter)
            && m_DraggablePlane.Raycast(newRay, out float newEnter))
        {
            oldPos = oldRay.GetPoint(oldEnter);
            newPos = newRay.GetPoint(newEnter);

            Vector3 delta = newPos - oldPos;

            transform.position += delta;
        }
    }

    #endregion

    #region ITriggerable

    public virtual void Trigger(TriggerableContext context) {}

    public virtual void MakeTriggerAction(AbsorptionGraph.GraphNode node, AbsorptionGraph graph) {}

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    #endregion
}