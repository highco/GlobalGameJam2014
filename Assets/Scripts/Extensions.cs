using UnityEngine;
using System.Collections;

public static class Extensions {

	public static Color ToColor(this PlayerType type)
    {
        switch(type)
        {
            default:
            case PlayerType.Paper:
                return  Color.white;
            case PlayerType.Rock:
                return  Color.black;
            case PlayerType.Scissors:
                return  Color.blue;
        }
    }

    public static PlayerType GetVictim(this PlayerType type)
    {
        switch(type)
        {
            default:
            case PlayerType.Paper:
                return PlayerType.Rock;
            case PlayerType.Rock:
                return PlayerType.Scissors;
            case PlayerType.Scissors:
                return PlayerType.Paper;
        }
    }

    public static PlayerType GetPredator(this PlayerType type)
    {
        switch(type)
        {
            default:
            case PlayerType.Paper:
                return PlayerType.Scissors;
            case PlayerType.Rock:
                return PlayerType.Paper;
            case PlayerType.Scissors:
                return PlayerType.Rock;
        }
    }
}
