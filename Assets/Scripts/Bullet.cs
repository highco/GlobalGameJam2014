using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public PlayerType Type { 
        set {
            _type = value;
            SetupTheLook();
        } 
        get { 
            return _type;
        }
    }

    private PlayerType _type;

    public void SetupTheLook()
    {
        this.renderer.material.color = _type.ToColor();
    }
}
