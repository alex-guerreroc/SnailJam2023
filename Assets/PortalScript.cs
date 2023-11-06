using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has collided with the portal
        if (other.gameObject.CompareTag("Player"))
        {
            //debug message
            Debug.Log("Player has collided with the portal");
            // Load the next level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



    }
}
