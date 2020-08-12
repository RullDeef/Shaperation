using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesRepository : MonoBehaviour
{
    public static ShapesRepository Instance;

    // setup from editor
    public BaseShape CommonBaseShape;
    public TransistorShape CommonTransistorShape;

    public Sprite SquareBaseSprite;
    public Sprite CircleBaseSprite;
    public Sprite TriangleBaseSprite;

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetBaseSprite(BaseShapeType type)
    {
        switch (type)
        {
            case BaseShapeType.Square:
                return SquareBaseSprite;
            case BaseShapeType.Circle:
                return CircleBaseSprite;
            case BaseShapeType.Triangle:
                return TriangleBaseSprite;
        }
        return null;
    }

    public Shape CreateRandom()
    {
        if (Random.Range(0.0f, 1.0f) < 1.0f / 3.0f)
            return CreateRandomBaseShape();
        else return CreateRandomTransistorShape();
    }

    private Shape CreateRandomTransistorShape()
    {
        TransistorShape shape = Instantiate(CommonTransistorShape, Vector3.zero, Quaternion.identity);

        BaseShapeType[] types = {
            BaseShapeType.Square,
            BaseShapeType.Circle,
            BaseShapeType.Triangle
        };

        BaseShapeType inputType = types[Random.Range(0, types.Length)];
        BaseShapeType outputType = types[Random.Range(0, types.Length)];

        shape.SetupIOTypes(inputType, outputType);

        return shape;
    }

    private BaseShape CreateRandomBaseShape()
    {
        BaseShape shape = Instantiate(CommonBaseShape, Vector3.zero, Quaternion.identity);

        BaseShapeType[] types = {
            BaseShapeType.Square,
            BaseShapeType.Circle,
            BaseShapeType.Triangle
        };

        shape.Type = types[Random.Range(0, types.Length)];
        return shape;
    }
}
