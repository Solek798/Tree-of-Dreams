using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SleepMenu : MonoBehaviour
{
    [SerializeField] private Transform questLayoutGroup = null;
    [SerializeField] private Text todaysEaringsText = null;

    public int TodaysEarings
    {
        get => Convert.ToInt32(todaysEaringsText.text);
        set => value.ToString();
    }

    private void Awake()
    {
        TodaysEarings = 0;
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
        Debug.Log("Start sorting out");
        foreach (var display in questLayoutGroup.GetComponentsInChildren<ProgressDisplay>())
        {
            if (display.IsFulfilled)
            {
                Destroy(display.gameObject);
            }
        }

        gameObject.SetActive(false);
    }
}
