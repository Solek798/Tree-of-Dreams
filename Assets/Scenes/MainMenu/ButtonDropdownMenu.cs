using UnityEngine;

public class ButtonDropdownMenu : MonoBehaviour
{

    
    public void OnBtnClick(Canvas canvasBtn)
    {
        canvasBtn.enabled = !canvasBtn.enabled;
    }
}
