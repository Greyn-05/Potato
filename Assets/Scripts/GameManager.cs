using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject knightPrefab; // �ӽ���ġ
    public GameObject PlayerCameraPrefab;
    public GameObject UIprefab;
    public GameObject inventoryUIPrefab;
    public GameObject CameraWall;

    public int enemyDeathCount = 0; // ���ī��Ʈ
    private int TotalDeathCount = 5; // 5�̻��̾�� ��Ż Ȱ��ȭ��

    // GameManager�� ���� �ν��Ͻ��� �����ϴ� ���� �Ӽ�
    public static GameManager Instance { get; private set; }

    private CreateMap createMapScript;
    public int currentStage = 1;

    private int[] stageCorrectPortal = { 2, 3, 1 }; // �� �������� ���� ��Ż 2(red)-> 3(yellow)-> 1(blue)

    public static event Action EnemyDeathEvent;

    private void Awake()
    {
        // Instance�� null���� Ȯ���Ѵ�. null�̸� ���� ��ü�� Instance�� �����Ѵ�.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // �ٸ� �ν��Ͻ��� �̹� �����ϸ� ���� ������ ��ü�� �ı��Ѵ�.
        }

        EnemyDeathEvent += EnemyDead;
    }
    void Start()
    {
        SceneManager.LoadScene("Seyeon", LoadSceneMode.Additive);
        InstantiateUI();
        InstantiateCameraWall();
    }

    private void OnDestroy()
    {
        EnemyDeathEvent -= EnemyDead;
    }

    public void OnEnemyDead()
    {
        EnemyDeathEvent?.Invoke();
    }

    void InstantiateKnight()
    {

        Instantiate(knightPrefab, new Vector3(0, 0, 0), Quaternion.identity);

    }

    void InstantPlayerCameraPrefa()
    {
        Instantiate(PlayerCameraPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void InstantiateUI()
    {
        Instantiate(UIprefab, new Vector3(0, 0,0 ), Quaternion.identity);
        Instantiate(inventoryUIPrefab);
    }

    private void InstantiateCameraWall()
    {
        GameObject newCameraWall =  Instantiate(CameraWall, new Vector3(0, 0, 0), Quaternion.identity);
        newCameraWall.transform.position = new Vector3(1, 3, 0);
        GameObject newPlayerCamera = Instantiate(PlayerCameraPrefab, new Vector3(0, 0, 0),Quaternion.identity);
        newPlayerCamera.transform.parent = newCameraWall.transform;

    }

    public void EnterPortal(int portalIndex)
    {
        OnPortalEnter(portalIndex);
    }

    private void OnPortalEnter(int portalIndex)
    {
        if (portalIndex == stageCorrectPortal[currentStage - 1])
        {
            GoToNextStage();
        }
        else
        {
            RestartCurrentStage();
        }
    }

    private void GoToNextStage()
    {
        currentStage++;
        createMapScript.PlaceMap();
        createMapScript.PlacePortals();
    }

    private void RestartCurrentStage()
    {
        createMapScript.PlaceMap();
        createMapScript.PlacePortals();
    }

    public void EnemyDead()
    {
        enemyDeathCount++;
        if (enemyDeathCount >= TotalDeathCount)
        {
            foreach (var portal in FindObjectsOfType<Portal>())
            {
                portal.SetActiveState(true);
            }
        }
    }
}
