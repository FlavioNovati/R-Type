using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsatingAlpha : MonoBehaviour
{
    [SerializeField] private CanvasGroup ElementToAnimate;
    [SerializeField] private float Speed;

    private void FixedUpdate()
    {
        ElementToAnimate.alpha = (Mathf.Sin(Time.time * Speed) + 1) * 0.5f;
    }
}
