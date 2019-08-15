using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Hand : MonoBehaviour
{
    [SerializeField] private Farmland farmland = null;
    [SerializeField] private Inventory inventory = null;
    [SerializeField] private int raycastDistance = 100;
    [SerializeField] private string collisionMaskName = "";

    private int _collisionMask = 0;

    private void Start()
    {
        _collisionMask = 1 << LayerMask.NameToLayer(collisionMaskName);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            EditorApplication.isPaused = true;
        }
        
        //Debug.Log(transform.parent.position);
        //Debug.Log(transform.parent.position);
        
        if (!PlayerScriptor.Instance.AllowInteracting)
            return;
        
        FarmlandLevel targetLevel = null;
        var currentTool = inventory.SelectedItem?.GetComponent<ITool>();

        if (Physics.Raycast(
            Camera.main.ScreenPointToRay(Input.mousePosition), 
            out var hit,
            raycastDistance,
            _collisionMask))
        {
            //Debug.Log("hit: " + (transform.parent.position - hit.point));
            foreach (FarmlandLevel level in farmland)
            {
                //Debug.Log(transform.parent.position);
                //Debug.Log(level.GetCellPosition(transform.parent.position));
                if (level.HitLevel(hit.collider.gameObject) && 
                    CheckToolInRange(currentTool, level.GetCellPosition(hit.point)))
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

            if (currentTool.IsUsable(space))
                currentTool.Use(space);
        }
    }

    private bool CheckToolInRange(ITool tool, Vector3 cell)
    {
        //Debug.Log("Cell: " + (transform.parent.position - cell));
        return (transform.parent.position - cell).sqrMagnitude <=
               (tool?.MaxUsingDistance ?? 0.0f);
    }
}
