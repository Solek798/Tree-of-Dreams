using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIDragger : MonoBehaviour
{
    public const string DRAG = "UIDragable";
    public const string DROP = "UIDropable";

    private bool _isDragging = false;
    private GameObject _draggingObject;
    private Transform _origParentTransform;

    // TODO(FK): wait for UI Solution
    public Canvas inventoryCanvas;
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // TODO(FK): Finish Drag 'n' drop mechanic
        //return;
        if (_isDragging)
        {
            _draggingObject.transform.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _draggingObject = GetDragableObject();
            
            if (_draggingObject != null)
            {
                Drag();
            }  
        }

        if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            var o = DnDRaycaster.Raycast(inventoryCanvas, DROP);
            Drop(o);
        }
    }

    private GameObject GetDragableObject()
    {
        return DnDRaycaster.Raycast(inventoryCanvas, DRAG);
    }

    private void Drag()
    {
        _origParentTransform = _draggingObject.transform.parent;
        _draggingObject.transform.SetParent(_draggingObject.GetComponentInParent<Canvas>().transform);
        _draggingObject.BroadcastMessage("OnDrag", SendMessageOptions.DontRequireReceiver);
        _isDragging = true;
    }

    private void Drop(GameObject target)
    {
        
        _draggingObject.BroadcastMessage("OnDrop", SendMessageOptions.DontRequireReceiver);
        
        var dropTarget = target?.GetComponentInChildren<IDropTarget>();
        
        if (dropTarget == null || !dropTarget.Handle(_draggingObject))
        {
            _draggingObject.transform.SetParent(_origParentTransform, false);
            _draggingObject.transform.localPosition = Vector2.zero;
        }
        
        _isDragging = false;
        _draggingObject = null;
        _origParentTransform = null;
    }

    public class DnDRaycaster
    {
        public static GameObject Raycast(Canvas targetCanvas, string tagToCompare)
        {

            return targetCanvas
                .gameObject
                .GetAllChildren()
                .Where(t => t.CompareTag(tagToCompare))
                .FirstOrDefault(t => GetGlobalRect((RectTransform) t.transform).Contains(Input.mousePosition));
        }

        private static Rect GetGlobalRect(RectTransform rectTransform)
        {
            
            var retVal = new Rect(rectTransform.rect);
            retVal.position += (Vector2)rectTransform.position;
            
            return retVal;
        }
    }
}
