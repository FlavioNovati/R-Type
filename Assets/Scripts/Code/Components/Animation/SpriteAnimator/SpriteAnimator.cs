using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private Sprite[] SpriteList;
    [SerializeField] private int Framerate = 30;
    [SerializeField] private bool Loop = true;
    [SerializeField] private bool DestroyOnEnd = true;
    //frame time
    private float Frequency;

    private void Awake()
    {
        Frequency = 1 / (float)Framerate;
    }
    
    IEnumerator Animate()
    {
        int i = 0;
        do
        {
            while (i < SpriteList.Length)
            {
                Renderer.sprite = SpriteList[i];
                i++;
                yield return new WaitForSeconds(Frequency);
            }
            i = 0;
        } while (Loop);
        if (DestroyOnEnd)
            Destroy(this.gameObject);
    }

    public void OnEnable()
    {
        StartCoroutine(Animate());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
