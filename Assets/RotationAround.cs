using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAround : MonoBehaviour
{
    public GameObject button; // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(ShowAndHide(button, 5.0f));
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator ShowAndHide(GameObject Option_1, float delay)
    {
        yield return new WaitForSeconds(delay);
        Option_1.SetActive(true);
        Debug.Log("reee");
    }
}