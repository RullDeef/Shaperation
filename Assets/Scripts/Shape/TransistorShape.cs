using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransistorShape : Shape
{
    public BaseShapeType InputType { get; private set; }
    public BaseShapeType OutputType { get; private set; }

    public SpriteRenderer InputHolder;

    public bool Absorbed = false;

    public void SetupIOTypes(BaseShapeType inputType, BaseShapeType outputType)
    {
        InputType = inputType;
        OutputType = outputType;

        InputHolder.sprite = ShapesRepository.Instance.GetBaseSprite(inputType);
        GetComponent<SpriteRenderer>().sprite = ShapesRepository.Instance.GetBaseSprite(outputType);
    }

    public void SetAbsorbed()
    {
        if (!Absorbed)
        {
            Absorbed = true;
            Destroy(InputHolder.gameObject);
        }
    }

    public bool CanAbsorp(Shape shape)
    {
        if (Absorbed)
            return false;
        
        else if (shape is BaseShape baseShape)
            return InputType == baseShape.Type;

        else if (shape is TransistorShape transistorShape)
            return InputType == transistorShape.OutputType;

        else return false;
    }

    public bool CanAbsorpToExplosion(Shape shape)
    {
        if (Absorbed)
            return false;
        
        else if (shape is TransistorShape transistorShape)
            return !transistorShape.Absorbed
                && InputType == transistorShape.OutputType
                && OutputType == transistorShape.InputType;
        else return false;
    }

    public override void Trigger(TriggerableContext context)
    {
        // Debug.Log($"Transistor shape triggered with {ClosestNeighbors.Count} neighbors!");

        foreach (Shape shape in ClosestNeighbors)
        {
            if (CanAbsorpToExplosion(shape))
            {
                context.WillExplode = true;
            }
            else if (CanAbsorp(shape))
            {
                Absorption absorption = new Absorption();
                absorption.Consumer = this;
                absorption.Consumee = shape;

                if (!context.Absorptions.Contains(absorption))
                {
                    context.Absorptions.Add(absorption);
                    context.Completed = false;
                    // Debug.Log("...and absorbed one of them!");
                }
            }
        }
    }

    public override void MakeTriggerAction(AbsorptionGraph.GraphNode node, AbsorptionGraph graph)
    {
        // Debug.Log($"Make trigger action on transistor (input {InputType}, output {OutputType})!");

        foreach (AbsorptionGraph.GraphNode nextNode in node.connectionsTo)
        {
            if (CanAbsorp(nextNode.shape))
            {
                TileMapCell cell = TileMap.Instance.GetCellWithShape(nextNode.shape);
                if (cell != null)
                {
                    cell.RemoveShape();
                    ScoreTracker.Instance.IncreaseScore(1);
                }
            }
        }

        SetAbsorbed();
    }
}
