using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Hand : MonoBehaviour
{
    [SerializeField] private Farmland farmland = null;
    [SerializeField] private Inventory inventory;
    public float plantDistance = 60.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
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
            var currentTool = inventory.SelectedItem?.GetComponent<ITool>();

            if (currentTool == null) return;
            
            var space = targetLevel.Interact();

            if (currentTool.IsUsable(space, transform.parent.position))
                currentTool.Use(space);
        }
    }
}
