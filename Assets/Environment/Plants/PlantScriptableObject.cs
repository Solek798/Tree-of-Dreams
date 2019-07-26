using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantObject", menuName = "ScriptableObjects/Plants", order = 1)]
public class PlantScriptableObject : ScriptableObject
{
    //Plantname
    public string Name;
    //The Time the Plant needs to grow
    public List<int> stateReachTime = new List<int>();
    //The Model of the specific State
    public List<GameObject> stateModel = new List<GameObject>();

    public int buyPrice = 0;
    public int sellPrice = 0;
}
