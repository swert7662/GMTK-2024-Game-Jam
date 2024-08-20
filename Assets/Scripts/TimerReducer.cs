using UnityEngine;

public class TimerReducer : MonoBehaviour
{
    [SerializeField] private TimerBar timerBar; // Reference to the TimerBar script
    [SerializeField] private float timeReductionAmount = 5f; // Amount of time to reduce from the timer

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided is the player
        if (other.CompareTag("Player"))
        {
            // Call the ReduceTimer method from the TimerBar script
            timerBar.ReduceTimer(timeReductionAmount);

            // Optionally, destroy the object after the collision
            Destroy(gameObject);
        }
    }
}
