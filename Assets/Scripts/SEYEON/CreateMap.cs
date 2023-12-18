using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class MapPrefab
{
    public GameObject prefab; // 맵 프리팹
}

public class CreateMap : MonoBehaviour
{
    public MapPrefab[] mapPrefabs;
    public float maxJumpWidth; // 최대 점프 가로
    public float maxJumpHeight; // 최대 점프 세로
    public Vector2 startPosition; // 시작 위치
    public float mapLength; // 생성할 맵 길이


    private void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        Vector2 currentPosition = startPosition; // 현재 위치를 시작 위치로
        while (currentPosition.x < startPosition.x + mapLength) // 현재 위치의 x값이 시작 위치의 x값 + 맵 길이보다 작을 때
        {
            MapPrefab mapPrefabData = mapPrefabs[Random.Range(0, mapPrefabs.Length)]; // 프리팹 배열에서 프리팹 랜덤 선택
            float mapWidth = mapPrefabData.prefab.GetComponent<Collider2D>().bounds.size.x; // 콜라이더에서 프리팹 크기 가져오기
            float mapHeight = Random.Range(-maxJumpHeight, maxJumpHeight);

            Vector2 spawnPosition = new Vector2 // 프리팹 위치 결정
                (
                currentPosition.x + mapWidth / 2 + Random.Range(maxJumpWidth / 2, maxJumpWidth),
                currentPosition.y + mapHeight
                );

            Instantiate(mapPrefabData.prefab, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity); // 프리팹 인스턴스화해서 배치

            currentPosition = spawnPosition + new Vector2(mapWidth / 2, 0); // 위치 업데이트
        }
    }
}
