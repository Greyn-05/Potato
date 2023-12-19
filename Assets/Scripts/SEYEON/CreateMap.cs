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
    public float width;
    public float height;
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


    private void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        Vector2 currentPosition = startPosition; // ���� ��ġ�� ���� ��ġ��
        float lastPrefabWidth = 0; // ������ ������ ���� ����
        float lastPrefabHeight = 0; // ������ ������ ���� ����

        while (currentPosition.x < startPosition.x + mapLength) // ���� ��ġ�� x���� ���� ��ġ�� x�� + �� ���̺��� ���� ��
        {
            MapPrefab mapPrefabData = mapPrefabs[Random.Range(0, mapPrefabs.Length)]; // ������ �迭���� ������ ���� ����
            float prefabWidth = mapPrefabData.width; // �Է��� ���� ������
            float prefabHeight = mapPrefabData.height; // �Է��� ���� ������

            float nextXPosition = currentPosition.x + lastPrefabWidth / 2 + prefabWidth / 2 + Random.Range(minDistanceBetweenPrefabs, maxJumpWidth); // ���� ������ ��ġ ���
            float nextYPosition = currentPosition.y + lastPrefabHeight / 2 + prefabHeight / 2 + Random.Range(-maxJumpHeight, maxJumpHeight); // ���� ������ ��ġ ���

            nextYPosition = Mathf.Clamp(nextYPosition, startPosition.y, startPosition.y + mapHeight - prefabHeight); // // ���� ��ġ�� �� ������ ���� �ʰ�

            Vector2 nextPosition = new Vector2(nextXPosition, nextYPosition); // ���� �������� ��ġ ����

            Instantiate(mapPrefabData.prefab, new Vector3(nextPosition.x, nextPosition.y, 0), Quaternion.identity); // ������ ����

            currentPosition = nextPosition; // ���� ��ġ ������Ʈ
            lastPrefabWidth = prefabWidth; // ������ ������ ���� ������Ʈ
            lastPrefabHeight = prefabHeight; // ������ ������ ���� ������Ʈ
        }
    }
}