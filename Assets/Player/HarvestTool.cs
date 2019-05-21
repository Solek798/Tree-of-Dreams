using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestTool : MonoBehaviour
{
    [SerializeField] private GameObject plant = null;
    [SerializeField] private Farmland farmland = null;
    [SerializeField] private GameObject CropPopUp = null;

    public float plantDistance = 60.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        CropPopUp.GetComponent<PopupCropUi>().CloseUiMenu();
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
            if (!targetLevel.IsLocked(cell) && 
                (targetLevel.GetWorldCord(cell) - transform.position).sqrMagnitude <= plantDistance)
            {
                //TODO give buttonclick the targetLevel 
                //Plant(targetLevel, plant, cell);
                CropPopUp.GetComponent<PopupCropUi>().SetFarmlandlevel(targetLevel);
                CropPopUp.GetComponent<PopupCropUi>().SetCellStats(cell);
                CropPopUp.GetComponent<PopupCropUi>().OpenUiMenu();
                
            }

            var plantState = hit.collider.gameObject.GetComponent<PlantState>();
            
            if (plantState != null && plantState.currentState == 2)
            {
                Harvest(targetLevel, hit.collider.gameObject, cell);
            }
        }
    }
    
    public void Plant(FarmlandLevel level, GameObject plant, Vector3Int cell)
    {

        Vector3 position = level.GetWorldCord(cell);
        
        level.LockCell(cell);

        var newPlant = Instantiate(plant, position, level.transform.rotation);
        
    }

    public void Harvest(FarmlandLevel level, GameObject plant, Vector3Int cell)
    {
        Destroy(plant);
        level.UnlockCell(cell);
    }

    
    
    //TODO buttonClick function
    public void ButtonClick()
    {
        var targetLevel = CropPopUp.GetComponent<PopupCropUi>().GetFarmlandlevel();
        var cell = CropPopUp.GetComponent<PopupCropUi>().GetCellStats();
        plant = GetComponent<PopUiButtons>().gameObject;
        
        
        Plant(targetLevel, plant, cell);
    }

    public void ExitButtonClick()
    {
        CropPopUp.GetComponent<PopupCropUi>().CloseUiMenu();
    }
}
