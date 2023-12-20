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

    private HealthSystem healthSystem;

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
        Debug.Log(statVisible ? "생겨라!" : "사라져라!");
    }

    private void HandleDeath()
    {
        Gameover();
    }

    public void Gameover()
    {
        gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void GameClear()
    {
        gameclearUI.SetActive(true);
    }

    public void clearscean()
    {
        FadeManager.Instance.StartFade();
        endingscene.SetActive(true);
    }

}
