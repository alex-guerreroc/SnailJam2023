using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cebolla : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Debug.Log(currentState);
    }

    protected override void Idle()
    {
        base.Idle();
        if(playerFound)
        {
            currentState = EnemyState.Charging;
            chargeTime = chargeTimeMax;
        }
    }

    protected override void Charging()
    {
        base.Charging();
        if(playerFound)
        {
            chargeTime -= Time.deltaTime;
        }
        if(chargeTime <= 0)
        {
            currentState = EnemyState.Chasing;
        }

        if(playerT.position.x < transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    protected override void Chasing()
    {
        //Collider2D leftFloor = Physics2D.OverlapCircle(transform.position + new Vector3(-0.5f, -0.6f,0),0.01f);
        //Collider2D rightFloor = Physics2D.OverlapCircle(transform.position + new Vector3(0.5f, -0.6f,0),0.01f);
        base.Chasing();
        if(rb.velocity.x == 0)
        {
            if(playerT.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(-1*speed, rb.velocity.y);
                sr.flipX = true;
            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                sr.flipX = false;
            }
        }

        if(chaseTime <= 0f)
        {
            rb.velocity = new Vector2(0,0);
        }
        
        
        /*
        if((rb.velocity.x > 0 && rightFloor != null) || (rb.velocity.x < 0 && leftFloor != null))
        {
            rb.velocity = new Vector2(-1*rb.velocity.x, rb.velocity.y);
        }*/
    }
}
