using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Vector3[] targets = null;
    private int _tempIndex = 0;
    
    

    public void SendLampinion()
    {
        var children = this.gameObject
            .GetAllChildren()
            .Select(t => t.GetComponent<Lampion>())
            .Where(t => t != null)
            .ToArray();
        
        if (children.Length == 0) return;

        var child = children[Random.Range(0, children.Length)];

        child.transform.SetParent(transform.parent, true);
        
        
        child.TravelTarget = targets[_tempIndex];
        _tempIndex++;
    }
}
