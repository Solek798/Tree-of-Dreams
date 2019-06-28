using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropTarget
{
    bool Handle(GameObject draggable);
}
