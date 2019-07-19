using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text livesText;
    public GameObject stageOneWall;
    public GameObject stageTwo;


    private Rigidbody rb;

    private int count;
    private int countTot;
    private int lives;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText ();
        lives = 5;
        SetLives ();
        winText.text = "";
        stageTwo.SetActive (false);
    }

     void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVerical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVerical);

        rb.AddForce (movement * speed);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            countTot = countTot + 1;
            SetCountText ();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText ();
            lives = lives - 1;
            SetLives ();
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
        if (countTot >= 4)
        {
            stageOneWall.SetActive (false);
            stageTwo.SetActive (true);
        }
        if (countTot >= 9)
        {
            winText.text = "Winner Winner!";
        }
        
    }
    void Update ()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }
}
        
   
