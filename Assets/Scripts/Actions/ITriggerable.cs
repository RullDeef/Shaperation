public interface ITriggerable
{
    void Trigger(TriggerableContext context);

    void MakeTriggerAction(AbsorptionGraph.GraphNode node, AbsorptionGraph graph);
}