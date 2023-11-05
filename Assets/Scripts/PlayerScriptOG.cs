using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int speed;
    public int jumpForce;

    private Rigidbody2D rb;
    private Vector2 direction;

    public enum groundState {Grounded, Air};
    public groundState currentState = groundState.Air;

    private GameObject standingOn = null;

    [SerializeField] private Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump();
        }

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
        Debug.Log(currentState);
    }

    void getGrounded(){
        Collider2D groundCol = Physics2D.OverlapArea(new Vector2(groundCheck.position.x + 0.5f,groundCheck.position.y + 0.2f), new Vector2(groundCheck.position.x + -0.5f,groundCheck.position.y - 0.2f));

        if(groundCol != null)
        {
            standingOn = groundCol.gameObject;
            currentState = groundState.Grounded;
        }
        else
        {
            standingOn = null;
            currentState = groundState.Air;
        }
    }

    void jump(){
        getGrounded();
        Vector2 power = new Vector2(0,0);
        if(currentState == groundState.Grounded)
        {
            power = Vector2.up * jumpForce;
            Debug.Log(power);
            standingOn.GetComponent<Rigidbody2D>().AddForce(-power, ForceMode2D.Impulse); //Adds force to the object that it is standing on, for physical accuracy. Newton's 3rd Law ;)
        }
        rb.AddForce(power, ForceMode2D.Impulse); //Makes the jump
    }

}
