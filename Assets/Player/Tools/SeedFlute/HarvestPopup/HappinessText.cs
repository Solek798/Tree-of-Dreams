using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessText : MonoBehaviour
{
    public GameObject Plant1;

    public void Start()
    {
        Plant1.SetActive(false);
    }

    public void OnMouseOver()
    {
        Plant1.SetActive(true);
    }

    public void OnMouseExit()
    {
        Plant1.SetActive(false);
    }

}
