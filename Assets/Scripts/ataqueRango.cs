using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ataqueRango : MonoBehaviour
{
    public float offset;
    public GameObject projectile;
    public Transform shotPoint;
    private float timeBtwShots;
    public float startTimeBtwShots;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
 private void Update()
    {
        // Handles the weapon rotation
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - (transform.position + new Vector3(0,+1.04f,0));
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("ShovelAttack");
                timeBtwShots = startTimeBtwShots;
            }
        }
        else {
            timeBtwShots -= Time.deltaTime;
        }

       
    }

    public void ShovelAttack()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - (transform.position + new Vector3(0,+1.04f,0));
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, rotZ + offset));
    }
    // Update is called once per frame

}
