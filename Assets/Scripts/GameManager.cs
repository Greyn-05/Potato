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
    public GameObject CameraWall;


    //public EnemyDeath _enemyDeath;
    public int potalCount;
    // GameManager�� ���� �ν��Ͻ��� �����ϴ� ���� �Ӽ�
    public static GameManager Instance { get; private set; }

    private CreateMap createMapScript;
    public int currentStage = 1;

    private int[] stageCorrectPortal = { 2, 3, 1 }; // �� �������� ���� ��Ż 2(red)-> 3(yellow)-> 1(blue)

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
    }
    void Start()
    {
        SceneManager.LoadScene("Seyeon", LoadSceneMode.Additive);
        InstantiateUI();
        InstantiateCameraWall();
    }

    

   
    void InstantiateUI()
    {
        Instantiate(UIprefab, new Vector3(0, 0,0 ), Quaternion.identity);
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

    public bool EnemyAllDeath()
    {
        Debug.Log(potalCount + "gameManager");
        if (potalCount >= 5)
        {

            return true;
        }
        else
        {
            return false;
        }
    }
}
