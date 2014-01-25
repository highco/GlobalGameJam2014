using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    private float _initialScale;

    void Awake()
    {
        _initialScale = transform.localScale.x;
    }

    public void ShowPercentage(float percentage)
    {
        transform.localScale = new Vector3(_initialScale * percentage, transform.localScale.y, transform.localScale.z);
    }
}
