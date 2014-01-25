using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public SpriteRenderer spriteRenderer;
    public CharacterType Type { 
        get {return _type;}
    }
    public Character owner;

    private CharacterType _type;

    public void SetTypeAndOwner(CharacterType aType, Character anOwner)
    {
        _type = aType;
        owner = anOwner;
        spriteRenderer.color = owner.player.color;
    }

    void OnCollisionEnter(Collision collision) 
    {
        Destroy(this.gameObject);
    }
}
