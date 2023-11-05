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

    protected bool playerFound = false;
    protected int moveDirection;
    protected float idleTime;
    protected float moveTime;
    protected float chaseTime;
    [SerializeField] protected float idleTimeMin;
    [SerializeField] protected float moveTimeMin;
    [SerializeField] protected float idleTimeMax;
    [SerializeField] protected float moveTimeMax;
    [SerializeField] protected float chaseTimeMax;

    public enum EnemyState {Idle, Moving, Chasing}
    protected EnemyState currentState = EnemyState.Idle;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerT = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        idleTime = idleTimeMax;
        moveTime = moveTimeMax;
        chaseTime = chaseTimeMax;
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
        if(playerT.position.x < transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

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

    public void TakeDamage(int damage){
            Instantiate(bloodEffect,transform.position,Quaternion.identity);
            health-=damage;
            Debug.Log("Damage Taken!");
        }
}
