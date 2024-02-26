using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image ProgressImage;
    [SerializeField] private AnimationCurve AnimationCurve;
    [SerializeField] private float AnimationSpeed = 3f;

    private void Awake()
    {
        ProgressImage.fillAmount = 1f;
    }

    public void SetProgress(float progress)
    {
        StartCoroutine(Animate(progress));
    }

    IEnumerator Animate(float newProgress)
    {
        float progress = 0f;
        float curveProgress = 0f;
        float startFill = ProgressImage.fillAmount;
        float diffFill = newProgress - startFill;

        //progress = diff progress animated

        while (progress < 1f)
        {
            curveProgress = AnimationCurve.Evaluate(progress);
            ProgressImage.fillAmount = startFill + (curveProgress * diffFill);

            progress += Time.deltaTime;
            yield return null;
        }
    }

}
