using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveFinishMovement : MonoBehaviour
{

    [SerializeField] public Transform targetTransform;
    [SerializeField] float scaleRatio;
    [SerializeField] float shrinkRate;
    [SerializeField] CaveTimer caveTimerScript;
    private ScaleScript scaleScript;
    [SerializeField] float moveSpeedToPlayer = 0.5F;
    [SerializeField] float rotationAngle = 45f;
    [SerializeField] Vector3 rotationDirection = new Vector3(1f,0f,0f);
    [SerializeField] public bool rotationStarted = false;
    [SerializeField] float rotateSpeed = 0.2f;
    [SerializeField] Vector3 scale;
    [SerializeField] float waitSeconds;
    [SerializeField] Vector3 stayAwayDistance = new Vector3(0.2f, 0.2f, 0.2f);
    private bool waited;
    [SerializeField] private Vector3 pointB;

    [SerializeField] private float duration;
    [SerializeField] float whichMovement; //0 for go to target and shrink and rotate , 1 for rotate, 2 for rotate 
    // Start is called before the first frame update
    void Start()
    {
        //  buttonVrScript = GameObject.Find("FinishButton").GetComponent<ButtonVr1>();
        //  scaleScript = objScaleScript.GetComponent<ScaleScript>();
        scale = new Vector3(scaleRatio, scaleRatio, scaleRatio);
        waited = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (caveTimerScript.movementStarted)
        {
            StartCoroutine("WaitBeforeGo");
            //go to target after this time
            if(waited)
            {
                switch(whichMovement){
                    case 0:
                        GoToTargetAndShrink();
                        if (rotationStarted)
                        {
                            rotateAround();
                        }
                        break;
                    case 1:
                        rotateAround();
                        break;
                    case 2:
                        MovePingPong();
                        break;


                }



            }
          

        }
    }
    IEnumerator MovePingPong()
    {
        var pointA = transform.position;
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, duration));
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, duration));
        }
  }


    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
{
    var i = 0.0f;
    var rate = 1.0f / time;
    while (i < 1.0f)
    {
        i += Time.deltaTime * rate;
        thisTransform.position = Vector3.Lerp(startPos, endPos, i);
        yield return null;
    }
}
void GoToTargetAndShrink()
    {
        //add staying away
        transform.position = Vector3.Lerp(transform.position, targetTransform.position + stayAwayDistance, moveSpeedToPlayer * Time.deltaTime);


        //scale until this
        transform.localScale = Vector3.Lerp(transform.localScale, scale, shrinkRate * Time.deltaTime);
         // it should work but there is a problem with position matching
        
        
    }
    void rotateAround()
    {
        gameObject.transform.RotateAround(targetTransform.position, rotationDirection, rotationAngle);
    }

    IEnumerator WaitBeforeGo()
    {
        
        yield return new WaitForSeconds(waitSeconds);
        waited = true;
    }
}
