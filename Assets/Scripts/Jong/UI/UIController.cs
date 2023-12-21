// UIController.cs

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject status;
    public GameObject gameOverUI;
    public GameObject gameclearUI;
    public GameObject endingscene;
    private bool statVisible = false;

    public static UIController Instance { get; private set; }
    private HealthSystem healthSystem;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        healthSystem = FindObjectOfType<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.OnDeath += HandleDeath;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleUI();
        }
    }

    private void ToggleUI()
    {
        statVisible = !statVisible;
        status.gameObject.SetActive(statVisible);
        Debug.Log(statVisible ? "���ܶ�!" : "�������!");
    }

    private void HandleDeath()
    {
        Gameover();
    }

    public void Gameover()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.stageNumber = 1;
    }

    public void ExitMenu()
    {
        Time.timeScale = 1f;
        RestartScene();
        GameManager.Instance.stageNumber = 1;
    }

    public void GameClear()
    {
        gameclearUI.SetActive(true);
    }

    public void clearscean()
    {
        FadeManager.Instance.StartFade();
        endingscene.SetActive(true);
        Invoke("RestartScene", 8f);
    }
    public void RestartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

}
