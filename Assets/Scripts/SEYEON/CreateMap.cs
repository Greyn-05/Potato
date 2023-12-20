using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class MapPrefab
{
    public GameObject prefab; // �� ������
    public float width; // ������ ����
    public float height; // ������ ����
}

[Serializable]
public struct PrefabData
{
    public Vector2 position; // ������ ��ġ
    public float width; // ����
    public float height; // ����
    public bool isGround; // ������
}

public class CreateMap : MonoBehaviour
{
    public MapPrefab[] mapPrefabs; // ������ �迭
    public float maxJumpWidth; // �ִ� ���� ����
    public float maxJumpHeight; // �ִ� ���� ����
    public Vector2 startPosition; // ���� ��ġ
    public float mapLength; // �� ����
    public float mapHeight; // �� ����
    public float minDistanceBetweenPrefabs; // �����ճ��� �ּ� �Ÿ�
    public float minHeightDifference; // �����ճ��� �ּ� ���� ����

    public GameObject trapPrefabs; // ���� ������
    public int numberOfTraps; // ���� ����

    public GameObject goldBox; // ����

    private List<PrefabData> createdPrefabsData = new List<PrefabData>(); // ������ ������ ����


    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        int maxPrefabCount = 20; // �ִ� �� ���� ����
        int createdPrefabCount = 0; // ������ ������ ����
        int attemptCount = 0; // Ž�� �õ� Ƚ��

        while (createdPrefabCount < maxPrefabCount) // �ִ� ������ �������� �ݺ�
        {
            MapPrefab mapPrefabData = mapPrefabs[Random.Range(0, mapPrefabs.Length)]; // ������ �迭���� ������ ���� ����
            float prefabWidth = mapPrefabData.width; // �Է��� ���� ������
            float prefabHeight = mapPrefabData.height; // �Է��� ���� ������

            Vector2 nextPosition; // ���� ������ ���� ��ġ
            bool positionIsValid; // ��ġ ��ȿ����

            do
            {
                positionIsValid = true;
                float nextXPosition = startPosition.x + Random.Range(0, mapLength - prefabWidth); // x�� �� ���� �ȿ��� ����
                float nextYPosition = startPosition.y + Random.Range(0, mapHeight - prefabHeight); // y�� �� ���� �ȿ��� ����

                nextPosition = new Vector2(nextXPosition, nextYPosition);

                foreach (var prefabData in createdPrefabsData) // ������ �����յ� ��ġ�� ��ġ���� �ƴ��� 
                {
                    float horizontalDistance = Mathf.Abs(prefabData.position.x - nextPosition.x) - (prefabData.width / 2 + prefabWidth / 2);
                    float verticalDistance = Mathf.Abs(prefabData.position.y - nextPosition.y) - (prefabData.height / 2 + prefabHeight / 2);

                    if (horizontalDistance < minDistanceBetweenPrefabs && verticalDistance < minHeightDifference)
                    {
                        positionIsValid = false; // ��ġ�� false
                        break;
                    }
                }

                attemptCount++;
                if (attemptCount > 100)
                {
                    break;
                }
            }
            while (!positionIsValid);

            if (positionIsValid)
            {
                Instantiate(mapPrefabData.prefab, new Vector3(nextPosition.x, nextPosition.y, 0), Quaternion.identity); // ������ ����
                createdPrefabsData.Add(new PrefabData { position = nextPosition, width = prefabWidth, height = prefabHeight, isGround = true }); // ������ ������ ��ġ, ����, �±� ����
                createdPrefabCount++; // ������ �� ++
                attemptCount = 0; // Ƚ�� �ʱ�ȭ
            }
            else
            {
                break;
            }
        }

        PlaceTraps(); // ���� ��ġ
        PlaceGoldBox(); // �ڽ� ��ġ
    }

    private void PlaceTraps()
    {
        List<PrefabData> groundPrefabs = new List<PrefabData>(); // �� �±� �������� ����Ʈ��

        foreach (var prefabData in createdPrefabsData)
        {
            if (prefabData.isGround)
            {
                groundPrefabs.Add(prefabData);
            }
        }

        for (int i = 0; i < numberOfTraps; i++)
        {
            if (groundPrefabs.Count == 0)
            {
                break; // �� �±װ� 0�̸� break
            }

            int randomIndex = Random.Range(0, groundPrefabs.Count);
            PrefabData groundPrefab = groundPrefabs[randomIndex];

            float trapX = groundPrefab.position.x;
            float trapY = groundPrefab.position.y + (groundPrefab.height / 2); // �� �������� ���� ������ ������ ����

            GameObject trapInstance = Instantiate(trapPrefabs, new Vector3(trapX, trapY, 0), Quaternion.identity);

            trapInstance.transform.position = groundPrefab.position;

            groundPrefabs.RemoveAt(randomIndex); // ����� �� �������� ����Ʈ���� ����
        }
    }

    private void PlaceGoldBox()
    {
        List<PrefabData> groundPrefabs = new List<PrefabData>();

        foreach (var prefabData in createdPrefabsData)
        {
            if (prefabData.isGround && !IsTrapOn(prefabData.position))
            {
                groundPrefabs.Add(prefabData);
            }
        }

        if (groundPrefabs.Count > 0)
        {
            int randomIndex = Random.Range(0, groundPrefabs.Count);
            PrefabData selectedGround = groundPrefabs[randomIndex];
           
            float boxX = selectedGround.position.x;
            float boxY = selectedGround.position.y - 0.5f;
            
            Instantiate (goldBox, new Vector3(boxX, boxY, 0), Quaternion.identity);
        }
        else
        {
            Debug.Log("�ڽ� �ڸ� ����");
        }
    }

    private bool IsTrapOn(Vector2 position, float tolerance = 0.1f) // �������� 0.1f �ȿ� ���� �ִ��� �˻�
    {
        foreach (GameObject trap in GameObject.FindGameObjectsWithTag("Trap"))
        {
            if (Vector2.Distance(trap.transform.position, position) <= tolerance)
            {
                return true; // ���� O
            }
        }

        return false; // ���� X
    }
}