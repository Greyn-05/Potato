using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Image status;
    private bool statVisible = false;

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
