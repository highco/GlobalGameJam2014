using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public CharacterType Type { 
        get {return _type;}
    }
    public Character owner;

    private CharacterType _type;

    public void SetTypeAndOwner(CharacterType aType, Character anOwner)
    {
        _type = aType;
        owner = anOwner;
        this.renderer.material.color = owner.player.color;
    }
}
