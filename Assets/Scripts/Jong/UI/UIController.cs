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
            // Stat Canvas�� Ȱ��ȭ�Ͽ� UI�� �����ݴϴ�.
            status.gameObject.SetActive(true);
            Debug.Log("���ܶ�!");
        }

        void HideUI()
        {
            // Stat Canvas�� ��Ȱ��ȭ�Ͽ� UI�� ����ϴ�.
            status.gameObject.SetActive(false);
            Debug.Log("�������!");
        }
    }
}
