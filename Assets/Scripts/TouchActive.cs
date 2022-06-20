using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using DG.Tweening;
using UnityEngine;

public class TouchActive : MonoBehaviour
{
    public GameObject TouchImage;

    private DOTweenPath dotAnimation;

    private void Start()
    {
        dotAnimation = GetComponent<DOTweenPath>();
    }

    private void OnCollisionEnter(Collision other)
    {
        TouchImage.SetActive(false);

        dotAnimation.DOPlay();
    }
}