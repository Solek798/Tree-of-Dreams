using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;


public class Lampion : MonoBehaviour
{
    public GameObject player;
    public QuestData questData;
    
    [SerializeField] private float maxDistanceToPlayer = 10f;
    [SerializeField] private QuestCard questCard = null;
    [SerializeField] private GameObject questPrefab = null;
    
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
        
        //questData.isJournal = false;
        
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
            questCard != null &&
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
            transform.position += (target - transform.position).normalized * 14f *Time.deltaTime;

            yield return null;
        }
    }

    private void OnQuestCardClosed()
    {
        Destroy(gameObject);
    }
}
