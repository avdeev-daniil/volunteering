using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonParent;

    private int currentLevel = 0;

    private List<GameObject> spawnedButtons =
        new List<GameObject>();

    public List<LevelsScript> levels = new List<LevelsScript>();

    public TMP_Text score;
    public int points = 0;

    public TMP_Text time;
    public float counter = 15f;

    private Vector2[][] levelPositions =
    {
        new Vector2[]
        {
            new Vector2(-200, 100),
            new Vector2(200, 100)
        },

        new Vector2[]
        {
            new Vector2(-200, 100),
            new Vector2(0, 200),
            new Vector2(200, 100)
        },

        new Vector2[]
        {
            new Vector2(-300, 100),
            new Vector2(100, 200),
            new Vector2(300, 100),
            new Vector2(-100, 200)
        },

        new Vector2[]
        {
            new Vector2(300, 100),
            new Vector2(0, 200),
            new Vector2(-300, 100),
            new Vector2(200, 200),
            new Vector2(-200, 200)
        }
    };

    private string maxNumber;

    private void Start()
    {
        ShuffleLevels(levels);
        LoadLevel();
    }

    void Update()
    {
        counter = counter - Time.deltaTime;
        time.text = $"Время: {counter}";
        if (counter <= 0){
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    void LoadLevel()
    {
        ClearButtons();

        List<string> numbers = levels[currentLevel].answers;

        Vector2[] positions =
            levelPositions[numbers.Count - 2];

        maxNumber = numbers[levels[currentLevel].rightIndex];

        // Перемешиваем числа
        List<string> shuffledNumbers =
            new List<string>(numbers);

        Shuffle(shuffledNumbers);

        for (int i = 0; i < shuffledNumbers.Count; i++)
        {
            GameObject buttonObj =
                Instantiate(buttonPrefab, buttonParent);

            RectTransform rect =
                buttonObj.GetComponent<RectTransform>();

            rect.anchoredPosition = positions[i];

            NumberButton button =
                buttonObj.GetComponent<NumberButton>();

            button.Init(shuffledNumbers[i], this);

            spawnedButtons.Add(buttonObj);
        }

        Debug.Log("Уровень " + (currentLevel + 1));
    }

    public void ReceiveNumber(string number)
    {
        if (number == maxNumber)
        {
            Debug.Log("Правильно!");

            points = points + 1;

            score.text = $"Счёт: {points}";

            currentLevel++;

            if (currentLevel >= levels.Count)
            {
                Debug.Log("Игра пройдена!");
                return;
            }

            LoadLevel();
        }
        else
        {
            if (points > 0)
            {
                points = points - 1;
            }

            score.text = $"Счёт: {points}";
            Debug.Log("Неправильно!");
        }
    }

    void ClearButtons()
    {
        foreach (GameObject button in spawnedButtons)
        {
            Destroy(button);
        }

        spawnedButtons.Clear();
    }

    void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex =
                Random.Range(i, list.Count);

            string temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void ShuffleLevels(List<LevelsScript> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex =
                Random.Range(i, list.Count);

            LevelsScript temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}