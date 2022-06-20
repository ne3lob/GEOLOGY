using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderAnimation : MonoBehaviour
{
    private float shaderValue = 0f;
    private float change = 0.0001f;
    public Material changeDisolve;

    void Start()
    {
        changeDisolve.SetFloat("Size", 0);
        InvokeRepeating("ChangeShader", 0.1f, 0.3f);
    }

    void ChangeShader()
    {
        if (shaderValue > 0.1f)
        {
            CancelInvoke();
        }
        shaderValue += change;
        changeDisolve.SetFloat("Size", shaderValue);
        
    }
}