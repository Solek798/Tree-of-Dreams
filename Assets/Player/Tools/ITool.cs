using UnityEngine;

public interface ITool
{
    bool Use(FarmlandSpace space);
    bool IsUsable(FarmlandSpace space, Vector3 usagePoint);
    
    float MaxUsingDistance { get; }
}
