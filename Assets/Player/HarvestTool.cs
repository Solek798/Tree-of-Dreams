using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestTool : MonoBehaviour
{
    [SerializeField] private GameObject plant;
    [SerializeField] private Farmland farmland;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3Int cell = new Vector3Int();
        FarmlandLevel targetLevel = null;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            foreach (var level in farmland.GetAllLevels())
            {

                if (level.Validate(hit.collider.gameObject, hit.point, out cell))
                {
                    targetLevel = level;
                    level.SetSelector(true, cell);
                }
                else
                {
                    level.SetSelector(false, cell);
                }
            }
        }
        
        if (Input.GetMouseButtonDown(0) && targetLevel != null)
        {
            if (!targetLevel.IsLocked(cell))
            {
                Plant(targetLevel, plant, cell);
            }
            else
            {
                Harvest();
            }
        }
    }
    
    public void Plant(FarmlandLevel level, GameObject plant, Vector3Int cell)
    {

        Vector3 position = level.GetWorldCord(cell);
        
        level.LockCell(cell);

        Instantiate(plant, position, level.transform.rotation);
    }

    public void Harvest()
    {
        
    }
    
}
