using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaElote : Enemy
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
    }
}
