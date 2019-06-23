using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Hand : MonoBehaviour
{
    [SerializeField] private GameObject[] plants = null;
    [SerializeField] private Farmland farmland = null;
    [SerializeField] private GameObject cropPopUp = null;

    public float plantDistance = 60.0f;
    public ITool currentTool;
    
    // Start is called before the first frame update
    void Start()
    {
        cropPopUp.GetComponent<PopupCropUi>().CloseUiMenu();
        currentTool = GetComponentInChildren<CloudPlow>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        var cell = new Vector3Int();
        FarmlandLevel targetLevel = null;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
           
            foreach (FarmlandLevel level in farmland)
            {

                if (level.HitLevel(hit.collider.gameObject))
                {
                    targetLevel = level;
                    level.ChangeSelector(true, hit.point);
                }
                else
                {
                    level.ChangeSelector(false);
                }
            }
        }
        
        if (Input.GetMouseButtonDown(0) && targetLevel != null)
        {
            var space = targetLevel.Interact();

            if (currentTool.IsUsable(space, transform.parent.position))
                currentTool.Use(space);
            
            /*if (!targetLevel.IsLocked(cell) && 
                (targetLevel.GetWorldCord(cell) - transform.position).sqrMagnitude <= plantDistance)
            {
                _currentTargetLevel = targetLevel;
                _currentCell = cell;
                
                cropPopUp.GetComponent<PopupCropUi>().OpenUiMenu();
                
            }

            var plantState = hit.collider.gameObject.GetComponent<PlantState>();
            
            if (plantState != null && plantState.currentState == 2)
            {
                Harvest(targetLevel, hit.collider.gameObject, cell);
            }*/
        }
    }
    
    /*public void Plant(FarmlandLevel level, GameObject plant, Vector3Int cell)
    {

        Vector3 position = level.GetWorldCord(cell);
        
        level.LockCell(cell);

        var newPlant = Instantiate(plant, position, level.transform.rotation);
        
    }
    
    public void Plant(GameObject plant)
    {
        Plant(_currentTargetLevel, plant, _currentCell);
    }

    public void Harvest(FarmlandLevel level, GameObject plant, Vector3Int cell)
    {
        //Destroy(plant);
        GetComponentInChildren<Inventory>()?.PickUp(plant);
        level.UnlockCell(cell);
    }*/
}
