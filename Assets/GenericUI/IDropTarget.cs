using UnityEngine;

public interface IDropTarget
{
    bool Handle(GameObject draggable);
}
