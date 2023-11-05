using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEnemigoNuevo : MonoBehaviour
{

    public int health;
    public float speed;

    public GameObject bloodEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health<=0){
            Destroy(gameObject);
        }
        transform.Translate(Vector2.left*speed*Time.deltaTime);

    }
    public void TakeDamage(int damage){
        Instantiate(bloodEffect,transform.position,Quaternion.identity);
        health-=damage;
        Debug.Log("Damage Taken!");

    }
}
