using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStart : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject NextLevel;
    

    private void OnCollisionEnter(Collision other)
    {
        NextLevel.SetActive(true);
        other.gameObject.SetActive(false);
        Debug.Log("'DAAAA");
    }
}