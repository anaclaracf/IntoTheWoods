using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D: MonoBehaviour {
   
    public float speed = 10.0f;
    private Rigidbody2D rb;
    private Vector3 movingDirection;
    private Animator animator;
  

    public float playerJump;

    private void Start() {
       rb = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
    }

    private void Update() {
       HandleAnimation();
       HandleMovement();
       HandleJump();
    }
    // private void FixedUpdate() {
    //     RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 2);

    // }

    private void HandleMovement() {
        float hAxis = Input.GetAxis("Horizontal");
        // float vAxis = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(hAxis, 0).normalized;
        movingDirection = new Vector3(hAxis, 0).normalized; 
        rb.MovePosition(transform.position + movingDirection * speed * Time.deltaTime);

    }
    private void HandleJump() {
        float hAxis = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown (KeyCode.Space)){
            transform.position += new Vector3(hAxis, playerJump);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position, transform.right * 2);
    }  

    private void HandleAnimation() {
        if (movingDirection != Vector3.zero) {
           animator.SetBool("isMoving", true);
        }
        else {
           animator.SetBool("isMoving", false);
        }
    }

}