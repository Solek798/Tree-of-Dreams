using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadMenu()
    {
        Debug.Log("LoadMenu");
        StartCoroutine(Load("MainMenu"));
    }

    public void LoadGame()
    {
        StartCoroutine(Load("DreamWorld"));
    }

    private IEnumerator Load(string scene)
    {
        Debug.Log("Load");
        Transition.Instance.FadeBlack();
        
        yield return new WaitForSeconds(Transition.Instance.FadeBlackTime);
        
        SceneManager.LoadScene(scene);
    }
}
