using System.Collections;
using UnityEngine;


public class Lampion : MonoBehaviour
{
    public GameObject player;
    public QuestData questData;
    
    [SerializeField] private float maxDistanceToPlayer = 10f;
    [SerializeField] private QuestCard questCard = null;
    [SerializeField] private GameObject questPrefab = null;
    [SerializeField] private GameObject floatingTextPrefab;
    
    private Vector3 _travelTarget;
    private GameObject hotkeyText;
    private Vector3 offset = new Vector3(0,4,0);
    
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

        if (playerInRange <= maxDistanceToPlayer)
        {
            if (!transform.GetComponentInChildren<TextMesh>())
            {
                hotkeyText = Instantiate(floatingTextPrefab, transform.position + offset, Quaternion.identity, transform);
            }

            hotkeyText.transform.LookAt(Camera.main.transform);
            hotkeyText.transform.Rotate(0,180,0);
            
            if (Input.GetKeyDown(KeyCode.E) && 
                questCard != null &&
                !questCard.gameObject.activeInHierarchy) 
            {
                LampionActivation();
            }
        }
        else if (hotkeyText != null)
        {
            Destroy(hotkeyText);
        }
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
