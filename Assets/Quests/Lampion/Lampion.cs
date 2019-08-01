using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;


public class Lampion : MonoBehaviour
{
    public GameObject player;
    public float maxDistanceToPlayer = 10f;
    public QuestData questData;
    
    [SerializeField] private GameObject dreamPostOffice = null;
    [SerializeField] private GameObject journalUi = null;
    [SerializeField] private QuestCard questCard = null;
    [SerializeField] private GameObject questPrefab = null;

    private bool _uiOpened;
    private Vector3 _travelTarget;
    
    public Vector3 TravelTarget
    {
        get => _travelTarget;
        set
        {
            _travelTarget = value;
            
            StopCoroutine("Movement");
            StartCoroutine("Movement", _travelTarget);
        }
    }
    
    private void Start()
    {
        _uiOpened = false;
        questData.isJournal = false;
        
    }


    private static void Parent( GameObject parentOb, GameObject childOb )
    {
        childOb.transform.SetParent(parentOb.transform, true);
        childOb.transform.localScale = new Vector3(1, 1, 1);
        
    }
    

    private void LampionActivation()
    {
        
        if (questData.isJournal == false)
        {
            Debug.Log("Test");
            //dreamPostOffice.GetComponent<DreamPostOffice>().QuestAddedToJournal(quest);
            //journalUi.GetComponent<Journal>().QuestAddedToJournal(quest);
            //journalUi.GetComponent<Journal>().slider.value = 1;
            //dreamPostOffice.GetComponent<DreamPostOffice>().slider.value = 1;
            //questData.AddQuestToJournal();
            var newQuest = Instantiate(questPrefab).GetComponent<Quest>();
            newQuest.Initialize(questData);
            questCard.InitializeQuestCard(newQuest);
            
            player.GetComponentInChildren<QuestCollector>()?.AddNewQuest(newQuest);
        }
        
    }

    private void Update()
    {
        var playerInRange = Vector3.Distance(transform.position, player.transform.position);
        
        if (Input.GetKeyDown(KeyCode.E) && 
            playerInRange <= maxDistanceToPlayer && 
            !questCard.gameObject.activeInHierarchy) 
        {
            LampionActivation();
        }


        //activate questcard
    }

    private IEnumerator Movement(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position += (target - transform.position).normalized * 14f *Time.deltaTime; //Vector3.Lerp(transform.position, target, 2f * Time.deltaTime);

            yield return null;
        }
    }
}
