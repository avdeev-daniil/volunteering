using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonParent;
    public List<GameObject> backgrounds = new List<GameObject>();
    private int currentLevel = 0;
    private int firstClick = -1;
    public List<GameObject> svetoforishe;
    private float timer_1 = 0;
    public GameObject firstScreen;
    public TMP_Text firstText;
    public TMP_Text firstText2;
    public Image firstFirstText;
    public Image chel2;
    public Button startedButton;
    public GameObject tablo;
    public GameObject graffic;
    public GameObject square;
    public GameObject square2;
    public GameObject you;
    public GameObject svet20;
    private float timer2 = 0f;
    public PlaySound4 sounds;
    private int gamestop = 0;
    public MoveToTarget chel;
    private List<GameObject> spawnedButtons =
        new List<GameObject>();

    public List<LevelsScript> levels = new List<LevelsScript>();

    public GameObject traficLight;
    public GameObject greenTraficLight;
    public GameObject yellowTraficLight;
    public TMP_Text score;
    public int points = 0;

    public TMP_Text time;
    public int counter = 30;

    private Vector2[][] levelPositions =
    {
        new Vector2[]
        {
            new Vector2(-373, 62),
            new Vector2(372, 62)
        },

        new Vector2[]
        {
            new Vector2(-378, 70),
            new Vector2(6, 70),
            new Vector2(378, 70)
        },

        new Vector2[]
        {
            new Vector2(-372, 72),
            new Vector2(-137, 72),
            new Vector2(150, 72),
            new Vector2(384, 72)
        },

        new Vector2[]
        {
            new Vector2(-183, 149),
            new Vector2(197, 149),
            new Vector2(-372, 61),
            new Vector2(2, 61),
            new Vector2(378, 61)
        }
    };

    private string maxNumber;

    private void Start()
    {
        score.text = $"";
        
    }

    void Update()
    {
        if (firstClick > -1)
        {
            timer_1 += Time.deltaTime;
            if (timer_1 > 1)
            {
                counter = counter - 1;
                timer_1 = 0f;
            }
            //time.text = $"Время: {counter}";
            if (counter % 6 == 0 && counter != 30){
                svetoforishe[counter / 6].SetActive(false);
                sounds.PlaySFX(5);
            }
            if (counter == 0)
            {
                Debug.Log("Игра пройдена!");
                ClearButtons();
                score.text = $"";
                traficLight.SetActive(false);
                svet20.SetActive(false);
                // tablo.SetActive(false);
                graffic.SetActive(false);
                square2.SetActive(false);
                firstScreen.SetActive(true);
                you.SetActive(true);
                firstText.text = $"Твой счёт:";
                firstText2.text = $"{points}";  
                firstClick = -2;
                GameObject currentBH = GameObject.FindWithTag("Background");
                Destroy(currentBH);
                return;
            }
        }
    }

    void LoadLevel()
    {
        if (gamestop == 0)
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
            var b = Instantiate(backgrounds[numbers.Count - 2], newpos, Quaternion.identity, buttonParent.parent);
            b.transform.SetSiblingIndex(0);

            b.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            b.GetComponent<RectTransform>().offsetMax = Vector2.zero;


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
    }

    public IEnumerator ReceiveNumber(string number, float x, float y)
    {
        if (gamestop == 0)
        {
            if (number == maxNumber)
            {
                Debug.Log("Правильно!");

                points = points + 10;
                gamestop = 1;
                chel.MoveTo(new Vector2(x, y));

                yield return new WaitForSeconds(0.3f);
                gamestop = 0;
                score.text = $"{points}";
                sounds.PlaySFX(1);

                currentLevel++;

                if (currentLevel >= levels.Count)
                {
                    Debug.Log("Игра пройдена!");
                    ClearButtons();
                    score.text = $"";
                    traficLight.SetActive(false);
                    // tablo.SetActive(false);
                    graffic.SetActive(false);
                    svet20.SetActive(false);
                    square2.SetActive(false);
                    firstScreen.SetActive(true);
                    you.SetActive(true);
                    firstText.text = $"Твой счёт:";
                    firstText2.text = $"{points}";  
                    firstClick = -2;
                    GameObject currentBH = GameObject.FindWithTag("Background");
                    Destroy(currentBH);
                    yield break;
                }

                LoadLevel();
            }
            else
            {
                if (points > 8)
                {
                    points = points - 8;
                }

                score.text = $"{points}";
                Debug.Log("Неправильно!");
                sounds.PlaySFX(0);
            }
        }
    }

    public void ReceiveSignal()
    {
        gamestop = 0;
        firstScreen.SetActive(false);
        firstText.text = $"";
        square.SetActive(false);
        firstText2.text = $"";
        ShuffleLevels(levels);
        you.SetActive(false);
        svet20.SetActive(true);
        currentLevel = 0;
        firstClick = 0;
        LoadLevel();
        foreach (GameObject button in svetoforishe)
        {
            button.SetActive(true);
        }
        points = 0;
        counter = 30;
        square2.SetActive(true);
        // tablo.SetActive(true);
        graffic.SetActive(true);
        score.text = $"{points}";
        timer2 = 0f;
        Debug.Log(firstClick);
    }

    public void TryAgain()
    {
        gamestop = 0;
        firstScreen.SetActive(false);
        you.SetActive(false);
        square.SetActive(true);
        gamestop = 0;
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
            Vector3 newpos = new Vector3(4.7f + i * 0.9f , -4.5f, 0);
            if (i >= 1 && i < 3)
            {
                GameObject jeden = Instantiate(yellowTraficLight, newpos, Quaternion.identity);
                svetoforishe.Add(jeden);
            }
            else if (i > 2)
            {
                GameObject jeden = Instantiate(greenTraficLight, newpos, Quaternion.identity);
                svetoforishe.Add(jeden);
            }
            else{
                GameObject jeden = Instantiate(traficLight, newpos, Quaternion.identity);
                svetoforishe.Add(jeden);
            }
        }
    }
}