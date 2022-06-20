using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    private OVRScreenFade ovr;
    // Start is called before the first frame update
    void Start()
    {
        ovr = GetComponent<OVRScreenFade>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewLevel()
    {
        ovr.FadeIn();
    }
}
