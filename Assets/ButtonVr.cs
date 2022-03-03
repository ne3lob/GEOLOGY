using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ButtonVr : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;

    public UnityEvent onRelease;

    GameObject presser;

    private bool isPressed;
    public StartLevel scriptFadeIn;

    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 1f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 1f, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }

    public void Pressed(int Scene)
    {
        SceneManager.LoadScene(Scene);
        scriptFadeIn.NewLevel();
    }
}