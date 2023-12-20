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
    public bool isGround; // 땅인지
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

    public GameObject trapPrefabs; // 함정 프리팹
    public int numberOfTraps; // 함정 갯수

    public GameObject goldBox; // 상자

    private List<PrefabData> createdPrefabsData = new List<PrefabData>(); // 프리팹 데이터 저장


    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        int maxPrefabCount = 20; // 최대 땅 생성 갯수
        int createdPrefabCount = 0; // 생성된 프리팹 갯수
        int attemptCount = 0; // 탐색 시도 횟수

        while (createdPrefabCount < maxPrefabCount) // 최대 프리팹 갯수까지 반복
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

                attemptCount++;
                if (attemptCount > 100)
                {
                    break;
                }
            }
            while (!positionIsValid);

            if (positionIsValid)
            {
                Instantiate(mapPrefabData.prefab, new Vector3(nextPosition.x, nextPosition.y, 0), Quaternion.identity); // 프리팹 생성
                createdPrefabsData.Add(new PrefabData { position = nextPosition, width = prefabWidth, height = prefabHeight, isGround = true }); // 생성된 프리팹 위치, 길이, 태그 저장
                createdPrefabCount++; // 프리팹 수 ++
                attemptCount = 0; // 횟수 초기화
            }
            else
            {
                break;
            }
        }

        PlaceTraps(); // 함정 설치
        PlaceGoldBox(); // 박스 설치
    }

    private void PlaceTraps()
    {
        List<PrefabData> groundPrefabs = new List<PrefabData>(); // 땅 태그 프리팹을 리스트로

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
                break; // 땅 태그가 0이면 break
            }

            int randomIndex = Random.Range(0, groundPrefabs.Count);
            PrefabData groundPrefab = groundPrefabs[randomIndex];

            float trapX = groundPrefab.position.x;
            float trapY = groundPrefab.position.y + (groundPrefab.height / 2); // 땅 프리팹의 세로 길이의 절반을 더함

            GameObject trapInstance = Instantiate(trapPrefabs, new Vector3(trapX, trapY, 0), Quaternion.identity);

            trapInstance.transform.position = groundPrefab.position;

            groundPrefabs.RemoveAt(randomIndex); // 사용한 땅 프리팹은 리스트에서 제거
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
            Debug.Log("박스 자리 없음");
        }
    }

    private bool IsTrapOn(Vector2 position, float tolerance = 0.1f) // 오차범위 0.1f 안에 함정 있는지 검사
    {
        foreach (GameObject trap in GameObject.FindGameObjectsWithTag("Trap"))
        {
            if (Vector2.Distance(trap.transform.position, position) <= tolerance)
            {
                return true; // 함정 O
            }
        }

        return false; // 함정 X
    }
}