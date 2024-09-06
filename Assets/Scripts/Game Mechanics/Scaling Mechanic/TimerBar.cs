using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    [SerializeField] private float timeBetweenScales = 30f;
    [SerializeField] private const int maxShrinks = 3;
    //[SerializeField] private GameObject gameOverScreen;

    private FPSMovement playerMovement;
    private LevelScaler levelScaler;
    private Slider timerSlider;
    private float timer = 0f;
    private int shrinkCount = 0; // Keep track of how many times the player has shrunk

    private void Start()
    {
        timerSlider = GameObject.FindGameObjectWithTag("TimerBar").GetComponent<Slider>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<FPSMovement>();
        levelScaler = GetComponent<LevelScaler>();
    }
    void Update()
    {
        timer += Time.deltaTime;

        timerSlider.value = timer / timeBetweenScales;

        if (timer >= timeBetweenScales)
        {
            ShrinkPlayer();
            timer = 0f;
        }
    }

    private void ShrinkPlayer()
    {
        if (shrinkCount >= maxShrinks)
        {
            GameOver();
        }
        else
        {
            shrinkCount++;
            levelScaler.ScalePlayer();
            PlayShrinkSound(); // Play the shrink sound
        }
    }

    public void ReduceTimer(float amount)
    {
        if (timer - amount >= 0)
        {
            timer -= amount;
        }
        else
        {
            timer = 0;
        }
    }

    private void GameOver()
    {
        Debug.LogError("Game Over!");
        //enabled = false;

        //playerMovement.enabled = false;

        //gameOverScreen.SetActive(true);

        // Optionally, you could freeze time or add other effects here
    }

    // Method to play a random shrink sound
    private void PlayShrinkSound()
    {
        AudioManager.Instance.PlaySFX("Misc", "Shrink");
    }
}
