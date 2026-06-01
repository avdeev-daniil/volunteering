using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberButton : MonoBehaviour
{
    public int number;

    private Button button;
    private GameManager gameManager;

    public void Init(int value, GameManager manager)
    {
        number = value;
        gameManager = manager;

        GetComponentInChildren<TMP_Text>().text =
            number.ToString();

        button = GetComponent<Button>();

        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        gameManager.ReceiveNumber(number);
    }
}