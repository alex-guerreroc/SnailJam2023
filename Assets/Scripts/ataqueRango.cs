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
    // Start is called before the first frame update
 private void Update()
    {
        // Handles the weapon rotation
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Debug.Log("Has shot!");
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else {
            timeBtwShots -= Time.deltaTime;
        }

       
    }

    // Update is called once per frame

}
