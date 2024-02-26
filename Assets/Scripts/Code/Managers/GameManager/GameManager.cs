using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<int> OnIncrementScore;
    public static Action OnPlayerDeath;

    public int Score = 0;

    private void OnEnable()
    {
        OnIncrementScore += (int score) => Score += score;
    }

    private void OnDisable()
    {
        OnIncrementScore -= OnIncrementScore;
    }

}
