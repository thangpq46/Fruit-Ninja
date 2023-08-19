using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    public bool isGameActive = false;
    private int score;
    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI textGameOver;
    public Button restartButton;
    public GameObject titleScreen;
    public TextMeshProUGUI textLives;
    private int lives = 3;
    private float volume = 1f;
    public Slider volumeSlider;
    private AudioSource backgroundMusic;
    public GameObject pausePanel;
    private bool ispaused = false;
    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = GetComponent<AudioSource>();
        volumeSlider.onValueChanged.AddListener(delegate
        {
            volume = volumeSlider.value;
            backgroundMusic.volume = volume;
            Debug.Log("Volume: " + volume);
        });
    }

    private void changePause()
    {
        ispaused = !ispaused;
        if (ispaused)
        {
            Time.timeScale = 0;
            pausePanel.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && isGameActive)
        {
            changePause();
        }
    }

    public void updateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        textMeshPro.text = "Score: " + score;
    }
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }

    }

    public void diseaseLive()
    {
        if (isGameActive && lives > 0)
        {
            lives--;
            textLives.text = "Lives: " + lives;
        }
        if (lives == 0)
        {
            GameOver();

        }
    }
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        textGameOver.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void startGame(int difficulty)
    {
        textMeshPro.gameObject.SetActive(true);
        textLives.gameObject.SetActive(true);
        textLives.text = "Lives: " + lives;
        isGameActive = true;
        spawnRate /= difficulty;
        titleScreen.gameObject.SetActive(false);
        StartCoroutine(SpawnTarget());
        score = 0;
        updateScore(0);
    }
}
