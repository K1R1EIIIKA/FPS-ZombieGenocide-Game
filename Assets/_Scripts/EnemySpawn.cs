using UnityEngine;
using Random = System.Random;

public class EnemySpawn : MonoBehaviour
{
    public int minPositionX = -8;
    public int maxPositionX = 8;
    public int minPositionZ = -1;
    public int maxPositionZ = 6;

    public GameObject enemyPrefab;

    private int _randX;
    private int _randZ;
    private readonly Random _random = new Random();

    private Level _level;
    
    void Start()
    {
        _level = GameObject.Find("EventSystem").GetComponent<Level>();
        
        InvokeRepeating(nameof(SpawnEnemies), 1, 1);
    }

    private void SpawnEnemies()
    {
        if (_level.prepareTime != 0 || _level.timer == 0) return;

        _randX = _random.Next(minPositionX, maxPositionX + 1);
        _randZ = _random.Next(minPositionZ, maxPositionZ + 1);
        
        Instantiate(enemyPrefab, new Vector3(_randX, 1, _randZ), Quaternion.identity, transform);
    }
}
