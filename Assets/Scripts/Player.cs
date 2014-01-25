using UnityEngine;
using System.Collections;

public class Player  {

    public string name;
    public int index;
    public Color color;
    public int score;
    public Character character;

    public Player(string aName, int aIndex)
    {
        name = aName;
        index = aIndex;
    }

    public void DoUpdate(float dt)
    {
        if (character != null)
        {
            character.DoUpdate(dt);
        }
    }
}
