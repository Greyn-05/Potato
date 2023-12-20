using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject knightPrefab; // �ӽ���ġ
    public GameObject PlayerCameraPrefab;
    public GameObject UIPrefab;
    // GameManager�� ���� �ν��Ͻ��� �����ϴ� ���� �Ӽ�
    public static GameManager Instance { get; private set; }

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
        InstantiateUI();
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
        Instantiate(UIPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}