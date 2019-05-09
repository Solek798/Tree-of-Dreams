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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                
                foreach (var level in farmland.GetAllLevels())
                {
                    Vector3Int result;
                    if (level.Validate(hit.collider.gameObject, hit.point, out result))
                        Plant(level, plant, result);
                }
            }
        }
    }
    
    public void Plant(FarmlandLevel level, GameObject plant, Vector3Int cell)
    {

        Vector3 position = level.GetWorldCord(cell);
        
        level.LockCell(cell);

        Instantiate(plant, position, level.transform.rotation);
    }
    
}
