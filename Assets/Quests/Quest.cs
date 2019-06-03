using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "QuestObject", menuName = "ScriptableObjects/Quest", order = 2)]
public class Quest : ScriptableObject
{
    //Questname
    public new string name;

    public string questDescription;

    public Sprite questNPCImage;
    
    

    //Requirements
    public List<GameObject> requirements = new List<GameObject>();
}