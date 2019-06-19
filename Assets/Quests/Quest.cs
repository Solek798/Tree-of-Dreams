using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "QuestObject", menuName = "ScriptableObjects/Quest", order = 2)]
public class Quest : ScriptableObject
{
    //Questname
    public new string name;

    [TextArea] public string questDescription;

    public Sprite questNPCImage;

    public bool isJournal = false;
    

    //Requirements
    public List<GameObject> requirements = new List<GameObject>();

    public bool AddQuestToJournal()
    {
        return isJournal = true;
    }
}