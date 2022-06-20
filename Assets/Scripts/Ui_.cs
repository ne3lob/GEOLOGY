using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ui_ : MonoBehaviour
{
    //public Image image;


    public void exitgame()
    {
        Application.Quit();
    }

    public void Scene1()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void Scene2()
    {
        SceneManager.LoadScene("Scene2");
    }

    public void Scene3()
    {
        SceneManager.LoadScene("Scene3");
    }
}