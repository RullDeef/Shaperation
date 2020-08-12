using System;
using UnityEngine;

public class ShapesProducer : MonoBehaviour, IShapesProducer
{
    private ShapeSlot[] m_Slots = new ShapeSlot[3];

    public static ShapesProducer Instance;

    void Awake()
    {
        Instance = this;
        FindAllShapeSlots();
    }

    private void FindAllShapeSlots()
    {
        int i = 0;

        foreach (Transform child in transform)
        {
            ShapeSlot slot = child.GetComponent<ShapeSlot>();
            m_Slots[i] = slot;
            i++;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            SpawnNewShapes();
        }
    }

    public bool HasNotPlacedShapes
    {
        get
        {
            foreach (ShapeSlot slot in m_Slots)
                if (!slot.IsEmpty)
                    return true;
            return false;
        }
    }

    public void SpawnNewShapes()
    {
        foreach (ShapeSlot slot in m_Slots)
        {
            slot.SpawnRandomShape();
        }
    }

    public void FreeShape(Shape shape)
    {
        foreach (ShapeSlot slot in m_Slots)
        {
            if (slot.Shape == shape)
            {
                slot.RemoveShape();
            }
        }
    }
}
