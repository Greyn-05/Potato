using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class MapPrefab
{
    public GameObject prefab; // 맵 프리팹
    public float width;
    public float height;
}

public class CreateMap : MonoBehaviour
{
    public MapPrefab[] mapPrefabs; // 프리팹 배열
    public float maxJumpWidth; // 최대 점프 가로
    public float maxJumpHeight; // 최대 점프 세로
    public Vector2 startPosition; // 시작 위치
    public float mapLength; // 맵 가로
    public float mapHeight; // 맵 세로
    public float minDistanceBetweenPrefabs; // 프리팹끼리 최소 거리
    public float minHeightDifference; // 프리팹끼리 최소 높이 차이


    private void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        Vector2 currentPosition = startPosition; // 현재 위치를 시작 위치로
        float lastPrefabWidth = 0; // 마지막 프리팹 가로 저장
        float lastPrefabHeight = 0; // 마지막 프리팹 세로 저장

        while (currentPosition.x < startPosition.x + mapLength) // 현재 위치의 x값이 시작 위치의 x값 + 맵 길이보다 작을 때
        {
            MapPrefab mapPrefabData = mapPrefabs[Random.Range(0, mapPrefabs.Length)]; // 프리팹 배열에서 프리팹 랜덤 선택
            float prefabWidth = mapPrefabData.width; // 입력한 길이 가져옴
            float prefabHeight = mapPrefabData.height; // 입력한 높이 가져옴

            float nextXPosition = currentPosition.x + lastPrefabWidth / 2 + prefabWidth / 2 + Random.Range(minDistanceBetweenPrefabs, maxJumpWidth); // 다음 프리팹 위치 계산
            float nextYPosition = currentPosition.y + lastPrefabHeight / 2 + prefabHeight / 2 + Random.Range(-maxJumpHeight, maxJumpHeight); // 다음 프리팹 위치 계산

            nextYPosition = Mathf.Clamp(nextYPosition, startPosition.y, startPosition.y + mapHeight - prefabHeight); // // 세로 위치가 맵 범위를 넘지 않게

            Vector2 nextPosition = new Vector2(nextXPosition, nextYPosition); // 다음 프리팹의 위치 결정

            Instantiate(mapPrefabData.prefab, new Vector3(nextPosition.x, nextPosition.y, 0), Quaternion.identity); // 프리팹 생성

            currentPosition = nextPosition; // 현재 위치 업데이트
            lastPrefabWidth = prefabWidth; // 마지막 프리팹 가로 업데이트
            lastPrefabHeight = prefabHeight; // 마지막 프리팹 세로 업데이트
        }
    }
}