using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using Oculus;
public class BlendDisplacement : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject toDisplace;
    Material toDisplaceMat;
    float heightInit;
    bool coRout;
    float lerpValue;
    bool checkCollision;
    bool isDissapeared = false;
    float changeAmount = 0.01f;
    [SerializeField] float duration = 1000000f;
    [SerializeField] float displaceAmount = 0.3f;
    [SerializeField] float dissapearMultiplier = 30f;
    BoxCollider colliderToScale;
    [SerializeField] const float deletionSpeed = 0.8f;
    [SerializeField]  float colliderBounds = 0.005f;
    GameObject light;
    void Start()
    {
        //  toDisplaceMat = gameObject.GetComponent<Renderer>().material;
        heightInit = gameObject.GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(0);
        checkCollision = false;
        coRout = false;
        colliderToScale = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(0) > heightInit + (displaceAmount * dissapearMultiplier))
        {
            //gameObject.SetActive(false);
            isDissapeared = true;
            //checkCollision = false;
           
        /*    colliderToScale.size.Set(colliderToScale.size.x, colliderToScale.size.y + colliderBounds, colliderToScale.size.z);
            colliderToScale.center.Set(colliderToScale.center.x, colliderToScale.center.y + colliderBounds, colliderToScale.center.z);*/
        }
        else if(checkCollision)
        {
            var lerp = Mathf.PingPong(Time.time, duration) / duration;
            float newValue = Mathf.Lerp(0f, displaceAmount, lerp);
            gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, gameObject.GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(0)+ newValue);
          //  Debug.Log(colliderToScale.size.y + colliderBounds);
            colliderToScale.size.Set(colliderToScale.size.x, colliderToScale.size.y + colliderBounds, colliderToScale.size.z);
            colliderToScale.center.Set(colliderToScale.center.x, colliderToScale.center.y + colliderBounds, colliderToScale.center.z);


        }
       /*
        if(checkCollision && toDisplaceMat.color.a > 0.3f)
        {
            var lerp = Mathf.PingPong(Time.time, duration) / duration;
            float newValue = Mathf.Lerp(0f, displaceAmount, lerp);
            gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, gameObject.GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(0)+ newValue);
            colliderToScale.size.Set(colliderToScale.size.x, colliderToScale.size.y + colliderBounds, colliderToScale.size.z) ;
            colliderToScale.center.Set(colliderToScale.center.x, colliderToScale.center.y + colliderBounds, colliderToScale.center.z);


        }*/
    }
    void FixedUpdate()
    {
        if(isDissapeared)
        {
         //   Debug.Log("maerial color before:" + toDisplaceMat.color.a);

           // toDisplaceMat.color = new Color(toDisplaceMat.color.r, toDisplaceMat.color.g, toDisplaceMat.color.b, Mathf.PingPong(Time.time * deletionSpeed, 1));
          //  Debug.Log("maerial color:" + toDisplaceMat.color.a);
            gameObject.SetActive(false);

            /*if (toDisplaceMat.color.a == 0)
            {
                gameObject.SetActive(false);
            }*/
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
   /* IEnumerator ChangeLerp()
    {
        var lerp = Mathf.PingPong(Time.time, duration) / duration;
        float newValue = Mathf.Lerp(0f, displaceAmount, lerp);
        toDisplaceMat.SetFloat("_HeightStrength", toDisplaceMat.GetFloat("_HeightStrength") + newValue);
        if (newValue == displaceAmount)
        {
            checkCollision = false;
        }
    }*/
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Hand"))
        {
            // StartCoroutine("LerpChange", displaceAmount);

            checkCollision = true;
            light =  collision.gameObject.transform.GetChild(1).gameObject;
            light.SetActive(true);

        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            checkCollision = false;
            light.SetActive(false);
            
        }
    }

}
