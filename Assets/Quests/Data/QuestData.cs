using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "QuestObject", menuName = "ScriptableObjects/Quest", order = 2)]
public class QuestData : ScriptableObject
{
    //Questname
    public new string name;

    [TextArea] public string questDescription;

    [TextArea] public string fullfillText;

    [TextArea] public string missingText;

    public Sprite questNPCImage;
    public Sprite roundQuestNPCImage;

    public bool isJournal = false;

    public int rewardDreamEssence;
    

    //Requirements
    public List<GameObject> requirements = new List<GameObject>();
    

    public void AddQuestToJournal()
    {
        isJournal = true;
    }
}