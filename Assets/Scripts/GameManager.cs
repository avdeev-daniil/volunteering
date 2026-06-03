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
    private int firstClick = -1;
    private List<GameObject> svetoforishe = new List<GameObject>();
    private float timer_1 = 0;
    public GameObject firstScreen;
    public TMP_Text firstText;
    public TMP_Text firstText2;

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
            new Vector2(-184, 76),
            new Vector2(185, 76)
        },

        new Vector2[]
        {
            new Vector2(-395, 51),
            new Vector2(2, 74),
            new Vector2(400, 56)
        },

        new Vector2[]
        {
            new Vector2(-400, 53),
            new Vector2(-181, 148),
            new Vector2(180, 49),
            new Vector2(399, 143)
        },

        new Vector2[]
        {
            new Vector2(197, 154),
            new Vector2(-195, 149),
            new Vector2(403, 50),
            new Vector2(-398, 42),
            new Vector2(-8, 45)
        }
    };

    private string maxNumber;

    private void Start()
    {
        score.text = $"";
        
    }

    void Update()
    {
        if (firstClick == -1 || firstClick == -2)
        {
            if (Input.GetMouseButton(0))
            {
                firstScreen.SetActive(false);
                firstText.text = $"";
                firstText2.text = $"";
                ShuffleLevels(levels);
                currentLevel = 0;
                if (firstClick == -1)
                {
                    CreateTraficLight();
                }
                LoadLevel();
                foreach (GameObject button in svetoforishe)
                {
                    button.SetActive(true);
                }
                points = 0;
                counter = 15;
                score.text = $"{points}";
                firstClick = 0;
            }
        }
        else
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
                ClearButtons();
                score.text = $"";
                traficLight.SetActive(false);
                firstScreen.SetActive(true);
                firstText.text = $"Ваш счёт: {points}";
                firstText2.text = $"Нажмите любую кнопку";
                firstClick = -2;
                return;
            }
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

            points = points + 15;
            if (points > 100)
            {
                points = 100;
            }

            score.text = $"{points}";

            currentLevel++;

            if (currentLevel >= levels.Count)
            {
                Debug.Log("Игра пройдена!");
                ClearButtons();
                score.text = $"";
                traficLight.SetActive(false);
                firstScreen.SetActive(true);
                firstText.text = $"Ваш счёт: {points}";
                firstText2.text = $"Нажмите любую кнопку";
                firstClick = -2;
                return;
            }

            LoadLevel();
        }
        else
        {
            if (points > 0)
            {
                points = points - 8;
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