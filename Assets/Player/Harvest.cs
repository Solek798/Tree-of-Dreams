using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : MonoBehaviour
{
    [SerializeField] private GameObject plant;
    [SerializeField] private Camera cam;
    [SerializeField] private Farmland farmland;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3Int result = new Vector3Int();
        FarmlandLevel targetLevel = null;

        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
        {
            foreach (var level in farmland.GetAllLevels())
            {

                if (level.Validate(hit.collider.gameObject, hit.point, out result))
                {
                    targetLevel = level;
                    level.SetSelector(true, result);
                }
                else
                {
                    level.SetSelector(false, result);
                }
            }
        }
        
        if (Input.GetMouseButtonDown(0) && targetLevel != null)
        {
            Plant(targetLevel, plant, result);
        }
    }
    
    public void Plant(FarmlandLevel level, GameObject plant, Vector3Int cell)
    {

        Vector3 position = level.GetWorldCord(cell);
        
        level.LockCell(cell);

        Instantiate(plant, position, level.transform.rotation);
    }
    
}
