using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmlandLevel : MonoBehaviour
{
    private HashSet<Vector3Int> _lockedCells;
    
    // Start is called before the first frame update
    void Start()
    {
        _lockedCells = new HashSet<Vector3Int>();
    }

    public void LockCell(Vector3Int cellToLock)
    {
        _lockedCells.Add(cellToLock);
    }

    public void UnlockCell(Vector3Int cellToUnlock)
    {
        _lockedCells.Remove(cellToUnlock);
    }

    public bool IsLocked(Vector3Int cell)
    {
        return _lockedCells.Contains(cell);
    }
}
