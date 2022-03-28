using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
public class CharacterLevel2 : MonoBehaviour
{

    public float speed = 10.0f;
    private Rigidbody2D rb;
    private Vector3 direction;
    private Animator animator;
    public float playerJump;
    public GameObject doorClose;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    GameManager gm;

    bool nextLevel = false;

    int jumpCount = 0;
    int facingLeft = 0;

    public AudioClip gem;
    public AudioClip drink;
    public AudioClip key;
    AudioSource audioSource;

    GameObject[] allGems;
    GameObject potion;
    GameObject keys;

    public GameObject canvas;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        potion = GetComponent<GameObject>();
        gm = GameManager.GetInstance();
        audioSource = GetComponent<AudioSource>();
        allGems = GameObject.FindGameObjectsWithTag("Gems");
        potion = GameObject.FindGameObjectWithTag("Potion");
        keys = GameObject.FindGameObjectWithTag("Key");
        gm.minute = 2;
        gm.seconds = 0;
        gm.ChangeState(GameManager.GameState.GAME);

    }

    private void Update()
    {

        // print(gm.gameState);
        HandleAnimation();
        HandleMovement();
        HandleJump();
        Reset();
        if (Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME)
        {
            gm.ChangeState(GameManager.GameState.PAUSE);
        }
        if (gm.vidas == 2)
        {
            life1.SetActive(false);
        }
        else if (gm.vidas == 1)
        {
            life1.SetActive(false);
            life2.SetActive(false);
        }
        else if (gm.vidas == 0 && gm.gameState == GameManager.GameState.GAME)
        {
            // print("aqui");
            gm.ChangeState(GameManager.GameState.ENDGAME);
            life1.SetActive(true);
            life2.SetActive(true);

        }
    }


    private void HandleMovement()
    {
        float hAxis = Input.GetAxis("Horizontal");
        direction = new Vector3(hAxis, 0).normalized;

        if (direction.x < 0 && facingLeft == 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            facingLeft = 1;
        }
        if (direction.x > 0 && facingLeft == 1)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            facingLeft = 0;
        }
        Vector3 vel = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = (direction * speed) + vel;
    }

    private void HandleJump()
    {
        float hAxis = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount < 1)
            {
                rb.AddForce(Vector2.up * playerJump, ForceMode2D.Impulse);
                jumpCount++;
            }
        }
    }


    private void Reset()
    {

        if (transform.position.y <= -19f)
        {

            if (gm.vidas > 0)
            {
                this.transform.position = new Vector3(-2.55f, -1.17f, 0f);
                playerJump = 7;
                foreach (GameObject g in allGems)
                {
                    g.SetActive(true);
                }
                keys.SetActive(true);
                potion.SetActive(true);
                doorClose.SetActive(true);
                nextLevel = false;

            }

            gm.pontos = 0;
            gm.minute = 2;
            gm.seconds = 0;
            gm.vidas--;


        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Potion")
        {
            collision.gameObject.SetActive(false);
            audioSource.PlayOneShot(drink, 0.2f);
            playerJump = 12;
        }
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = 0;
        }
        if (collision.gameObject.tag == "Key")
        {
            collision.gameObject.SetActive(false);
            doorClose.SetActive(false);
            audioSource.PlayOneShot(key, 0.2f);
            nextLevel = true;
        }
        if (collision.gameObject.tag == "Door" && nextLevel)
        {
            DontDestroyOnLoad(canvas);
            SceneManager.LoadScene("Level3");
        }
        if (collision.gameObject.tag == "Gems")
        {
            collision.gameObject.SetActive(false);
            audioSource.PlayOneShot(gem, 0.1f);
            gm.pontos++;
        }


    }

    private void HandleAnimation()
    {

        if (direction != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

}