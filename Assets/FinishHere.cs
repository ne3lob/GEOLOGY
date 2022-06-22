using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishHere : MonoBehaviour
{
    public OVROverlay overlay;
    public OVROverlay text;
    private float timerToNextLevel;
    [SerializeField] public float WaitingSec;
    
    private void Start()
   
        {
            
        }

    private void Update()
    {
        timerToNextLevel += Time.deltaTime;
        if (timerToNextLevel > WaitingSec)
        {
            StartCoroutine(LoadYourAsyncScene());
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if(other.gameObject.CompareTag("Hand")&& goToCave==true)
    //     {
    //         Debug.Log("Tut");
    //         overlay.hidden = false;
    //         text.hidden = false;
    //         StartCoroutine(LoadYourAsyncScene());
    //
    //     }
    // }

    IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSeconds(3);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("CaveMovement");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
       // levelChanged = false;
    }
}
