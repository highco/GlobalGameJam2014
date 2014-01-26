using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public GameObject characterPrefab;
    public CharacterType[] typesForCharacters;

    private List<Player> _players = new List<Player>();
    private GameObject[] _spawnPoints;
    private GUI _gui;

    void Awake()
    {
        _spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        _gui = GameObject.FindObjectOfType<GUI>();
    }

    void Start()
    {
        Color[] colors = new Color[] {Color.green, Color.red, Color.yellow};
        for (int i = 0; i < 3; i++)
        {
            Player player = new Player("Player_" + i, i);
            player.score = 0;
            player.color = colors[i];
            _players.Add(player);
        }

        SpawnCharacters();
        UpdateGUI();
    }

    void SpawnCharacters()
    {
        List<GameObject> selectedSpawnPoints = GetRandomSpawnPoints(3);
        int randomTypeIndex = Random.Range(0, typesForCharacters.Length);
        for (int i = 0; i < 3; i++)
        {
            SpawnCharacterForPlayer(_players[i], 
                                    typesForCharacters[(randomTypeIndex + i) % typesForCharacters.Length], 
                                    selectedSpawnPoints[i]);
        }
    }

    List<GameObject> GetRandomSpawnPoints(int numOfSpawnPoints)
    {
        List<GameObject> unusedSpawnPoints = new List<GameObject>(_spawnPoints);
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < numOfSpawnPoints; i++)
        {
            GameObject randomSpawnPoint = unusedSpawnPoints[Random.Range(0 , unusedSpawnPoints.Count)];
            result.Add(randomSpawnPoint);
            unusedSpawnPoints.Remove(randomSpawnPoint);
        }
        return result;
    }

    void UpdateGUI()
    {
        foreach (Player player in _players)
            _gui.ShowScoreForPlayer(player);
    }

    void Update()
    {
        foreach(Player player in _players)
            player.DoUpdate(Time.deltaTime);
    }

    void SpawnCharacterForPlayer(Player player, CharacterType type, GameObject spawnPoint)
    {
        Character newCharacter = (Instantiate(characterPrefab, spawnPoint.transform.position, Quaternion.identity) as GameObject).GetComponent<Character>();
        newCharacter.player = player;
        player.character = newCharacter;
        newCharacter.InitializeWithType(type);
    }

    public void CharacterHit(Bullet bullet, Character deadCharacter)
    {
        bullet.owner.player.score += 1;
        _gui.ShowScoreForPlayer(bullet.owner.player);

        foreach (Player player in _players)
        {
            player.character.Stop();
        }

        deadCharacter.player.character = null;
        deadCharacter.Explode();

        StartCoroutine(WaitAndRespawn(1f, bullet.owner));
    }

    IEnumerator WaitAndRespawn(float waitTime, Character winner)
    {
        yield return new WaitForSeconds(waitTime / 2f);
        foreach(Player player in _players)
        {
            if (player.character != null)
            {
                if (player.character == winner)
                    iTween.ScaleTo(player.character.gameObject, Vector3.one * 1f, waitTime / 4f);
                else
                    iTween.ScaleTo(player.character.gameObject, Vector3.zero, waitTime / 4f);
            }
        }
        yield return new WaitForSeconds(waitTime / 2f);        
        foreach(Player player in _players)
            if (player.character != null)
                Destroy(player.character.gameObject);

        SpawnCharacters();
    }
}
