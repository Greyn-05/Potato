using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject status;
    public GameObject gameOverUI;
    private bool statVisible = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleUI();
        }
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


    private void ToggleUI()
    {
        statVisible = !statVisible;

        status.gameObject.SetActive(statVisible);
        Debug.Log(statVisible ? "생겨라!" : "사라져라!");
    }
}
