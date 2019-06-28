using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int SceneToLoad;
    
    public void OnButtonClick()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
