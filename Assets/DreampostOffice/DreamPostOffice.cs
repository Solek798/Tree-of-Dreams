using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DreamPostOffice : MonoBehaviour
{
    
    [SerializeField] private GameObject layoutGroup = null;
    // TODO(FK): delete FormerlySerializedAs Attribute
    [FormerlySerializedAs("questPanel")] [SerializeField] private GameObject questPanelPrefab = null;
    [SerializeField] private GameObject dreamPostOffice = null;
    [SerializeField] private AudioSource audioPlayer = null;
    [SerializeField] private AudioClip enterDPO = null;
    [SerializeField] private AudioClip finishDreamLetter;
    
    // temp
    [SerializeField] private Inventory inventory = null;
    [SerializeField] private Journal journal = null;

    public Scrollbar slider = null;
   
    public GameObject player;
    public float maxDistanceToPostOffice = 10f;
    private bool _uiOpened;
    
    
    private void Start()
    {
        _uiOpened = false;
        SetSliderDefaults();

        var sellArea = GetComponentInChildren<SellArea>();
        if (!sellArea) return;
        
        sellArea.Inventory = inventory;
        sellArea.Journal = journal;

        audioPlayer.clip = enterDPO;
    }

    
    private static void Parent( GameObject parentOb, GameObject childOb )
    {
        childOb.transform.SetParent(parentOb.transform, true);
        childOb.transform.localScale = new Vector3(1, 1, 1);
        
    }

    private void Update()
    {
        var playerInRange = Vector3.Distance(dreamPostOffice.transform.position, player.transform.position);
        
        if (Input.GetKeyDown(KeyCode.E) && playerInRange <= maxDistanceToPostOffice && _uiOpened == false) 
        {
            OpenPostOfficeMenu();
            _uiOpened = true;
        }

        if (!audioPlayer.isPlaying && audioPlayer.clip == finishDreamLetter)
        {
            audioPlayer.clip = enterDPO;
        }

    }
    
    public void AddDisplay(QuestDisplay display)
    {
        display.transform.SetParent(layoutGroup.transform);
        display.transform.localScale = Vector3.one;
    }

    private void OpenPostOfficeMenu()
    {
        audioPlayer.Play();
        SetSliderDefaults();
        gameObject.GetComponent<Canvas>().enabled = true;
        _uiOpened = true;
        
        UIStatus.Instance.DialogOpened = true;
    }

    private void SetSliderDefaults()
    {
        slider.value = 1;
        slider.size = 0.5f;
        slider.numberOfSteps = 0;
    }

    public void OnExitButtonClick()
    {
        _uiOpened = false;
        gameObject.GetComponent<Canvas>().enabled = false;
        
        UIStatus.Instance.DialogOpened = false;
    }

    public void OnDayFinished(int earnedCash)
    {
        inventory.Currency += earnedCash;
        journal.EarningsCounter += 
            inventory.Currency + earnedCash > inventory.MaxCurrency ? inventory.MaxCurrency : earnedCash;
    }

    public void OnQuestFulfilled()
    {
        audioPlayer.clip = finishDreamLetter;
        audioPlayer.Play();
    }
}
