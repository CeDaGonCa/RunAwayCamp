using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Turns { PlayerTurn, HunterTurn, EndOfGame }
public class FreeRoamController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] HunterController hunterController;
    
    [SerializeField] Text turnText;
    [SerializeField] Image portrait;
    [SerializeField] Sprite playerToken;
    [SerializeField] Sprite hunterToken;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Timer gameTimer;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] EventDisplay eventText;
    
    Turns currentTurn;
    private List<HunterController> hunters;
    private float timer;
    public float timePerTurn;
    private float hunterTime = 3f;
    private float playerTime = 15f;

    private void Awake()
    {
        hunters = new List<HunterController>();
        
    }
    private void Start()
    {
        timePerTurn = playerTime;
        portrait.sprite = playerToken;
        turnText.text = "Your turn";
        hunterController.onPlayerDeath += playerDeath;
        hunters.Add(hunterController);
    }
    private void Update()
    {
        if (currentTurn == Turns.PlayerTurn)
        {
            playerController.HandleUpdate();
        }
        else if (currentTurn == Turns.HunterTurn)
        {
            foreach (var hunter in hunters)
            {
                hunter.HandleUpdate();
            }
        }
        timer += Time.deltaTime;

        if(timer > timePerTurn)
        {
            timer = 0f;
            changeTurn();
        }
    }
    private void makeNewHunter()
    {
        HunterController tmp = Instantiate(hunterController, this.transform);
        tmp.transform.position = new Vector3(-1.5f,0.8f,0);
        hunters.Add(tmp);
        tmp.onPlayerDeath += playerDeath;
        eventText.DisplayEvent("A new hunter has arrived");
    }
    private void increaseHunterTurnTime()
    {
        hunterTime += 0.5f;
        eventText.DisplayEvent("Hunters chase for longer");
    }
    private void changeTurn()
    {
        if (currentTurn == Turns.PlayerTurn)
        {
            currentTurn = Turns.HunterTurn;
            portrait.sprite = hunterToken;
            turnText.text = "Hunter's turn";
            playerController.stopAnimaiton();
            timePerTurn = hunterTime;
            
            
        }
        else if (currentTurn == Turns.HunterTurn)
        {
            currentTurn = Turns.PlayerTurn;
            portrait.sprite = playerToken;
            turnText.text = "Your turn";
            timePerTurn = playerTime;
            if(UnityEngine.Random.value * 100 <= 34f)
            {
                int function = Random.Range(0,1);
                if(function == 0)
                {
                    makeNewHunter();
                }
                else if (function == 1)
                {
                    increaseHunterTurnTime();
                }
            }
        }
    }
    void playerDeath()
    {
        // PlaySomeAnimation;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gameOverScreen.SetActive(true);
        portrait.enabled = false;
        turnText.enabled = false;
        currentTurn = Turns.EndOfGame;
        float finalElapsedTime = gameTimer.stopTimer();
        int minutes = Mathf.FloorToInt(finalElapsedTime / 60);
        int seconds = Mathf.FloorToInt(finalElapsedTime % 60);
        timerText.text = string.Format("Congrats! You survived for: {0:00}:{1:00}",minutes, seconds);

    }

}
