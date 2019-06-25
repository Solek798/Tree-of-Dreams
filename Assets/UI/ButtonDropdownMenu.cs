using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDropdownMenu : MonoBehaviour
{

    [SerializeField] private Canvas canvasBtn;

    private void Start()
    {
        canvasBtn.enabled = false;
    }
    public void OnBtnClick()
    {
        canvasBtn.enabled = true;
    }
}
