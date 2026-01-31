using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public enum Turns { PlayerTurn, HunterTurn }
public class FreeRoamController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] HunterController hunterController;
    [SerializeField] Camera worldCamera;
    [SerializeField] Text turnText;
    [SerializeField] Image portrait;
    [SerializeField] Sprite playerSprite;
    [SerializeField] Sprite hunterSprite;
    
    
    private CameraFlow cameraTarget;
    
    Turns currentTurn;
    private float timer;
    public float timePerTurn = 10f;

    private void Start()
    {
        
        cameraTarget = worldCamera.GetComponent<CameraFlow>();
        cameraTarget.target = playerController.transform;
        portrait.sprite = playerSprite;
        turnText.text = "Your turn";
    }
    private void Update()
    {
        if (currentTurn == Turns.PlayerTurn)
        {
            playerController.HandleUpdate();
        }
        else if (currentTurn == Turns.HunterTurn)
        {
            hunterController.HandleUpdate();
        }
        timer += Time.deltaTime;

        if(timer > timePerTurn)
        {
            timer = 0f;
            changeTurn();
        }
    }
    private void changeTurn()
    {
        if (currentTurn == Turns.PlayerTurn)
        {
            currentTurn = Turns.HunterTurn;
            cameraTarget.target = hunterController.transform;
            portrait.sprite = hunterSprite;
            turnText.text = "Hunter's turn";
            portrait.color = Color.red;
        }
        else if (currentTurn == Turns.HunterTurn)
        {
            currentTurn = Turns.PlayerTurn;
            cameraTarget.target = playerController.transform;
            portrait.sprite = playerSprite;
            turnText.text = "Your turn";
        }
    }


}
