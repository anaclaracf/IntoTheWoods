using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D: MonoBehaviour {
   
    public float speed = 10.0f;
    private Rigidbody2D rb;
    private Vector3 direction;
    private Animator animator;
    public GameObject potion;
    // private Scene scene;
    public float playerJump;

    int jumpCount = 0;

    private void Start() {
       rb = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
       potion = GetComponent<GameObject>();
    //    scene = GetComponent<>();
    }

    private void Update() {
       HandleAnimation();
       HandleMovement();
       HandleJump();
       Dead();
    }


    private void HandleMovement() {
        float hAxis = Input.GetAxis("Horizontal");
        direction = new Vector3(hAxis, 0).normalized;
        Vector3 vel = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = (direction * speed) + vel;
    }

    private void HandleJump() {
        float hAxis = Input.GetAxis("Horizontal");
        
        if(Input.GetKeyDown (KeyCode.Space)){
            if(jumpCount < 1){
                rb.AddForce(Vector2.up*playerJump, ForceMode2D.Impulse);
                jumpCount++;
            }
        }
    }

    private void Dead(){

        if(transform.position.y <= -5f){
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene("Level1");
        }
    }

    void OnCollisionEnter2D(Collision2D collision){

         if (collision.gameObject.tag == "Potion"){
            Destroy(collision.gameObject);
            this.transform.localScale =  new Vector3(1.0f, 1.0f,1.0f);
            // animator.SetBool("isMini", true);
            // Debug.Log("Do something here");
        }
        // animator.SetBool("isMini", false);
        if (collision.gameObject.tag == "Ground"){
            jumpCount = 0;
        }
        if (collision.gameObject.tag == "Key"){
            Destroy(collision.gameObject);
            // doorClose.GetComponent<Renderer>().enabled = false;
            // doorClose.SetActive(false);
            GameObject.Find("doorClosed").GetComponent<GameObject>().SetActive(false);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position, transform.right * 2);
    }  

    private void HandleAnimation() {
        
        if (direction != Vector3.zero) {
           animator.SetBool("isMoving", true);
        }
        else {
           animator.SetBool("isMoving", false);
        }
    }

}