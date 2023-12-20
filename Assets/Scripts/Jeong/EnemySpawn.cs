using UnityEngine;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // �� ������ �迭
    public List<GameObject> spawnedEnemies = new List<GameObject>(); // ������ ������ ����
    private int currentScenario;
    public int CurrentScenario => currentScenario;
    // ���� �����ϴ� �޼ҵ�
    public void SpawnEnemies(int scenario)
    {
        currentScenario = scenario;
        // ������ ������ ������ ����
        foreach (GameObject enemy in spawnedEnemies)
        {
            Destroy(enemy);
        }
        spawnedEnemies.Clear();

        // ��Ȳ�� ���� �� ����
        switch (scenario)
        {
            case 1:
                SpawnEnemy(0, 7); // Enemy 1�� 7���� ����
                SpawnEnemy(1, 3); // Enemy 2�� 3���� ����
                break;
            case 2:
                SpawnEnemy(1, 5); // Enemy 2�� 5���� ����
                SpawnEnemy(2, 2); // Enemy 3�� 2���� ����
                break;
            case 3:
                SpawnEnemy(0, 5); // Enemy 1�� 5���� ����
                SpawnEnemy(1, 5); // Enemy 2�� 5���� ����
                SpawnEnemy(2, 2); // Enemy 3�� 2���� ����
                break;
            case 4:
                SpawnEnemy(3, 1); // Enemy 4�� 1���� ����
                break;
        }
    }

    // Ư�� ���� ������ ������ŭ �����ϴ� �޼ҵ�
    private void SpawnEnemy(int enemyIndex, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-10, 10), Random.Range(-0, 15));
            GameObject spawnedEnemy = Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity);
            spawnedEnemies.Add(spawnedEnemy);
        }
    }

    // �ٸ� ��ũ��Ʈ���� �� �޼ҵ带 ȣ���Ͽ� �� ���� �ó������� ����
    public void StartSpawn(int stageCount)
    {
        SpawnEnemies(stageCount);
    }
}
