using System;
using UnityEngine;

public class ScaleScript : MonoBehaviour
{
    private Transform targetPointOne;

    //private AlembicStreamPlayer _player;
    public Camera camGameObject;
    private GameObject o;

    private float timer = 0.0f;
    private int seconds;
    public bool _goToPlayer = false;

    [SerializeField] private GameObject proceduralCube;
    [SerializeField] private Animator _animator;
    [SerializeField] private String onStringBool;
    [SerializeField] private String offStringBool;
    

    // [SerializeField] private TimeLinePlayer _timeLinePlayerScript;


    void Start()
    {
        o = gameObject;
        targetPointOne = o.transform;
        // _timeLinePlayerScript = proceduralCube.GetComponent<TimeLinePlayer>();
        _animator = proceduralCube.GetComponent<Animator>();
    }

    void Update()
    {
        TargetObjectOne(targetPointOne);
    }

    private void TargetObjectOne(Transform transformTarget)
    {
        TargetPointsOne(new Vector3(), transformTarget, false,onStringBool,offStringBool);
    }

    // [SerializeField] public float speedGrowing = 0.1f;
    // [SerializeField] public float maxGrowth = 3f;
    // [SerializeField] public float minGrowth = 1f;

    public float timeToConcentrate = 5f;

    void TargetPointsOne(Vector3 vector, Transform transformTarget, bool b,String nameboolOn,String nameboolOff)
    {
        vector = camGameObject.WorldToViewportPoint(transformTarget.position);
        b = vector.z > 0 && vector.x > 0 && vector.x < 1 && vector.y > 0 && vector.y < 1;


        if (b)
        {
            _animator.SetBool(nameboolOn, true);
            _animator.SetBool(nameboolOff, false);

            //_timeLinePlayerScript.StartTimeline();
            //scaling
            // o.transform.localScale = Vector3.Lerp(o.transform.localScale, new Vector3(maxGrowth, maxGrowth, maxGrowth), speedGrowing * Time.deltaTime);
            timer += Time.deltaTime;
            seconds = (int)(timer % 60f);


            if (timer > timeToConcentrate)
            {
                _goToPlayer = true;
                timer = 0;
                Debug.Log("BUM");
            }
        }
        else
        {
            _animator.SetBool(nameboolOn, false);

            _animator.SetBool(nameboolOff, true);
            timer = 0;
            //scaling
            // o.transform.localScale = Vector3.Lerp(o.transform.localScale, new Vector3(minGrowth, minGrowth, minGrowth), speedGrowing * Time.deltaTime);
        }
    }
}