using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {

    public GUIText[] playerScoreLabels;

    public void ShowScoreForPlayer(Player player)
    {
        playerScoreLabels[player.index].text = player.name + ": " + player.score; 
    }
}
