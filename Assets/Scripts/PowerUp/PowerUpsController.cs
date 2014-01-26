using UnityEngine;
using System.Collections;

public class PowerUpsController : MonoBehaviour {

    public GameObject[] _spawnPoints;
    public GameObject[] powerupPrefabsForTypes;

    public float spawningInterval = 6f;
    public float spawningVariance = 2f;

    private float _spawningTimer;
    private float _nextSpawnTime;

    void Awake()
    {
        _spawnPoints = GameObject.FindGameObjectsWithTag("PowerUpRespawn");
        _nextSpawnTime = spawningInterval + Random.Range(-spawningVariance / 2f, spawningVariance / 2f);
    }

    void Update()
    {
        _spawningTimer += Time.deltaTime;
        if (_spawningTimer > _nextSpawnTime)
        {
            GameObject spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            PowerUpSpawnPoint point = spawnPoint.GetComponent<PowerUpSpawnPoint>();
            Spawn(point);

            spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            point = spawnPoint.GetComponent<PowerUpSpawnPoint>();
            Spawn(point);

            _nextSpawnTime = spawningInterval + Random.Range(-spawningVariance / 2f, spawningVariance / 2f);
            _spawningTimer = 0f;
        }
    }

    void Spawn(PowerUpSpawnPoint point)
    {
        if (point.occupier == null)
        {
            PowerupType type = point.possibleTypes[Random.Range(0, point.possibleTypes.Length)];
            PowerUp powerUp = (Instantiate(powerupPrefabsForTypes[(int)type], 
                                           point.transform.position, 
                                           Quaternion.Euler(90f, 0f, 0f)) as GameObject).GetComponent<PowerUp>();
            powerUp.type = type;
            powerUp.spawnPoint = point;
            point.occupier = powerUp;
        }
    }
}
