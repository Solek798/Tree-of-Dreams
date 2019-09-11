using System.Collections;
using System.Linq;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private Farmland farmland = null;
    [SerializeField] private LampionFactory lampionFactory = null;
    [SerializeField] private Journal journal = null;
    [SerializeField] private GameObject sleepMenu = null;
    [SerializeField] private int questFrequency = 0;
    [SerializeField] private AudioSource audioPlayer = null;
    [SerializeField] private AudioClip goToSleepSound = null;
    [SerializeField] private AudioClip wakeUpSound = null;
    [SerializeField] private QuestCollector questCollector = null;
    [SerializeField] private GameObject thxForPlayingScreen = null;
    [SerializeField] private GameObject floatingTextPrefab = null;

    public GameObject player = null;
    public float maxDistanceToSleep = 10f;

    private int _daysSinceLastQuest = 0;
    private int _questCount = 0;
    private GameObject _hotkeyText = null;
    private Vector3 _offset = new Vector3(8,8,0);


    private void Start()
    {
        _questCount = lampionFactory.questData.Count;
        lampionFactory.CreateAndSend();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= maxDistanceToSleep)
        {
            if (!transform.GetComponentInChildren<TextMesh>())
            {
                _hotkeyText = Instantiate(floatingTextPrefab, transform.position + _offset, Quaternion.identity, transform);
            }
            
            _hotkeyText.transform.LookAt(Camera.main.transform);
            _hotkeyText.transform.Rotate(0,180,0);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Sleep());
            }    
        }
        else if (_hotkeyText != null)
        {
            Destroy(_hotkeyText);
        }
    }

    private bool ProccessNight()
    {
        foreach (FarmlandLevel level in farmland)
        {
            foreach (var space in level.GetAllSpaces())
            {
                space.UpdateState();
            }
        }

        if (_daysSinceLastQuest == questFrequency - 1)
        {
            lampionFactory.CreateAndSend();
            _daysSinceLastQuest = 0;
        }
        else
        {
            _daysSinceLastQuest++;
        }

        journal.Days++;

        
        
        return false;
    }

    private IEnumerator Sleep()
    {
        audioPlayer.clip = goToSleepSound;
        audioPlayer.Play();
        
        Transition.Instance.FadeBlack();

        yield return new WaitForSeconds(Transition.Instance.FadeBlackTime);
        ProccessNight();
        sleepMenu.SetActive(true);

        Transition.Instance.FadeNormal();
    }

    private  IEnumerator WakeUp()
    {
        Transition.Instance.FadeBlack();

        yield return new WaitForSeconds(Transition.Instance.FadeBlackTime);
        
        if (questCollector.GetAllQuests().Count(t => t.IsFulFilled) == _questCount)
        {
            thxForPlayingScreen.SetActive(true);
        }

        Transition.Instance.FadeNormal();
        
        audioPlayer.clip = wakeUpSound;
        audioPlayer.Play();
    }

    public void ResumeNight()
    {
        StartCoroutine(WakeUp());
    }
}