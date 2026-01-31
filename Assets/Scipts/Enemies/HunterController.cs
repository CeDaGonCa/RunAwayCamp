using System;
using System.Collections;
using UnityEngine;

public class HunterController : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask solidObjectsLayer;
    private bool isMoving;
    private Vector2 input;
    
    private Animator animator;
    
    private Vector3[] possibleDirections;

    [SerializeField] Transform playerPos;
    
    
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
        
        
        if (!isMoving)
        {
            float minDistance = float.MaxValue;
            // need to check if its walkable in future
            Vector3 direction = Vector3.zero;
            foreach (var availableDirection in possibleDirections){
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x,availableDirection.y,0f);
                float distance = manhattanDistance(this.playerPos.position,newPosition);
                
                if (distance < minDistance)
                {
                    minDistance = distance;
                    direction = availableDirection;
                }
            }
            StartCoroutine(Move(transform.position + direction));
        }
        
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
