using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    public Scrollbar slider = null;
    
    [SerializeField] private GameObject layoutGroup = null;
    [SerializeField] private Canvas journalCanvas = null;
    [SerializeField] private Toggle pauseTab = null;
    [SerializeField] private Text dayCounter = null;
    [SerializeField] private Text earningsCounter = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private Image imageNPC = null;
    [SerializeField] private AudioSource audioPlayer = null;
    [SerializeField] private TextMeshProUGUI nameNPC = null;
    [SerializeField] private TextMeshProUGUI storyNPC = null;
    [SerializeField] private ToggleGroup questToggleGroup = null;

    [SerializeField] private GameObject noQuestsText = null;
    [SerializeField] private GameObject noQuestsImage = null;
    [SerializeField] private GameObject questInfo = null;
    [SerializeField] private TutorialButton defaultTutorialButton = null;
    
    private bool _uiOpened;

    public int Days
    {
        get => Convert.ToInt32(dayCounter.text);
        set => dayCounter.text = value.ToString();
    }

    public int EarningsCounter
    {
        get => Convert.ToInt32(earningsCounter.text);
        set => earningsCounter.text = value.ToString();
    }

    private void Start()
    {
        Days = 0;
        EarningsCounter = 0;
        defaultTutorialButton.OnButtonClick();
    }

    
    private static void Parent( GameObject parentOb, GameObject childOb )
    {
        childOb.transform.SetParent(parentOb.transform, false);
        childOb.transform.localScale = new Vector3(1, 1, 1);
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && _uiOpened == false)
        {
            OpenJournal();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && _uiOpened == true)
        {
            CloseJournal();
        }
        
    }

    public void AddDisplay(QuestDisplay display)
    {
        display.transform.SetParent(layoutGroup.transform);
        display.transform.localScale = new Vector3(1, 1, 1);

        var toggle = display.GetComponent<Toggle>();
        toggle.group = questToggleGroup;
        toggle.interactable = true;
        
        noQuestsText.SetActive(false);
        noQuestsImage.SetActive(false);
        questInfo.SetActive(true);
        
        if (layoutGroup.GetComponentsInChildren<QuestDisplay>().Length == 1)
        {
            toggle.isOn = true;
            OnSelectedDisplayChanged(display);
        }
        
    }

    public void OpenJournal()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleState"))
        {
            audioPlayer.Play();

            animator.Play("PopUPPanelIn");

            pauseTab.isOn = true;

            slider.value = 1;

            _uiOpened = true;

            UIStatus.Instance.DialogOpened = true;


            if (layoutGroup.GetComponentsInChildren<QuestDisplay>().Length <= 0)
            {
                noQuestsText.SetActive(true);
                noQuestsImage.SetActive(true);
                questInfo.SetActive(false);

                
            }
        }      
    }
    
    
    private void CloseJournal()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PopUPPanelIn"))
        {
            animator.Play("PopUPPanelOut");
            
            audioPlayer.Play();

            _uiOpened = false;

            UIStatus.Instance.DialogOpened = false;
        }
    }
    
    public void OnExitButtonClick()
    {
        CloseJournal();
    }
    
    public void OnSelectedDisplayChanged(QuestDisplay display)
    {
        nameNPC.text = display.Quest.Data.name;
        imageNPC.sprite = display.Quest.Data.questNPCImage;
        storyNPC.text = display.Quest.Data.questDescription;
    }
}
