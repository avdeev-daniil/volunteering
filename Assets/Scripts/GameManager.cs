using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonParent;

    private int currentLevel = 0;

    private List<GameObject> spawnedButtons =
        new List<GameObject>();

    private int[][] levels =
    {
        new int[] { 2, 7 },
        new int[] { 5, 1, 9 },
        new int[] { 4, 12, 8, 3 },
        new int[] { 11, 6, 15, 2, 9 },
        new int[] { 10, 1, 4, 6, 9 },
        new int[] { 9, 3, 4, 1, 8 },
        new int[] { 53, 52, 54, 51, 50 }
    };

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
        },
        new Vector2[]
        {
            new Vector2(300, 100),
            new Vector2(0, 200),
            new Vector2(-300, 100),
            new Vector2(200, 200),
            new Vector2(-200, 200)
        },
        
        new Vector2[]
        {
            new Vector2(300, 100),
            new Vector2(0, 200),
            new Vector2(-300, 100),
            new Vector2(200, 200),
            new Vector2(-200, 200)
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

    private int maxNumber;

    private void Start()
    {
        LoadLevel();
    }

    void LoadLevel()
    {
        ClearButtons();

        int[] numbers = levels[currentLevel];

        Vector2[] positions =
            levelPositions[currentLevel];

        maxNumber = numbers.Max();

        // Перемешиваем числа
        List<int> shuffledNumbers =
            new List<int>(numbers);

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

    public void ReceiveNumber(int number)
    {
        if (number == maxNumber)
        {
            Debug.Log("Правильно!");

            currentLevel++;

            if (currentLevel >= levels.Length)
            {
                Debug.Log("Игра пройдена!");
                return;
            }

            LoadLevel();
        }
        else
        {
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

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex =
                Random.Range(i, list.Count);

            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}