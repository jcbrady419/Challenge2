using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    Animator anim;
    public float speed;
    public Text score;
    public Text lives;
    public Text loseText;
    public Text winText;
    private int scoreValue = 0;
    private int livesValue = 03;
    private bool facingRight = true;
    private bool isJumping = true;



    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives.text = livesValue.ToString();
        loseText.text = "";
        winText.text = "";
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        anim = GetComponent<Animator>();
    }
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    // Update is called once per frame
    void Update()
    
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement ));
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
            else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (isJumping == false && vertMovement == 0)
        {
            
            anim.SetBool("isJumping", false);
        }
        if (isJumping == true && vertMovement > 0)
        {
            
            anim.SetBool("isJumping", true);
        }
        
        

        if (Input.GetKeyDown(KeyCode.D))

        {
          anim.SetInteger("State", 1);
         }
         if (Input.GetKeyUp(KeyCode.D))
    {
          anim.SetInteger("State", 0);
         }
         if (Input.GetKeyDown(KeyCode.A))

        {
          anim.SetInteger("State", 1);
         }
         if (Input.GetKeyUp(KeyCode.A))
    {
          anim.SetInteger("State", 0);
         }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4) 
        {
            livesValue = 3;
            lives.text = livesValue.ToString();
            transform.position = new Vector2(69f, 0); 
        }
        if (scoreValue == 8)
        {
            winText.text = "You Win! Game made by Jacob Brady!";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        
        
        if (livesValue == 0)
        {
            loseText.text= "Game Over! You Lose!";
            Destroy(this);
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.collider.tag == "Ground")
        {
            isJumping = false;
            if (Input.GetKey(KeyCode.W))
            {
                isJumping = true;
                rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            }
        }
    }
    
}