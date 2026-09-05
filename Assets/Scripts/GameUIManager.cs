using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public AudioSource backgroundMusic; 
    private float pitchIncrease = 1.25f;

    private Label timerLabel;
    private Label levelLabel;
    private Button pauseBtn;
    private Button restartBtn;
    private Label deathCounterLabel;
    private Label jumpCounterLabel;

    public GameObject ball; 
    private Vector3 startPosition;

    void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;
        startPosition = ball.transform.position;

        timerLabel = root.Q<Label>("timer");
        levelLabel = root.Q<Label>("level");
        pauseBtn = root.Q<Button>("PauseBtn");
        restartBtn = root.Q<Button>("RestartBtn");
        deathCounterLabel = root.Q<Label>("deathCount");
        jumpCounterLabel = root.Q<Label>("jumpCount");

        pauseBtn.text = GameManager.isPaused ? "Resume" : "Pause";
        restartBtn.text = "Restart";
        levelLabel.text = "Level " + GameManager.currentLevel;
        pauseBtn.clicked += Pause;
        restartBtn.clicked += Restart;
    }


    void Update()
    {
        Timer();
        CheckJump();
        CheckDeath();

    }

    void Timer(){
        timerLabel.Focus();
        if (GameManager.remainingTime > 0)
        {
            GameManager.remainingTime -= Time.deltaTime;
            int totalSeconds = Mathf.CeilToInt(GameManager.remainingTime);
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            timerLabel.text = string.Format("{0}:{1:00}", minutes, seconds);
            if (GameManager.remainingTime <= 10)
            {
                timerLabel.style.color = Color.red;
                backgroundMusic.pitch = pitchIncrease;
            }
            else
            {
                timerLabel.style.color = Color.black;
                backgroundMusic.pitch = 1f; 
            }
        }
    }

    void Pause(){
        Debug.Log("Pause clicked!");
        GameManager.isPaused = !GameManager.isPaused;
        pauseBtn.text = GameManager.isPaused ? "Resume" : "Pause";
        Time.timeScale = GameManager.isPaused ? 0 : 1;
        if (GameManager.isPaused)
        {
            backgroundMusic.Pause();
        }
        else
        {
            backgroundMusic.UnPause();
        }
    }

    void Restart(){
        Debug.Log("Restart clicked!");
        SceneManager.LoadScene("Scenes/Start");
    }

    void CheckJump(){
        jumpCounterLabel.text = "Jumps: " + GameManager.numberJumps.ToString();
    }
    void CheckDeath()
    {
        deathCounterLabel.text = "Deaths: " + GameManager.numberDeaths.ToString();
    }
}
