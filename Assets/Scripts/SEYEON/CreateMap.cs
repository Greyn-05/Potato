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
    public float width; // 프리팹 가로
    public float height; // 프리팹 세로
}

[Serializable]
public struct PrefabData
{
    public Vector2 position; // 프리팹 위치
    public float width; // 가로
    public float height; // 세로
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

    private List<PrefabData> createdPrefabsData = new List<PrefabData>(); // 프리팹 데이터 저장


    private void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        int maxPrefabCount = 15;
        int createdPrefabCount = 0;

        while (createdPrefabCount < maxPrefabCount) // 최대 프리팹 갯수까지
        {
            MapPrefab mapPrefabData = mapPrefabs[Random.Range(0, mapPrefabs.Length)]; // 프리팹 배열에서 프리팹 랜덤 선택
            float prefabWidth = mapPrefabData.width; // 입력한 길이 가져옴
            float prefabHeight = mapPrefabData.height; // 입력한 높이 가져옴

            Vector2 nextPosition; // 다음 프리팹 생성 위치
            bool positionIsValid; // 위치 유효한지

            do
            {
                positionIsValid = true;
                float nextXPosition = startPosition.x + Random.Range(0, mapLength - prefabWidth); // x는 맵 길이 안에서 랜덤
                float nextYPosition = startPosition.y + Random.Range(0, mapHeight - prefabHeight); // y는 맵 높이 안에서 랜덤

                nextPosition = new Vector2(nextXPosition, nextYPosition);

                foreach (var prefabData in createdPrefabsData) // 생성된 프리팹들 위치랑 겹치는지 아닌지 
                {
                    float horizontalDistance = Mathf.Abs(prefabData.position.x - nextPosition.x) - (prefabData.width / 2 + prefabWidth / 2);
                    float verticalDistance = Mathf.Abs(prefabData.position.y - nextPosition.y) - (prefabData.height / 2 + prefabHeight / 2);

                    if (horizontalDistance < minDistanceBetweenPrefabs && verticalDistance < minHeightDifference)
                    {
                        positionIsValid = false; // 겹치면 false
                        break;
                    }
                }
            }
            while (!positionIsValid);

            if (positionIsValid)
            {
                Instantiate(mapPrefabData.prefab, new Vector3(nextPosition.x, nextPosition.y, 0), Quaternion.identity); // 프리팹 생성
                createdPrefabsData.Add(new PrefabData { position = nextPosition, width = prefabWidth, height = prefabHeight }); // 생성된 프리팹 위치, 길이 저장
                createdPrefabCount++; // 프리팹 수 ++
            }
        }
    }
}