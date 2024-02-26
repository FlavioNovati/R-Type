using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text PlayerScore;
    [SerializeField] private ProgressBar PlayerHP;
    [SerializeField] private RectTransform HealthWarning;
    [SerializeField] private float HealthWarningScreen = 0.3f;

    private float PlayerHp;
    private float CurrentPlayerHealth = -15f;
    private float UIScore;

    private void UpdateScore(int score)
    {
        UIScore += score;
        PlayerScore.text = "Score: " + UIScore.ToString();
    }

    private void UpdateHealth(float HP)
    {
        if (CurrentPlayerHealth < -10f)
        {
            CurrentPlayerHealth = HP;
            PlayerHp = HP;
        }

        PlayerHP.SetProgress(HP/PlayerHp);

        if(HP/PlayerHp <= HealthWarningScreen)
            HealthWarning.gameObject.SetActive(true);
        else
            HealthWarning.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.OnIncrementScore += UpdateScore;
        PlayerManager.OnHealthChanged += UpdateHealth;
    }
}
