using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int speed;
    public int playerDetectRadius;

    public GameObject bloodEffect;

    protected Transform playerT;
    protected Animator anim;
    protected SpriteRenderer sr;
    protected Rigidbody2D rb;

    protected bool playerFound = false;
    protected int moveDirection;
    protected float idleTime;
    protected float moveTime;
    protected float chaseTime;
    protected float chargeTime;
    [SerializeField] protected float idleTimeMin;
    [SerializeField] protected float moveTimeMin;
    [SerializeField] protected float idleTimeMax;
    [SerializeField] protected float moveTimeMax;
    [SerializeField] protected float chaseTimeMax;
    [SerializeField] protected float chargeTimeMax;

    public enum EnemyState {Idle, Moving, Chasing, Charging}
    protected EnemyState currentState = EnemyState.Idle;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerT = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        idleTime = idleTimeMax;
        moveTime = moveTimeMax;
        chaseTime = chaseTimeMax;
        chargeTime = chargeTimeMax;
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        if (health<=0){
            Destroy(gameObject);
        }




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
            case EnemyState.Charging:
                Charging();
                break;
        }
        playerFound = false;
    }

    protected virtual void Idle()
    {
        if(playerFound)
        {
            currentState = EnemyState.Chasing;
            chaseTime = chaseTimeMax;
            anim.SetTrigger("ToMove");
        }
        idleTime -= Time.deltaTime;
    }

    protected virtual void Moving()
    {
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

    protected virtual void Chasing()
    {
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

    protected virtual void Charging()
    {
        chargeTime -= Time.deltaTime;
    }

    public void TakeDamage(int damage){
            Instantiate(bloodEffect,transform.position,Quaternion.identity);
            health-=damage;
            Debug.Log("Damage Taken!");
        }
}
