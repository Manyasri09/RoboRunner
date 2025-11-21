using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;        
    public TextMeshProUGUI gameOverScoreText; 
    public TextMeshProUGUI highScoreText;     
    public GameObject gameOverPanel;          

    private int score = 0;
    private int highScore = 0;
    private bool isGameOver = false;

    void Awake()
    {
        
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Start()
    {
        UpdateLiveScoreUI();
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void AddScore(int value)
    {
        if (isGameOver) return;

        score += value;
        UpdateLiveScoreUI();
    }

    private void UpdateLiveScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        isGameOver = true;

        
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            if (gameOverScoreText != null)
                gameOverScoreText.text = "Score: " + score.ToString();

            if (highScoreText != null)
                highScoreText.text = "High Score: " + highScore.ToString();
        }

     
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
