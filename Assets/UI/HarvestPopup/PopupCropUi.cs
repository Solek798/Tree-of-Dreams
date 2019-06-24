using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCropUi : MonoBehaviour
{
    private Vector3Int cell;
    private FarmlandLevel targetLevel;
    private GameObject _plantPrefab = null;

    public void SetPlantPrefab(GameObject prefab)
    {
        _plantPrefab = prefab;
    }

    public GameObject GetPlantPrefab()
    {
        return _plantPrefab;
    }

    public void OpenUiMenu()
    {
        this.gameObject.SetActive(true);
        _plantPrefab = null;
    }

    public void CloseUiMenu()
    {
        this.gameObject.SetActive(false);
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
