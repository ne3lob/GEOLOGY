using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using Oculus;
public class VertexDisplacement : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject toDisplace;
    Material toDisplaceMat;
    void Start()
    {
        toDisplaceMat = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("fl" + toDisplaceMat.GetFloat("_HeightStrength"));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Hand"))
        {
            toDisplaceMat.SetFloat("_HeightStrength", toDisplaceMat.GetFloat("_HeightStrength") + 0.1f);
        }
    }
}
