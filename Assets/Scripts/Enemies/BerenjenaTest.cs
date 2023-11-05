using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerenjenaTest : Enemy
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
        if(idleTime <= 0f)
        {
            currentState = EnemyState.Moving;
            moveTime = Random.Range(moveTimeMin, moveTimeMax);
            int rand = Random.Range(0,2);
            if(rand == 0)
            {
                moveDirection = -1;
            }
            else if(rand == 1)
            {
                moveDirection = 1;
            }
            
            anim.SetTrigger("ToMove");
        }
    }

    protected override void Moving()
    {
        
        if(moveDirection < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveDirection*10000,transform.position.y), Time.deltaTime*speed/3);
        
        base.Moving();
    }

    protected override void Chasing()
    {
        base.Chasing();
        transform.position = Vector2.MoveTowards(transform.position, playerT.position, speed*Time.deltaTime);
    }
}
