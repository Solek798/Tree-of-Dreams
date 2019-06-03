using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCard : MonoBehaviour
{
    public void OnExitButtonPressed()
    {
        GetComponent<Canvas>().enabled = false;
    }
}
