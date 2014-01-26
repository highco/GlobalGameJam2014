using UnityEngine;
using System.Collections;

public static class Extensions {

	public static Color ToColor(this CharacterType type)
    {
        switch(type)
        {
            default:
            case CharacterType.Paper:
                return  Color.white;
            case CharacterType.Rock:
                return  Color.black;
            case CharacterType.Scissors:
                return  Color.blue;
        }
    }

    public static CharacterType GetVictim(this CharacterType type)
    {
        switch(type)
        {
            default:
            case CharacterType.Paper:
                return CharacterType.Rock;
            case CharacterType.Rock:
                return CharacterType.Scissors;
            case CharacterType.Scissors:
                return CharacterType.Paper;
        }
    }

    public static CharacterType GetPredator(this CharacterType type)
    {
        switch(type)
        {
            default:
            case CharacterType.Paper:
                return CharacterType.Scissors;
            case CharacterType.Rock:
                return CharacterType.Paper;
            case CharacterType.Scissors:
                return CharacterType.Rock;
        }
    }

    public static CharacterType ToCharacterType(this PowerupType type)
    {
        switch(type)
        {
            default:
            case PowerupType.ShapeShiftRock:
                return CharacterType.Rock;
            case PowerupType.ShapeShiftPaper:
                return CharacterType.Paper;
            case PowerupType.ShapeShiftScissors:
                return CharacterType.Scissors;
        }
    }
}
