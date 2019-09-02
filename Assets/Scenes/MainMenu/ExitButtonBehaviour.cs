using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExitButtonBehaviour : MonoBehaviour
{
    public void OnExitClick()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
