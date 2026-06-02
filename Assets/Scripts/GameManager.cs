using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonParent;
    public List<GameObject> backgrounds = new List<GameObject>();
    private int currentLevel = 0;
    private int firstClick = 0;
    private List<GameObject> svetoforishe = new List<GameObject>();
    private float timer_1 = 0;

    private List<GameObject> spawnedButtons =
        new List<GameObject>();

    public List<LevelsScript> levels = new List<LevelsScript>();

    public GameObject traficLight;
    public TMP_Text score;
    public int points = 0;

    public TMP_Text time;
    public int counter = 15;

    private Vector2[][] levelPositions =
    {
        new Vector2[]
        {
            new Vector2(-311, 76),
            new Vector2(314, 76)
        },

        new Vector2[]
        {
            new Vector2(357, 106),
            new Vector2(-345, 106),
            new Vector2(0, 172)
        },

        new Vector2[]
        {
            new Vector2(222, 167),
            new Vector2(-214, 167),
            new Vector2(-241, -112),
            new Vector2(250, -112)
        },

        new Vector2[]
        {
            new Vector2(-270, -117),
            new Vector2(273, -117),
            new Vector2(311, 115),
            new Vector2(0, 200),
            new Vector2(-303, 126)
        }
    };

    private string maxNumber;

    private void Start()
    {
        ShuffleLevels(levels);
        CreateTraficLight();
        LoadLevel();
    }

    void Update()
    {
        timer_1 += Time.deltaTime;
        if (timer_1 > 1)
        {
            counter = counter - 1;
            timer_1 = 0f;
        }
        //time.text = $"Время: {counter}";
        if (counter % 3 == 0 && counter != 15){
            svetoforishe[counter / 3].SetActive(false);
        }
        if (counter == 0)
        {
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
        Vector3 newpos = new Vector3(0, 0, 0);
        GameObject currentBH = GameObject.FindWithTag("Background");
        if (firstClick != 0)
        {
            Destroy(currentBH);
        }
        else{
            firstClick = 1;
        }
        Instantiate(backgrounds[numbers.Count - 2], newpos, Quaternion.identity);

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

            score.text = $"{points}";

            currentLevel++;

            if (currentLevel >= levels.Count)
            {
                Debug.Log("Игра пройдена!");
                UnityEditor.EditorApplication.isPlaying = false;
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

            score.text = $"{points}";
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

    void CreateTraficLight()
    {
        for (int i = 0; i < 5; i++){
            Vector3 newpos = new Vector3(5.6f + i * 0.9f , -4.5f, 0);
            GameObject jeden = Instantiate(traficLight, newpos, Quaternion.identity);
            svetoforishe.Add(jeden);
        }
    }
}