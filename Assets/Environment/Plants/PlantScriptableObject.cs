using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantObject", menuName = "ScriptableObjects/Plants", order = 1)]
public class PlantScriptableObject : ScriptableObject
{
    //Plantname
    public string Name;
    //The Time the Plant needs to grow
    public List<int> StateReachTime = new List<int>();
    //The Model of the specific State
    public List<GameObject> StateModel = new List<GameObject>();

}
