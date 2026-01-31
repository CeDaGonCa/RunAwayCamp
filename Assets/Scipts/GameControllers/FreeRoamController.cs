using System.Threading;
using UnityEngine;

public enum Turns { PlayerTurn, HunterTurn }
public class FreeRoamController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] HunterController hunterController;
    [SerializeField] Camera worldCamera;
    private CameraFlow cameraTarget;
    Turns currentTurn;
    private float timer;

    private void Start()
    {
        cameraTarget = worldCamera.GetComponent<CameraFlow>();
        cameraTarget.target = playerController.transform;
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

        if(timer > 10f)
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
        }
        else if (currentTurn == Turns.HunterTurn)
        {
            currentTurn = Turns.PlayerTurn;
            cameraTarget.target = playerController.transform;
        }
    }


}
