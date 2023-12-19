using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    private GameObject status;
    private GameObject gameOverUI;
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

        if (statVisible)
        {
            ShowUI();
        }
        else
        {
            HideUI();
        }
        void ShowUI()
        {
            // Stat Canvas를 활성화하여 UI를 보여줍니다.
            status.gameObject.SetActive(true);
            Debug.Log("생겨라!");
        }

        void HideUI()
        {
            // Stat Canvas를 비활성화하여 UI를 숨깁니다.
            status.gameObject.SetActive(false);
            Debug.Log("사라져라!");
        }
    }
}
