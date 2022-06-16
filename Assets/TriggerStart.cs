using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TriggerStart : MonoBehaviour
{
    // Start is called before the first frame update
    public Text LogCollsiionEnter;
    public Text LogCollisionStay;
    public Text LogCollisionExit;
 
    private void OnCollisionEnter(Collision collision)
    {
        LogCollsiionEnter.text = "On Collision Enter: " + collision.collider.name;
        Debug.Log("buuuuuu");
    }
 
    private void OnCollisionStay(Collision collision)
    {
        LogCollisionStay.text = "On Collision stay: " + collision.collider.name;
        Debug.Log("buuuuuu");
    }
 
    private void OnCollisionExit(Collision collision)
    {
        LogCollisionExit.text = "On Collision exit: " + collision.collider.name;
        Debug.Log("buuuuuu");
    }
}
