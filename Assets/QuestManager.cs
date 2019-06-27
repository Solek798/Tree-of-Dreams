using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Vector3[] targets;
    private int _tempIndex = 0;
    
    

    public void SendLampinion()
    {
        var children = this.gameObject.GetAllChildren();
        
        if (children.Length == 0) return;

        var child = children[Random.Range(0, children.Length)];

        if (child == gameObject) return;

        child.transform.SetParent(transform.parent, true);
        
        //child.SetActive(true);
        child.GetComponent<Lampion>().TravelTarget = targets[_tempIndex];
        _tempIndex++;
    }
}
