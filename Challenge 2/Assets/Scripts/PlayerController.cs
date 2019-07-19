using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public Text countText;
    public Text winText;
    public Text livesText;
    public float jumpForce;
    public GameObject stageOne;
    public GameObject stageTwo;

    private Rigidbody2D rb2d;
    private int count;
    private int lives;
    private bool facingRight;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        SetCountText ();
        lives = 3;
        SetLives ();
        winText.text = "";
        stageTwo.SetActive (false);
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        Walk ();
        Jump ();
        if (Input.GetKey("escape"))
            Application.Quit();
    }
    void FixedUpdate()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed); 

        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }  
        
        
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
           if(Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0 ,jumpForce), ForceMode2D.Impulse); 
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText ();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive (false);
            SetCountText ();
            lives = lives - 1;
            SetLives ();
            anim.SetInteger("State",2);
            anim.SetInteger("State",0);
        }
    }
    void SetLives ()
    {
        livesText.text = "Lives: " + lives.ToString ();
        if (lives <= 0)
        {
            winText.text = "You Lose";
            Destroy(gameObject);

        }
    }
    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString ();
        if (count >= 4)
        {
            stageOne.SetActive (false);
            stageTwo.SetActive (true);
            lives = 3;
        }
        if (count >= 8)
        {
            winText.text = "Winner Winner!";
        }
        
    }
    void Walk ()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetInteger("State",1);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetInteger("State",0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetInteger("State",1);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetInteger("State",0);
        }
    }
    void Jump ()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetInteger("State",2);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            anim.SetInteger("State",0);
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
}
}
