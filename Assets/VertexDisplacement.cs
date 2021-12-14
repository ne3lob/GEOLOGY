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
    float heightInit;
    bool coRout;
    float lerpValue;
    bool checkCollision;
    float duration = 0.1f;
    float changeAmount = 0.01f;

    [SerializeField] float displaceAmount = 0.4f;
    void Start()
    {
        toDisplaceMat = gameObject.GetComponent<Renderer>().material;
        heightInit = toDisplaceMat.GetFloat("_HeightStrength");
        checkCollision = false;
        coRout = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(toDisplaceMat.GetFloat("_HeightStrength") > heightInit + (displaceAmount * 10))
        {
            gameObject.SetActive(false);
        }
       
        if(checkCollision)
        {
            var lerp = Mathf.PingPong(Time.time, duration) / duration;
            float newValue = Mathf.Lerp(0f, displaceAmount, lerp);
            toDisplaceMat.SetFloat("_HeightStrength", toDisplaceMat.GetFloat("_HeightStrength") + newValue);

             
            
        }
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
    IEnumerator LerpChange(float displaceAmount)
    {
        float newValue = 0;
        float startPosition = 0;
        float time = 0;
        while (changeAmount < displaceAmount)
        {
            newValue = Mathf.Lerp(startPosition, displaceAmount, changeAmount / displaceAmount);
            toDisplaceMat.SetFloat("_HeightStrength", toDisplaceMat.GetFloat("_HeightStrength") + changeAmount);
            changeAmount += 0.01f;
            yield return null;
        }
        changeAmount = 0.01f;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Hand"))
        {
            // StartCoroutine("LerpChange", displaceAmount);

            checkCollision = true;


        }
    }
}
