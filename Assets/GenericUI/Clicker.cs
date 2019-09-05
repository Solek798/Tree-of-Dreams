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

