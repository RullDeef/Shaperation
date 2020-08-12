using System.Collections.Generic;

public struct Absorption
{
    public Shape Consumer;
    public Shape Consumee;

    public override bool Equals(object obj)
    {
        return obj is Absorption absorption &&
               EqualityComparer<Shape>.Default.Equals(Consumer, absorption.Consumer) &&
               EqualityComparer<Shape>.Default.Equals(Consumee, absorption.Consumee);
    }

    public override int GetHashCode()
    {
        int hashCode = 2035172253;
        hashCode = hashCode * -1521134295 + EqualityComparer<Shape>.Default.GetHashCode(Consumer);
        hashCode = hashCode * -1521134295 + EqualityComparer<Shape>.Default.GetHashCode(Consumee);
        return hashCode;
    }
}