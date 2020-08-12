public interface IShapesProducer
{
    bool HasNotPlacedShapes { get; }
    void SpawnNewShapes();
}