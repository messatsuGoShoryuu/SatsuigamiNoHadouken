using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    Text m_text;
    string m_points;
    string m_lives;

    // Use this for initialization
    void Start()
    {
        EventSystem.AddListener<Event_LivesUpdated>(OnLivesUpdated);
        EventSystem.AddListener<Event_PointsUpdated>(OnPointsUpdated);
		EventSystem.AddListener<Event_GameOver>(OnGameOver);
        m_points = GameState.points.ToString();
        m_lives = GameState.lives.ToString();
    }

    private void OnGameOver(Event_GameOver data)
    {
		EventSystem.RemoveListener<Event_LivesUpdated>(OnLivesUpdated);
		EventSystem.RemoveListener<Event_PointsUpdated>(OnPointsUpdated);
        EventSystem.RemoveListener<Event_GameOver>(OnGameOver);
        m_text.text = "GAME OVER! YOU WERE BANNED FROM THE CHURCH OF SATSUI!";
        StartCoroutine(RestartGameAfter5Seconds());
    }

    IEnumerator RestartGameAfter5Seconds()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("MainMenu");
    }

    private void OnPointsUpdated(Event_PointsUpdated data)
    {
		m_points = GameState.points.ToString();
		UpdateText();
    }

    private void OnLivesUpdated(Event_LivesUpdated data)
    {
		m_lives = GameState.lives.ToString();
        UpdateText();
    }

    void UpdateText()
    {
        m_text.text = "POINTS: " + m_points + "\nLIFE: " + m_lives;
    }
}
