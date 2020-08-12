using UnityEngine;

public enum BaseShapeType
{
    Square,
    Circle,
    Triangle
}

public class BaseShape : Shape
{
    private BaseShapeType m_Type;

    public BaseShapeType Type
    {
        get => m_Type;
        set
        {
            m_Type = value;
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = ShapesRepository.Instance.GetBaseSprite(m_Type);
        }
    }

    public override void Destroy()
    {
        Debug.Log("Base Destroyed!");
        MonoBehaviour.Destroy(gameObject);
    }
}