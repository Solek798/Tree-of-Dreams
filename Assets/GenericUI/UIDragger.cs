﻿using System.Collections;
using System.Linq;
using UnityEngine;

public class UIDragger : MonoBehaviour
{
    // Set to public for potential further usage
    // ReSharper disable once MemberCanBePrivate.Global
    public const string DRAG = "UIDragable";
    // ReSharper disable once MemberCanBePrivate.Global
    public const string DROP = "UIDropable";

    [SerializeField] private float timeBeforeDrag = 0.25f;
    [SerializeField] private Canvas[] registeredCanvases = null;
    
    private bool _isDragging = false;
    private bool _prefacingDrag = false;
    private GameObject _draggingObject;
    private Transform _origParentTransform;
    
    


    private void Update()
    {
        if (_isDragging)
        {
            _draggingObject.transform.position = Input.mousePosition;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            _draggingObject = GetInteractableObject(DRAG);
            
            if (_draggingObject != null)
            {
                StartCoroutine(nameof(PrefaceDrag));
            }  
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_prefacingDrag)
            {
                StopCoroutine(nameof(PrefaceDrag));
                _prefacingDrag = false;
            }
            
            if (_isDragging)
            {
                StopCoroutine(nameof(PrefaceDrag));
                _prefacingDrag = false;
                
                var objectWhereToDrop = GetInteractableObject(DROP);
                Drop(objectWhereToDrop);
            }
        }
    }

    private GameObject GetInteractableObject(string kind)
    {
        GameObject retVal = null;
        
        foreach (var canvas in registeredCanvases)
        {
            if (!canvas.isActiveAndEnabled) continue;
            
            retVal = DnDRaycaster.Raycast(canvas, kind);
            
            if (retVal != null) break;
        }
        
        return retVal;
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

    private IEnumerator PrefaceDrag()
    {
        _prefacingDrag = true;
        yield return new WaitForSeconds(timeBeforeDrag);
        _prefacingDrag = false;
        
        Drag();
    }

    // Set to public for potential further usage
    // ReSharper disable once MemberCanBePrivate.Global
    public static class DnDRaycaster
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
