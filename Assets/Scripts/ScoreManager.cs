using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;        // top-left live score
    public TextMeshProUGUI gameOverScoreText; // shown on GameOver panel
    public TextMeshProUGUI highScoreText;     // shown on GameOver panel
    public GameObject gameOverPanel;          // your GameOver UI panel

    private int score = 0;
    private int highScore = 0;
    private bool isGameOver = false;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Load the high score from PlayerPrefs
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

        // Update high score if needed
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        // Show Game Over UI
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            if (gameOverScoreText != null)
                gameOverScoreText.text = "Score: " + score.ToString();

            if (highScoreText != null)
                highScoreText.text = "High Score: " + highScore.ToString();
        }

        // Pause game time (optional)
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
