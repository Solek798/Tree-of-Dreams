using System;
using UnityEngine;
using UnityEngine.UI;

public class SleepMenu : MonoBehaviour
{
    [SerializeField] private QuestCollector questCollector = null;
    [SerializeField] private Transform questLayoutGroup = null;
    [SerializeField] private Text todaysEaringsText = null;

    public int TodaysEarings
    {
        get => Convert.ToInt32(todaysEaringsText.text);
        set => value.ToString();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var quest in questCollector.GetAllQuests())
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
