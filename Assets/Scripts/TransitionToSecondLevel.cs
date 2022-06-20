using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToSecondLevel : MonoBehaviour
{
    public OVROverlay overlay;
    public OVROverlay text;
    // Start is called before the first frame update
    void Start()
    {
        overlay.hidden = true;
        text.hidden = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
