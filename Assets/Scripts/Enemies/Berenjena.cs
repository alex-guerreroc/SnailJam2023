using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Berenjena : MonoBehaviour
{
    public int hp;
    public int speed;
    public int playerDetectRadius;

    private Transform playerT;
    private Animator anim;
    private SpriteRenderer sr;

    private bool playerFound = false;
    private int moveDirection;
    private float idleTime;
    private float moveTime;
    private float chaseTime;
    [SerializeField] private float idleTimeMin;
    [SerializeField] private float moveTimeMin;
    [SerializeField] private float idleTimeMax;
    [SerializeField] private float moveTimeMax;
    [SerializeField] private float chaseTimeMax;

    public enum EnemyState {Idle, Moving, Chasing}
    EnemyState currentState = EnemyState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        playerT = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        idleTime = idleTimeMax;
        moveTime = moveTimeMax;
        chaseTime = chaseTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] nearbyCol = Physics2D.OverlapCircleAll(transform.position, playerDetectRadius);
        for(int i = 0; i < nearbyCol.Length; i++)
        {
            if(nearbyCol[i] == playerT.GetComponent<Collider2D>())
            {
                playerFound = true;
            }
        }
        switch(currentState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Moving:
                Moving();
                break;
            case EnemyState.Chasing:
                Chasing();
                break;
        }
        playerFound = false;
    }

    public void Idle()
    {
        if(playerFound)
        {
            currentState = EnemyState.Chasing;
            chaseTime = chaseTimeMax;
            anim.SetTrigger("ToMove");
        }
        idleTime -= Time.deltaTime;
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

    public void Moving()
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
        
        if(playerFound)
        {
            currentState = EnemyState.Chasing;
            chaseTime = chaseTimeMax;
        }
        moveTime -= Time.deltaTime;
        if(moveTime <= 0f)
        {
            currentState = EnemyState.Idle;
            idleTime = Random.Range(idleTimeMin, idleTimeMax);
            anim.SetTrigger("ToStopMove");
        }
    }

    public void Chasing()
    {
        if(playerT.position.x < transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, playerT.position, speed*Time.deltaTime);
        if(!playerFound)
        {
            chaseTime -= Time.deltaTime;
        }
        else
        {
            chaseTime = chaseTimeMax;
        }

        if(chaseTime <= 0f)
        {
            currentState = EnemyState.Idle;
            idleTime = Random.Range(idleTimeMin, idleTimeMax);
            moveTime = Random.Range(moveTimeMin, moveTimeMax);
            anim.SetTrigger("ToStopMove");
        }
    }
}
