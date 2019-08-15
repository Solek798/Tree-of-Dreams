using UnityEngine;

public interface ITool
{
    bool Use(FarmlandSpace space);
    bool IsUsable(FarmlandSpace space);
    
    float MaxUsingDistance { get; }
}
