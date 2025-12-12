using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;


public class EnemySpawner : MonoBehaviour
{
    public UIManager uiManager;
    public GameObject enemyPrefab;
    public GameManager gm;
    public float spawnDelay = 0.2f;
    public float spawnRadius = 10f;
    public Transform player;

    public BoxCollider2D spawnArea;
    private Camera mainCamera;
    public int currentWave = 1;
    

    public List<GameObject> aliveEnemies = new List<GameObject>();
    private bool isSpawningWave = false;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawningWave && aliveEnemies.Count == 0) 
        {
            currentWave++;
            uiManager.UpdateWaveCounter(currentWave);
            SpawnWave();
        }
    }

    private void SpawnWave() 
    {
        isSpawningWave =true;
        int enemiesToSpawn = currentWave * 5;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            
        }
        isSpawningWave=false;
        enemiesToSpawn=0;
    }

    public void SpawnEnemy()
    {
        Vector2 spawnPos = GetRandomSpawnPos();
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        aliveEnemies.Add(enemy);

    }

    public Vector2 GetRandomSpawnPos()
    {
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector2 spawnPosition;


        int safety = 0;
        do
        {
            safety++;
            if (safety > 50)
            {
                
                return spawnArea.bounds.center;
            }

            bool spawnOutsideHoriz = Random.value > 0.5f;

            if (spawnOutsideHoriz)
            {
                // Left or Right of camera
                float x = player.position.x + (Random.value > 0.5f ? 1 : -1) * (cameraWidth / 2 + spawnRadius);
                float y = player.position.y + Random.Range(-cameraHeight / 2, cameraHeight / 2);

                spawnPosition = new Vector2(x, y);
            }
            else
            {
                // Above or Below camera
                float x = player.position.x + Random.Range(-cameraWidth / 2, cameraWidth / 2);
                float y = player.position.y + (Random.value > 0.5f ? 1 : -1) * (cameraHeight / 2 + spawnRadius);

                spawnPosition = new Vector2(x, y);
            }

        }
        while (!spawnArea.bounds.Contains(spawnPosition));

        return spawnPosition;
    }

    public void RemoveEnemyFromWave(GameObject enemy)
    {
        aliveEnemies.Remove(enemy);
    }
}
