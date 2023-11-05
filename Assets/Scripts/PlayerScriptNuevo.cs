using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptNuevo : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce;
    public float speed;
    private float moveInput;
    public Transform feetPos;
    public float checkRadius;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;


    private float coyoteTime=0.12f;
    private float coyoteTimeCounter;

    private float jumpBufferTime=0.08f;
    private float jumpBufferCounter;
    public LayerMask whatIsGround;

    private bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }


void Update()
    {
        isGrounded=Physics2D.OverlapCircle(feetPos.position,checkRadius,whatIsGround);
        Debug.Log(isGrounded);
        if(isGrounded==true){
            coyoteTimeCounter=coyoteTime;
        }
        else{
            coyoteTimeCounter-=Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            jumpBufferCounter=jumpBufferTime;
        }

        if(moveInput>0){
            transform.eulerAngles=new Vector3(0,0,0);
        } else if(moveInput<0){
            transform.eulerAngles=new Vector3(0,180,0);
        }

        if(coyoteTimeCounter>0f && jumpBufferCounter>0f){
            isJumping=true;
            jumpTimeCounter=jumpTime;
            rb.velocity=Vector2.up*jumpForce;
            jumpBufferCounter=0;
        } else{
            jumpBufferCounter-=Time.deltaTime;
        }


        if(Input.GetKey(KeyCode.Space) && isJumping==true){

            if(jumpTimeCounter>0){
                rb.velocity=Vector2.up*jumpForce;
                jumpTimeCounter-=Time.deltaTime;

            }
            else{
                isJumping=false;
            }

            
        
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            isJumping=false;
            coyoteTimeCounter=0f;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        moveInput=Input.GetAxisRaw("Horizontal");
        rb.velocity=new Vector2(moveInput*speed,rb.velocity.y);


    }
}
