using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragger : MonoBehaviour
{

    private bool _isDragging = false;
    private GameObject _draggingObject;
    private Transform _origPos;
    private List<RaycastResult> _results;
    
    // Start is called before the first frame update
    private void Start()
    {
        _results = new List<RaycastResult>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isDragging)
        {
            _draggingObject.transform.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _draggingObject = GetDragableObject();
            print(_draggingObject);

            if (_draggingObject != null)
            {
                _origPos = _draggingObject.transform.parent;
                _draggingObject.transform.parent = _draggingObject.GetComponentInParent<Canvas>().transform;
                _isDragging = true;
            }  
        }

        if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            _draggingObject.transform.parent = _origPos;
            _draggingObject.transform.localPosition = Vector2.zero;
            _isDragging = false;
        }
    }

    private GameObject GetDragableObject()
    {
        var pointer = new PointerEventData(EventSystem.current) {position = Input.mousePosition};

        EventSystem.current.RaycastAll(pointer, _results);

        if (_results.Count == 0)
            return null;
        
        return _results
                .Select(t => t.gameObject)
                .FirstOrDefault(t => t.CompareTag("UIDragable"));
    }
}
