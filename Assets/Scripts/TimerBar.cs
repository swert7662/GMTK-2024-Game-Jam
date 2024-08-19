using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    public Slider timerSlider; // Assign this in the Inspector
    public float timeBetweenScales = 30f; // Time in seconds between each scaling
    public LevelScaler levelScaler; // Reference to the LevelScaler script

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        //timerSlider.value = timer / timeBetweenScales;

        if (timer >= timeBetweenScales)
        {
            levelScaler.ScaleLevel();
            timer = 0f; // Reset the timer after scaling
        }
    }
}
