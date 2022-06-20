using UnityEngine;
using System.Collections;

public class ObjectShake : MonoBehaviour
{
    float speed = 1.0f; //how fast it shakes
    float amount = 1.0f; //how much it shakes

    void OnGUI()
    {
    }

    void Update()
    {
        var transformPosition = transform.position;
        transformPosition.x = Mathf.Sin(Time.time * speed) * amount;
    }
}