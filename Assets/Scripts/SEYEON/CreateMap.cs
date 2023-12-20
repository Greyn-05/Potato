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
    public float width; // 가로 길이
    public float height; // 세로 길이
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
    public float maxJumpWidth; // 점프 최대 길이
    public float maxJumpHeight; // 점프 최대 높이
    public Vector2 startPosition; // 맵 생성 시작 위치
    public float mapLength; // 맵 가로
    public float mapHeight; // 맵 세로
    public float minDistanceBetweenPrefabs; // 프리팹 사이 최소 거리
    public float minHeightDifference; // 프리팹 사이 최소 높이

    public GameObject trapPrefabs; // 함정
    public int numberOfTraps; // 함정 갯수

    public GameObject goldBox; // 금박스

    public GameObject[] portalPrefabs; // 포탈 프리팹

    private List<PrefabData> createdPrefabsData = new List<PrefabData>(); // 프리팹 데이터 저장 배열


    private void Start()
    {
        PlaceMap();
    }

    public void PlaceMap()
    {
        int maxPrefabCount = 25; // 최대 생성 갯수
        int createdPrefabCount = 0; // 생성된 프리팹 갯수
        int attemptCount = 0; // 시도횟수

        while (createdPrefabCount < maxPrefabCount) // 최대 갯수 생성까지 반복
        {
            MapPrefab mapPrefabData = mapPrefabs[Random.Range(0, mapPrefabs.Length)]; // 랜덤 프리팹 선택
            float prefabWidth = mapPrefabData.width; // 가로 입력값
            float prefabHeight = mapPrefabData.height; // 세로 입력값

            Vector2 nextPosition; // 다음 생성 위치
            bool positionIsValid; // 위치 유효?

            do
            {
                positionIsValid = true;
                float nextXPosition = startPosition.x + Random.Range(0, mapLength - prefabWidth); // 랜덤 x
                float nextYPosition = startPosition.y + Random.Range(0, mapHeight - prefabHeight); // 랜덤 y

                nextPosition = new Vector2(nextXPosition, nextYPosition);

                foreach (var prefabData in createdPrefabsData)
                {
                    float horizontalDistance = Mathf.Abs(prefabData.position.x - nextPosition.x) - (prefabData.width / 2 + prefabWidth / 2);
                    float verticalDistance = Mathf.Abs(prefabData.position.y - nextPosition.y) - (prefabData.height / 2 + prefabHeight / 2);

                    if (horizontalDistance < minDistanceBetweenPrefabs && verticalDistance < minHeightDifference)
                    {
                        positionIsValid = false;
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
                Instantiate(mapPrefabData.prefab, new Vector3(nextPosition.x, nextPosition.y, 0), Quaternion.identity);
                createdPrefabsData.Add(new PrefabData { position = nextPosition, width = prefabWidth, height = prefabHeight, isGround = true });
                createdPrefabCount++; 
                attemptCount = 0; 
            }
            else
            {
                break;
            }
        }

        PlaceTraps();
        PlaceGoldBoxes();
        PlacePortals();
    }

    private void PlaceTraps()
    {
        List<PrefabData> groundPrefabs = new List<PrefabData>();

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
                break;
            }

            int randomIndex = Random.Range(0, groundPrefabs.Count);
            PrefabData groundPrefab = groundPrefabs[randomIndex];

            float trapX = groundPrefab.position.x;
            float trapY = groundPrefab.position.y + (groundPrefab.height / 2); 

            GameObject trapInstance = Instantiate(trapPrefabs, new Vector3(trapX, trapY, 0), Quaternion.identity);

            trapInstance.transform.position = groundPrefab.position;

            groundPrefabs.RemoveAt(randomIndex); 
        }
    }

    private void PlaceGoldBoxes()
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
    }

    private bool IsTrapOn(Vector2 position, float tolerance = 0.1f) 
    {
        foreach (GameObject trap in GameObject.FindGameObjectsWithTag("Trap"))
        {
            if (Vector2.Distance(trap.transform.position, position) <= tolerance)
            {
                return true;
            }
        }

        return false;
    }

    public void PlacePortals()
    {
        List<PrefabData> groundPrefabs = new List<PrefabData>(createdPrefabsData);

        for (int i = 0; i < portalPrefabs.Length; i++)
        {
            bool placed = false;
            int attempts = 0;

            while (!placed && attempts < 500) // 최대 500번 시도
            {
                int randomIndex = Random.Range(0, groundPrefabs.Count);
                PrefabData groundPrefab = groundPrefabs[randomIndex];

                float portalX = groundPrefab.position.x;
                float portalY = groundPrefab.position.y + (groundPrefab.height / 2);

                if (!IsTrapAndBox(new Vector2(portalX, portalY), 0.1f)) // 0.1f는 허용오차
                {
                    GameObject portalInstance = Instantiate(portalPrefabs[i], new Vector3(portalX, portalY, 0), Quaternion.identity);
                    Portal portalScript = portalInstance.GetComponent<Portal>();
                    if (portalScript != null)
                    {
                        portalScript.portalIndex = i + 1;
                        portalScript.CheckPortalActive();
                    }

                    placed = true;
                }
                attempts++;
            }
        }
    }

    private bool IsTrapAndBox(Vector2 position, float tolerance = 0.1f)
    {
        foreach (GameObject trap in GameObject.FindGameObjectsWithTag("Trap"))
        {
            if (Vector2.Distance(trap.transform.position, position) <= tolerance)
            {
                return true;
            }
        }

        foreach (GameObject box in GameObject.FindGameObjectsWithTag("Box"))
        {
            if (Vector2.Distance(box.transform.position, position) <= tolerance)
            {
                return true;
            }
        }

        return false;
    }
}