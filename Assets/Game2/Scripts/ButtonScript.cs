using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberButton : MonoBehaviour
{
    public string number;

    private Button button;
    private GameManager1 gameManager;

    public void Init(string value, GameManager1 manager)
    {
        number = value;
        gameManager = manager;

        GetComponentInChildren<TMP_Text>().text =
            number.ToString();

        button = GetComponent<Button>();

        button.onClick.AddListener(OnClick);

        // var a = Camera.main;
    }

    void OnClick()
    {
        StartCoroutine(gameManager.ReceiveNumber(number, GetComponent<RectTransform>().position.x, GetComponent<RectTransform>().position.y));
    }
}