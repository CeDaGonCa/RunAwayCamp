using UnityEngine;
using TMPro;
using DG.Tweening;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float speed = 25f;
    float elapsedTime;
    private bool isTimerRunning = true;
    void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("Time survived: {0:00}:{1:00}",minutes, seconds);
        }else
        {   
            
            //this.transform.position = Vector3.MoveTowards(this.transform.position, destination.transform.position,speed);
        }

    }
    public float stopTimer()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("Congrats! You survived for: {0:00}:{1:00}",minutes, seconds);
        isTimerRunning = false;
        return elapsedTime;
        
    }
}
