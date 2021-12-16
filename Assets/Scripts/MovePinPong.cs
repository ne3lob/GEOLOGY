using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePinPong : MonoBehaviour
{
    [SerializeField] private Vector3 pointB;

    [SerializeField] private float duration;

    IEnumerator Start()
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
}