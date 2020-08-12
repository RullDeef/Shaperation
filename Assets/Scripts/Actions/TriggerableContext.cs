using System.Collections.Generic;

public class TriggerableContext
{
    public bool Completed = false;
    public bool WillExplode = false;
    public List<Absorption> Absorptions = new List<Absorption>();
}