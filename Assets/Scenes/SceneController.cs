﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("DreamWorld");
    }
}
