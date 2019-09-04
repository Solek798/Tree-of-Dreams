using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    [SerializeField] private AudioSource audioPlayer;
    
    // Update is called once per frame
    void Update()
    {
        Debug.Log(IsMouseOverButton());
        if (Input.GetMouseButtonUp(0) && IsMouseOverButton())
        {    
            audioPlayer.Play();
        }
    }

    private bool IsMouseOverButton()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        
        Debug.Log(raycastResults.Count);
        
        for (int i = 0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject.GetComponent<Button>() != null)
            {
                Debug.Log("ein button");
                return true;
            }
        }
        return false;
    }
}

