using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishHere : MonoBehaviour
{
    public OVROverlay overlay;
    public OVROverlay text;

    private void Start()
   
        {
            overlay.hidden = true;
            text.hidden = true;
        }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hand"))
        {
            overlay.hidden = false;
            text.hidden = false;
            StartCoroutine(LoadYourAsyncScene());

        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSeconds(3);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("staticCave");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
       // levelChanged = false;
    }
}
