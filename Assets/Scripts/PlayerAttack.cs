using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{


    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public AudioSource audioPlayer;
    public int damage;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack<=0){
            if(Input.GetMouseButtonDown(1)){
                anim.SetTrigger("ShotgunAttack");
                Debug.Log("Has attacked!");
                Collider2D[] enemiesToDamage=Physics2D.OverlapCircleAll(attackPos.position,attackRange,whatIsEnemies);
                for (int i=0; i<enemiesToDamage.Length;i++){
                    if(enemiesToDamage[i].tag == "Enemy")
                    {
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                        audioPlayer.Play();
                    }
                    
                }
                timeBtwAttack=startTimeBtwAttack;
            }

        } else {
            timeBtwAttack-=Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(attackPos.position,attackRange);
    }
}
