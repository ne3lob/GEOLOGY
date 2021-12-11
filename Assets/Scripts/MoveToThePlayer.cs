using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToThePlayer : MonoBehaviour
{
    [SerializeField]public Transform targetTransform;
    [SerializeField] public GameObject objScaleScript;
    private ScaleScript scaleScript;
    // Start is called before the first frame update
    void Start()
    {
        scaleScript = objScaleScript.GetComponent<ScaleScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scaleScript._goToPlayer)
        {
            GoToTarget();
        }
    }

    void GoToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, targetTransform.position, 0.5f*Time.deltaTime);
    }
}
