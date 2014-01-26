using UnityEngine;
using System.Collections;
using System;

public class GUI : MonoBehaviour {

    public GUIText[] playerScoreLabels;
    public GUIText timerLabel;

    private float _timeLeft = 120f;

    public void ShowScoreForPlayer(Player player)
    {
        playerScoreLabels[player.index].text = player.name + ": " + player.score; 
        playerScoreLabels[player.index].color = player.color;
    }

    void Update()
    {
        _timeLeft -= Time.deltaTime;
        timerLabel.text = FormatSeconds(_timeLeft);
    }

    string FormatSeconds(float elapsed)
    {
        int d = (int)(elapsed * 100.0f);
        int minutes = d / (60 * 100);
        int seconds = (d % (60 * 100)) / 100;
        int hundredths = d % 100;
        return String.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, hundredths);
    }
}
