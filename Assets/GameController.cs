using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public PlayerType[] typesForPlayers;

    private GameObject[] _spawnPoints;
    private List<Player> _players = new List<Player>();

    void Awake()
    {
        _spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
    }

    void Start()
    {
        SpawnPlayersAtStart();
    }

    void SpawnPlayersAtStart()
    {
        List<GameObject> unusedSpawnPoints = new List<GameObject>(_spawnPoints);
        foreach (PlayerType type in typesForPlayers)
        {
            Transform spawnPoint = unusedSpawnPoints[Random.Range(0 , unusedSpawnPoints.Count)].transform;
            Player newPlayer = (Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity) as GameObject).GetComponent<Player>();
            newPlayer.playerIndex = (int)type;
            newPlayer.InitializeWithType(type);
            unusedSpawnPoints.Remove(spawnPoint.gameObject);
        }
    }

    void Update()
    {
        
    }
}
