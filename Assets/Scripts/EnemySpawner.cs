using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerTransform; // Assign the player's Transform in the Inspector
    public float spawnInterval = 30f;
    private int enemiesSpawned = 0;
    private int maxEnemies = 2; // Max number of enemies to spawn

    private void Start()
    {
        // Start the spawning process
        StartCoroutine(SpawnEnemiesWithDelay());
    }

    private IEnumerator SpawnEnemiesWithDelay()
    {
        while (enemiesSpawned < maxEnemies)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
            enemiesSpawned++;
        }
    }

    private void SpawnEnemy()
    {
        // Instantiate the enemy at the spawner's position
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        // Set the player as the target for the enemy AI
        if (enemy.GetComponent<EnemyAI>() != null)
        {
            enemy.GetComponent<EnemyAI>().player = playerTransform;
        }
    }
}
