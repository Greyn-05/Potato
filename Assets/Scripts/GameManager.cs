using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject knightPrefab; // �ӽ���ġ
    public GameObject PlayerCameraPrefab;
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
            //DontDestroyOnLoad(gameObject); // ���� ����Ǿ ��ü�� �ı����� �ʵ��� �Ѵ�.
        }
        else
        {
            Destroy(gameObject); // �ٸ� �ν��Ͻ��� �̹� �����ϸ� ���� ������ ��ü�� �ı��Ѵ�.
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
