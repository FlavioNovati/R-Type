using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static Action TriggerEnd;

    [SerializeField] private TMP_Text PlayerScore;
    [SerializeField] private ProgressBar PlayerHPBar;
    [SerializeField] private RectTransform HealthWarning;
    [SerializeField] private RectTransform StartScreen;
    [SerializeField] private RectTransform GameOverScreen;
    [SerializeField] private RectTransform VictoryScreen;
    [SerializeField] private float HealthWarningScreen = 0.3f;

    private float StartHp = -15f;
    private float UIScore;

    private void Start()
    {
        StartHp = PlayerManager.HP;
        StartScreen.gameObject.SetActive(true);
        GameOverScreen.gameObject.SetActive(false);
        VictoryScreen.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    private void UpdateScore(int score)
    {
        UIScore += score;
        PlayerScore.text = "Score: " + UIScore.ToString();
    }

    private void UpdateHealth(float value)
    {
        PlayerHPBar.SetProgress( (PlayerManager.HP + value) / StartHp );

        if(((PlayerManager.HP + value) / StartHp) <= HealthWarningScreen)
            HealthWarning.gameObject.SetActive(true);
        else
            HealthWarning.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        StartScreen.gameObject.SetActive(false);
    }

    public void ShowDeathMenu()
    {
        StartCoroutine(ShowDeathCoroutine());
    }

    private IEnumerator ShowDeathCoroutine()
    {
        yield return new WaitForSeconds(5);
        GameOverScreen.gameObject.SetActive(true);
    }

    private void BossDefeated()
    {
        VictoryScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        GameManager.OnIncrementScore += UpdateScore;
        PlayerManager.OnHealthChanged += UpdateHealth;
        GameManager.OnPlayerDeath += ShowDeathMenu;
        Boss.OnBossDefeated += BossDefeated;
    }
}
