using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelFinishingScript : MonoBehaviour
{

    [SerializeField] public Transform targetTransform;
    [SerializeField] float scaleRatio;
    [SerializeField] float shrinkRate;
    [SerializeField] ButtonVr1 buttonVrScript;
    private ScaleScript scaleScript;
    public bool buttonPressed;
    [SerializeField] float moveSpeedToPlayer = 0.5F;
    [SerializeField] float rotationAngle = 45f;
    [SerializeField] public bool rotationStarted = false;
    [SerializeField] float rotateSpeed = 0.2f;
    [SerializeField] Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        //  buttonVrScript = GameObject.Find("FinishButton").GetComponent<ButtonVr1>();
        //  scaleScript = objScaleScript.GetComponent<ScaleScript>();
        scale = new Vector3(scaleRatio, scaleRatio, scaleRatio);
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonVrScript.finishingStarted)
        {
            if(gameObject.GetComponent<MovePinPong>() != null)
            {
                gameObject.GetComponent<MovePinPong>().enabled = false;
            }
            //go to target after this time
            GoToTargetAndShrink();
            
            if(rotationStarted)
            {
                rotateAround();
            }

        }

       

    }
    void GoToTargetAndShrink()
    {
        //add staying away
        transform.position = Vector3.Lerp(transform.position, targetTransform.position, moveSpeedToPlayer * Time.deltaTime);
      

        //scale until this
        transform.localScale = Vector3.Lerp(transform.localScale, scale, shrinkRate * Time.deltaTime);
        if (transform.localScale == scale) // it should work but there is a problem with position matching
        {
            rotationStarted = true;
        }
    }
    void rotateAround()
    {
        gameObject.transform.RotateAround(targetTransform.position, new Vector3(1f, 0f, 1f), rotationAngle);
    }
}
