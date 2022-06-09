using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishingManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] List<FirstLevelFinishingScript> finishCheckList = new List<FirstLevelFinishingScript>();
    [SerializeField] ButtonVr1 buttonVrScript;
    [SerializeField] GameObject button;
    [SerializeField] bool finishChecking = false;
    [SerializeField] int scene;
   
    void Start()
    {
        scene = 1;
    }

    // Update is called once per frame
    void Update()
    {
        finishChecking = true;
        foreach (FirstLevelFinishingScript a in finishCheckList)
        {

            if (!a.rotationStarted)
            {
                finishChecking = false;
            }
        }
        if (finishChecking)
        {
            button.SetActive(true);
            //   buttonVrScript.FinishLevel(scene);
        }

    }
}
