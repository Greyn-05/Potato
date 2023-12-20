using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject knightPrefab; // 임시조치
    public GameObject PlayerCameraPrefab;
    // GameManager의 단일 인스턴스를 저장하는 정적 속성
    public static GameManager Instance { get; private set; }

    private CreateMap createMapScript;
    public int currentStage = 1;
    private int[] stageCorrectPortal = { 2, 3, 1 }; // 각 스테이지 정답 포탈 2(red)-> 3(yellow)-> 1(blue)

    private void Awake()
    {
        // Instance가 null인지 확인한다. null이면 현재 객체를 Instance로 설정한다.
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // 씬이 변경되어도 객체가 파괴되지 않도록 한다.
        }
        else
        {
            Destroy(gameObject); // 다른 인스턴스가 이미 존재하면 새로 생성된 객체를 파괴한다.
        }
    }
    void Start()
    {
        SceneManager.LoadScene("Seyeon", LoadSceneMode.Additive);
        InstantiateKnight();
        InstantPlayerCameraPrefa();
    }


    void InstantiateKnight()
    {
        
        Instantiate(knightPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        
    }

    void InstantPlayerCameraPrefa()
    {
        Instantiate(PlayerCameraPrefab, new Vector3(0, 0, 0), Quaternion.identity);
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
}
