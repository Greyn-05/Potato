using UnityEngine;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 적 프리팹 배열
    public List<GameObject> spawnedEnemies = new List<GameObject>(); // 생성된 적들을 추적
    private int currentScenario;
    public int CurrentScenario => currentScenario;
    // 적을 생성하는 메소드
    public void SpawnEnemies(int scenario)
    {
        currentScenario = scenario;
        // 이전에 생성된 적들을 제거
        foreach (GameObject enemy in spawnedEnemies)
        {
            Destroy(enemy);
        }
        spawnedEnemies.Clear();

        // 상황에 따라 적 생성
        switch (scenario)
        {
            case 1:
                SpawnEnemy(0, 7); // Enemy 1번 7마리 생성
                SpawnEnemy(1, 3); // Enemy 2번 3마리 생성
                break;
            case 2:
                SpawnEnemy(1, 5); // Enemy 2번 5마리 생성
                SpawnEnemy(2, 2); // Enemy 3번 2마리 생성
                break;
            case 3:
                SpawnEnemy(0, 5); // Enemy 1번 5마리 생성
                SpawnEnemy(1, 5); // Enemy 2번 5마리 생성
                SpawnEnemy(2, 2); // Enemy 3번 2마리 생성
                break;
            case 4:
                SpawnEnemy(3, 1); // Enemy 4번 1마리 생성
                break;
        }
    }

    // 특정 적을 지정된 수량만큼 생성하는 메소드
    private void SpawnEnemy(int enemyIndex, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-10, 10), Random.Range(-0, 15));
            GameObject spawnedEnemy = Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity);
            spawnedEnemies.Add(spawnedEnemy);
        }
    }

    // 다른 스크립트에서 이 메소드를 호출하여 적 생성 시나리오를 시작
    public void StartSpawn(int stageCount)
    {
        SpawnEnemies(stageCount);
    }
}
