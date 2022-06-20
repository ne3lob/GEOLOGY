using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpener : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> walls;
    [SerializeField] int deletedCount;
    const int wallsErased = 8;
    [SerializeField] GameObject but;
    void Start()
    {
        deletedCount = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(walls != null)
        {
            foreach (GameObject a in walls)
            {
                Debug.Log(a.name + " " + a.activeSelf);
                if (!a.activeSelf) 
                {
                    deletedCount += 1;
                    walls.Remove(a);
                }
            }
        }
       
        if (deletedCount == wallsErased)
        {
            but.SetActive(true);
        }
    }
   


}
