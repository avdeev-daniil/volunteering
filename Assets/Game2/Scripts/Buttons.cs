using UnityEngine;
using UnityEngine.UI;

public class ButtonSender : MonoBehaviour
{
    public Button button;
    public GameManager1 manager;

    private void Start()
    {
        button.onClick.AddListener(SendSignal);
    }

    private void SendSignal()
    {
        manager.ReceiveSignal();
    }
}