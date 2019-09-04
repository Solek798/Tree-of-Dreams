using System.Collections;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private Farmland farmland = null;
    [SerializeField] private LampionFactory lampionFactory = null;
    [SerializeField] private Journal journal = null;
    [SerializeField] private GameObject sleepMenu = null;
    [SerializeField] private int questFrequency;
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

        if (daysSinceLastQuest == questFrequency)
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
    }

    public void ResumeNight()
    {
        StartCoroutine(WakeUp());
    }
}