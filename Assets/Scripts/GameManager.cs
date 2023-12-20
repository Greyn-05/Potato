using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
        SceneManager.LoadScene("Seyeon");
    }
    void Update()
    {
        
    }
}
