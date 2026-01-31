using System.Collections;
using UnityEngine;

public class HunterController : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask solidObjectsLayer;
    private bool isMoving;
    private Vector2 input;
    
    private Animator animator;

    [SerializeField] Transform playerPos;
    
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    public void HandleUpdate()
    {
        Vector3 targetPos = (playerPos.position - transform.position);
        StartCoroutine(Move(targetPos));

    }
    
    IEnumerator Move(Vector3 targetPos)
    {
        
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon*2)
        {
            transform.position = Vector3.MoveTowards(transform.position,targetPos, moveSpeed * Time.deltaTime);
            yield return null;
            
        }
        transform.position = targetPos;
        
    }
    
}
