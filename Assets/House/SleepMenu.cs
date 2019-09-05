using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SleepMenu : MonoBehaviour
{
    [SerializeField] private Transform questLayoutGroup = null;
    [SerializeField] private Text todaysEaringsText = null;
    [SerializeField] private DreamPostOffice dreamPostOffice = null;

    public int TodaysEarings
    {
        get => Convert.ToInt32(todaysEaringsText.text);
        set => todaysEaringsText.text = value.ToString();
    }

    private void Awake()
    {
        TodaysEarings = 0;
    }
    
    private void OnEnable()
    {
        PlayerScriptor.Instance.AllowMoving = false;
        PlayerScriptor.Instance.AllowInteracting = false;
        
        foreach (var display in questLayoutGroup.GetComponentsInChildren<ProgressDisplay>())
        {
            if (display.IsFulfilled)
            {
                Debug.Log("Is fulfilled");
                TodaysEarings += display.Quest.Data.rewardDreamEssence;
            }
        }
    }

    private void OnDisable()
    {
        PlayerScriptor.Instance.AllowMoving = true;
        PlayerScriptor.Instance.AllowInteracting = true;
    }

    public void AddDisplay(ProgressDisplay display)
    {
        display.transform.SetParent(questLayoutGroup.transform);
        display.transform.localScale = Vector3.one;
    }
    
    public void ManageDisplays()
    {
        StartCoroutine(WaitUntilTransitionFinished());
    }

    private IEnumerator WaitUntilTransitionFinished()
    {
        yield return new WaitForSeconds(Transition.Instance.FadeBlackTime);
        SortOutFulfilledDisplays();
    }

    private void SortOutFulfilledDisplays()
    {
        foreach (var display in questLayoutGroup.GetComponentsInChildren<ProgressDisplay>())
        {
            if (display.IsFulfilled)
            {
                Destroy(display.gameObject);
            }
        }

        dreamPostOffice.OnDayFinished(TodaysEarings);
        TodaysEarings = 0;
        gameObject.SetActive(false);
    }
}
