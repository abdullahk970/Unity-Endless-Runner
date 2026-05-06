using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;

    private float score;
    public bool isGameOver = false;

    // NEW VARIABLE
    public float gameSpeed = 6f;

    // ✅ Added variables (coins system)
    public TextMeshProUGUI coinText;
    public int coins = 0;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (isGameOver) return;

        // Score system
        score += Time.deltaTime * 10;
        scoreText.text = "Score: " + Mathf.FloorToInt(score);

        // Speed increase system
        gameSpeed += Time.deltaTime * 0.3f;
    }

    // ✅ Added function (as per your requirement)
    public void AddScore(int amount)
    {
        score += amount;
    }

    // ✅ Added function (coin system)
    public void AddCoin()
    {
        coins++;
        coinText.text = "Coins: " + coins;
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER CALLED");

        isGameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // game freeze
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // important reset
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}