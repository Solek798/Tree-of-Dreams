using System.Collections;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private Farmland farmland = null;
    [SerializeField] private LampionFactory lampionFactory = null;
    [SerializeField] private Journal journal = null;
    [SerializeField] private GameObject sleepMenu = null;
    [SerializeField] private int questFrequency;
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip goToSleepSound;
    [SerializeField] private AudioClip wakeUpSound;

    public GameObject player;
    public float maxDistanceToSleep = 10f;

    private int daysSinceLastQuest;


    private void Start()
    {
        lampionFactory.CreateAndSend();
    }

    private void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, player.transform.position));
        if (Input.GetKeyDown(KeyCode.E) &&
            Vector3.Distance(transform.position, player.transform.position) <= maxDistanceToSleep)
        {
            StartCoroutine(Sleep());
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

        if (daysSinceLastQuest == questFrequency - 1)
        {
            lampionFactory.CreateAndSend();
            daysSinceLastQuest = 0;
        }
        else
        {
            daysSinceLastQuest++;
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
        //sleepMenu.SetActive(false);

        Transition.Instance.FadeNormal();
        
        audioPlayer.clip = wakeUpSound;
        audioPlayer.Play();
    }

    public void ResumeNight()
    {
        StartCoroutine(WakeUp());
    }
}