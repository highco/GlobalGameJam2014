using UnityEngine;
using System.Collections;

public class CharacterShooter : MonoBehaviour
{
    public GameObject[] bulletPrefabs;
    public Transform turret;
    public Transform bulletSpawnPoint;
    public float reloadTime = 1f;
    public float bulletSpeed;
    public Color color;

    private float _reloadingTimer;
    private bool _controlsDisabled;    

    public bool Shoot(float horizontal, float vertical, CharacterType type, float dt)
    {
        _reloadingTimer += dt;

        if (_controlsDisabled)
            return false;

        float xDirection = horizontal;
        float yDirection = vertical;
        if (Mathf.Abs(xDirection) > 0.01 || Mathf.Abs(yDirection) > 0.01)
            turret.transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(yDirection, -xDirection) / Mathf.PI * 180f, 0f); 

        if (_reloadingTimer > reloadTime &&
            (Mathf.Abs(xDirection) > 0.01 || Mathf.Abs(yDirection) > 0.01))
        {
            Shoot(type);
            return true;
        }
        return false;
    }

    void Shoot(CharacterType type)
    {
        _reloadingTimer = 0f;

        GameObject bullet = Instantiate(bulletPrefabs[(int)type], bulletSpawnPoint.position, Quaternion.identity) as GameObject;
        Vector3 direction = turret.transform.forward;
        direction = Quaternion.Euler(0f, Random.Range(-4f, 4f), 0f) * direction;
        bullet.rigidbody.AddForce(direction * bulletSpeed);
        bullet.transform.rotation = turret.transform.rotation;
        bullet.transform.parent = GameObject.FindWithTag("DynamicObjects").transform;
        bullet.transform.localScale = bullet.transform.localScale * (0.8f + Random.Range(0f, 0.3f));
        bullet.GetComponent<Bullet>().SetTypeAndOwner(type, GetComponent<Character>());
        Destroy(bullet, 1f);
    }

    public void SetDash(bool dash)
    {
        _controlsDisabled = dash;
    }
    
    IEnumerator EnableControlsAfter(float time)
    {
        yield return new WaitForSeconds(time);
        _controlsDisabled = false;
    }
}
