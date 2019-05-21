using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCropUi : MonoBehaviour
{
    private Vector3Int cell;
    private FarmlandLevel targetLevel;
    
    
    public void OpenUiMenu()
    {
        this.GetComponent<Canvas>().enabled = true;
    }

    public void CloseUiMenu()
    {
        this.GetComponent<Canvas>().enabled = false;
    }


    public void SetCellStats(Vector3Int _cell)
    {
        cell = _cell;
    }

    public void SetFarmlandlevel(FarmlandLevel _targetLevel)
    {
        targetLevel = _targetLevel;
    }

    public Vector3Int GetCellStats()
    {
        return cell;
    }

    public FarmlandLevel GetFarmlandlevel()
    {
        return targetLevel;
    }
}
