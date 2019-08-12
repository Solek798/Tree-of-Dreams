using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExitButtonBehaviour : MonoBehaviour
{
    public void OnExitClick()
    {
        #if true
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
