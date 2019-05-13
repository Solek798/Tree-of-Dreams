using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantObject", menuName = "ScriptableObjects/Plants", order = 1)]
public class PlantScriptableObject : ScriptableObject
{
    public string Name;
    public List<int> StateReachTime = new List<int>();
    public List<GameObject> StateModel = new List<GameObject>();

}
