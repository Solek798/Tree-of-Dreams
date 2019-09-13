using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    [SerializeField] private Texture2D cursorNormal = null;
    [SerializeField] private Texture2D cursorClick = null;
    
    [SerializeField] private AudioSource audioPlayer = null;

    private void Start()
    {
        Cursor.SetCursor(cursorNormal, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
        
        
        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorNormal, Vector2.zero, CursorMode.Auto);
            if (IsMouseOverButton())
            {
                audioPlayer.Play();                
            }
        }

        else if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorClick, Vector2.zero, CursorMode.Auto);        }
    }

    private bool IsMouseOverButton()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        
        for (int i = 0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject.GetComponent<Button>() != null ||
                raycastResults[i].gameObject.GetComponent<Toggle>() != null)
            {
                return true;
            }
        }
        return false;
    }
}

