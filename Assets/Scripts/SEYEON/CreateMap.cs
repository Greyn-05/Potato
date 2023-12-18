using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class MapPrefab
{
    public GameObject prefab; // �� ������
}

public class CreateMap : MonoBehaviour
{
    public MapPrefab[] mapPrefabs;
    public float maxJumpWidth; // �ִ� ���� ����
    public float maxJumpHeight; // �ִ� ���� ����
    public Vector2 startPosition; // ���� ��ġ
    public float mapLength; // ������ �� ����


    private void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        Vector2 currentPosition = startPosition; // ���� ��ġ�� ���� ��ġ��
        while (currentPosition.x < startPosition.x + mapLength) // ���� ��ġ�� x���� ���� ��ġ�� x�� + �� ���̺��� ���� ��
        {
            MapPrefab mapPrefabData = mapPrefabs[Random.Range(0, mapPrefabs.Length)]; // ������ �迭���� ������ ���� ����
            float mapWidth = mapPrefabData.prefab.GetComponent<Collider2D>().bounds.size.x; // �ݶ��̴����� ������ ũ�� ��������
            float mapHeight = Random.Range(-maxJumpHeight, maxJumpHeight);

            Vector2 spawnPosition = new Vector2 // ������ ��ġ ����
                (
                currentPosition.x + mapWidth / 2 + Random.Range(maxJumpWidth / 2, maxJumpWidth),
                currentPosition.y + mapHeight
                );

            Instantiate(mapPrefabData.prefab, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity); // ������ �ν��Ͻ�ȭ�ؼ� ��ġ

            currentPosition = spawnPosition + new Vector2(mapWidth / 2, 0); // ��ġ ������Ʈ
        }
    }
}
