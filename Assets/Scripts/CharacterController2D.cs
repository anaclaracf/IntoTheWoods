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
    public float playerJump;
    public GameObject doorClose;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    GameManager gm;

    bool nextLevel = false;

    int jumpCount = 0;


    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        potion = GetComponent<GameObject>();
        gm = GameManager.GetInstance();
        if(gm.vidas == 2){
            Destroy(life1);
        }
        else if(gm.vidas == 1){
            Destroy(life1);
            Destroy(life2);
        }
        else if(gm.vidas == 0){
            // tela de morte
            print("oi");
        }
    }

    private void Update() {
       HandleAnimation();
       HandleMovement();
       HandleJump();
       Reset();
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

    private void Reset(){

        if(transform.position.y <= -5f){
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene("Level1");
            gm.pontos = 0;
            gm.minute = 2;
            gm.seconds = 0;
            gm.vidas--;
           
        }
    }

    void OnCollisionEnter2D(Collision2D collision){

         if (collision.gameObject.tag == "Potion"){
            Destroy(collision.gameObject);
            this.transform.localScale =  new Vector3(1.0f, 1.0f,1.0f);
        }
        if (collision.gameObject.tag == "Ground"){
            jumpCount = 0;
        }
        if (collision.gameObject.tag == "Key"){
            Destroy(collision.gameObject);
            doorClose.SetActive(false);
            nextLevel = true;
        }
        if (collision.gameObject.tag == "Door" && nextLevel){
            SceneManager.LoadScene("Level2");
        }
        if(collision.gameObject.tag=="Gems"){
            Destroy(collision.gameObject);
            gm.pontos++;
        }


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