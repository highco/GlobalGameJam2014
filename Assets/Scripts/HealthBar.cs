using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    public Color fullColor;
    public Color emptyColor;

    private float _initialScale;

    void Awake()
    {
        _initialScale = transform.localScale.x;
    }

    public void ShowPercentage(float percentage)
    {
        transform.localScale = new Vector3(_initialScale * percentage, transform.localScale.y, transform.localScale.z);
        renderer.material.color = Color.Lerp(emptyColor, fullColor, percentage);
    }
}
