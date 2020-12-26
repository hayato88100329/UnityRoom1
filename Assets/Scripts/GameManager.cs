#define DEBUG_KEY

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField]
    bool gameoverEnabled = default;
    [SerializeField]
    TextMeshProUGUI scoreText = default;
    [SerializeField]
    TextMeshProUGUI timeText = default;
    [SerializeField]
    TextMeshProUGUI highScoreText = default;
    [SerializeField]
    int point = 100;





    static int score;
    static float time;
    static int highScore = 100;
    const float StartTime = 10f;
    const int ScoreMax = 99999;
    static bool gameover;
    static bool clear;
    static bool gameStarted;

    public static bool CanChangeToTitle { get; private set; }

    public static float GetTime
    {
        get
        {
            return Mathf.Round(time * 10f) / 10f;
        }
    }
    private void Awake()
    {
        Instance = this;
        gameover = false;
        clear = false;
        gameStarted = false;
        highScore = PlayerPrefs.GetInt("HighScore", highScore);
        Item.ClearCount();
    }

    // Start is called before the first frame update
    void Start()
    {
        TinyAudio.PlaySe(TinyAudio.Se.Click);
        UpdateScoreText();
        UpdateTimeText();
        UpdateHighScoreText();
    }

    // Update is called once per frame  

    void FixedUpdate()
    {
        if (gameover || !gameoverEnabled) return;

        time -= Time.fixedDeltaTime;
        if (time <= 0)
        {
            time = 0;
            ToGameover();
        }
        UpdateTimeText();



#if DEBUG_KEY
        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene("Gameover", LoadSceneMode.Additive);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("Clear", LoadSceneMode.Additive);
        }
#endif

    }
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{score:00000}";
        }
    }


    void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = $"{highScore:00000}";
        }
    }

    public void AddPoint(int point)
    {
        score += point;

        //上限チェックその１
        if (score > ScoreMax)
        {
            score = ScoreMax;
        }
        /*上限チェックその2
        score = score < ScoreMax ? score : ScoreMax;
        上限チェックその3(関数型)
        score = Mathf.Min(score, ScoreMax);*/


        UpdateTimeText();
    }
    void UpdateTimeText()
    {
        if (timeText != null)
        {
            timeText.text = $"{ time:0.0}";
        }

    }

    public static void ToGameover()
    {
        SceneManager.LoadScene("Gameover", LoadSceneMode.Additive);
        CheckHighScore();
        gameover = true;
    }

    public static void ToClear()
    {
        if (clear || gameover) return;

        SceneManager.LoadScene("Clear", LoadSceneMode.Additive);
        CheckHighScore();
        clear = true;
    }

    public void StartGame()
    {
        if (gameStarted) return;

        SceneManager.LoadScene("Game");
        gameStarted = true;
    }

    public static void CheckHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            Instance.StartCoroutine(RankingProc());
        }
    }
    const float RankingShowWait = 2f;

    static IEnumerator RankingProc()
    {
        CanChangeToTitle = false;
        yield return new WaitForSeconds(RankingShowWait);
        //naichilab.RankingLoader.Instance.SendScoreAndShowRanking(highScore);
        CanChangeToTitle = true;

    }

}
