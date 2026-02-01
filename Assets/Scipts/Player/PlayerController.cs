using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask solidObjectsLayer;
    private bool isMoving;
    private Vector2 input;
    private Animator animator;
    [SerializeField] GameObject flashLight;
    
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            
            if(input.x != 0) input.y = 0;

            if(input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
                flashLight.transform.rotation = Quaternion.Euler(0,0,-input.x * 90 + (input.y == -1? 180:0) );
                if(isWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
                
            }
        }

        animator.SetBool("isMoving",isMoving);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position,targetPos, moveSpeed * Time.deltaTime);
            yield return null;
            
        }
        transform.position = targetPos;
        isMoving = false;
    }
    private bool isWalkable(Vector3 targetPos)
    {
        
        if(Physics2D.OverlapCircle(targetPos, 0.2f,solidObjectsLayer) != null){
            return false;
        }
        return true;
    
    }
    public void stopAnimaiton()
    {
        animator.SetBool("isMoving",false);
    }
}
