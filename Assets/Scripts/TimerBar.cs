using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    [SerializeField] private Slider timerSlider; // Assign this in the Inspector
    [SerializeField] private float timeBetweenScales = 30f; // Time in seconds between each scaling
    [SerializeField] private LevelScaler levelScaler; // Reference to the LevelScaler script

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        timerSlider.value = timer / timeBetweenScales;

        if (timer >= timeBetweenScales)
        {
            levelScaler.ScaleLevel();
            timer = 0f; // Reset the timer after scaling
        }
    }
}
