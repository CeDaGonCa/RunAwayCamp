using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum hunterState {hunting, reaping};
public class HunterController : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask solidObjectsLayer;
    private bool isMoving;
    private Vector2 input;
    
    private Animator animator;
    private hunterState state;
    public event Action onPlayerDeath;
    
    private Vector3[] possibleDirections;

    [SerializeField] Transform playerPos;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] AudioClip DeathSound;
    public float circleRadius = 1.25f;
    
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        possibleDirections = new Vector3[] {Vector3.up,Vector3.down,Vector3.right,Vector3.left};
        
    }

    void Start()
    {
        
    }

    public void HandleUpdate()
    {
        switch (state)
        {
            case hunterState.hunting:
                {
                    hunt();
                    break;
                }
            case hunterState.reaping:
                {
                    reap();
                    break;
                }
        }
    }
    private void hunt()
    {
        if (!isMoving)
        {
            HashSet<Vector3> visitedPosition = new HashSet<Vector3>();
            float minDistance = float.MaxValue;
            // need to check if its walkable in future
            Vector3 direction = Vector3.zero;
            foreach (var availableDirection in possibleDirections){
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x,availableDirection.y,0f);
                //if(!isWalkable(newPosition) || visitedPosition.Contains(newPosition)) continue;
                float distance = manhattanDistance(this.playerPos.position,newPosition);
                
                if (distance < minDistance)
                {
                    minDistance = distance;
                    direction = availableDirection;
                }
            }
            
            animator.SetFloat("moveX",direction.x);
            animator.SetFloat("moveY",direction.y);
            StartCoroutine(Move(transform.position + direction));
            if(Physics2D.OverlapCircle(transform.position,circleRadius,playerLayer) != null)
            {
                Debug.Log("YOU ARE DEAD");
                state = hunterState.reaping;
            }
            visitedPosition.Add(transform.position + direction);
        }
        animator.SetBool("isMoving", isMoving);
    }
    private bool isWalkable(Vector3 targetPos)
    {
        
        if(Physics2D.OverlapCircle(targetPos, 0.2f,solidObjectsLayer) != null){
            return false;
        }
        return true;
    
    }
    
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, circleRadius);
    }
    */
    
    /*
    Haven't decided if death should always kill or only when they move
    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        Debug.Log("TRIGGERED");
        Debug.Log($"og layer = {collision.gameObject.layer}, Player layer = {playerLayer}");
        if(collision.gameObject.layer name == something)
        {
            state = hunterState.reaping;
            Debug.Log("YOU RE DEAD");
        }
    }*/
    private void reap()
    {
        SoundFXManager.instance.PlaySoundFXClip(DeathSound,transform,1f);
        onPlayerDeath();
    }
    private float manhattanDistance(Vector3 a, Vector3 b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    } 
    IEnumerator Move(Vector3 targetPos)
    {
        //Debug.Log($"Going to position X = {targetPos.x}, Y = {targetPos.y}");
        isMoving = true;
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position,targetPos, moveSpeed * Time.deltaTime);
            yield return null;
            
        }
        transform.position = targetPos;
        isMoving = false;
        
    }
    
}

