using UnityEngine;
using TMPro; // Для использования TextMeshPro

public class SignInteraction : MonoBehaviour
{
    public GameObject textPanel;        // Панель с текстом
    public TextMeshProUGUI messageText; // UI TextMeshPro, где пишется сообщение
    [TextArea] public string message;   // Само сообщение, которое будет показано

    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            textPanel.SetActive(!textPanel.activeSelf);
            messageText.text = message;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            textPanel.SetActive(false); // Скрываем, когда уходим от таблички
        }
    }
}
