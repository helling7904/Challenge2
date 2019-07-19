using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    Transform tran;
    Animator anim;
    private Rigidbody2D rb2d;
    private int count;
    public float speed;
    public Text countText;
    public Text winText;
    void Start ()
    {
        tran = GetComponent<Transform> ();
        rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
        count = 0;
        winText.text = "";
        SetCountText ();
    }
    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        Vector2 movement = new Vector2 (moveHorizontal,moveVertical);
        rb2d.AddForce (movement * speed);
        

    }
    void OnTriggerEnter2D(Collider2D other) 
	{
        if (other.gameObject.CompareTag ("PickUp")) 
		{
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText ();
		}
	}
    void SetCountText()
	{
        countText.text = "Count: " + count.ToString ();
        if (count >= 12)
            winText.text = "You win!";
    }
    void Update ()
    {
        Walk ();
        if (Input.GetKey("escape"))
            Application.Quit();
    }
    void Walk ()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FlipLeft();
            anim.SetInteger("State",1);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            FlipRight();
            anim.SetInteger("State",0);
        }
    }
    void FlipLeft()
    {
        tran.localScale -= new Vector3(0.6F, 0, 0);
    }
    void FlipRight()
    {
        tran.localScale += new Vector3(0.6F, 0, 0);
    }

}
