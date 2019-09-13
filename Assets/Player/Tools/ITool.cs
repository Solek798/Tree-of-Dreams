using System.Collections;

public interface ITool
{
    IEnumerator Use(FarmlandSpace space);
    bool IsUsable(FarmlandSpace space);
    
    float MaxUsingDistance { get; }
}
