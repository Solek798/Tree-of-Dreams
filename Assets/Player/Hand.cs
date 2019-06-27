using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Hand : MonoBehaviour
{
    [SerializeField] private Farmland farmland = null;
    [SerializeField] private Inventory inventory;

    // Update is called once per frame
    private void Update()
    {
        RaycastHit hit;
        var cell = new Vector3Int();
        FarmlandLevel targetLevel = null;
        var currentTool = inventory.SelectedItem?.GetComponent<ITool>();

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), 
            out hit,
            100,
            1 << LayerMask.NameToLayer("Clouds")))
        {
           
            foreach (FarmlandLevel level in farmland)
            {
                
                if (level.HitLevel(hit.collider.gameObject) && 
                    CheckToolInRange(currentTool, hit.point))
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
            

            if (currentTool == null) return;
            
            var space = targetLevel.Interact();

            if (currentTool.IsUsable(space, transform.parent.position))
                currentTool.Use(space);
        }
    }

    private bool CheckToolInRange(ITool tool, Vector3 target)
    {
        return (transform.parent.position - target).sqrMagnitude <=
               (tool?.MaxUsingDistance ?? 0.0f);
    }
}
