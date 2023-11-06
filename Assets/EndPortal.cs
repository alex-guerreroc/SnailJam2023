using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Check if the player has collided with the portal
        if (other.gameObject.CompareTag("Player"))
        {
            //debug message
            Debug.Log("Player has collided with the portal");
            // Load the next level
            SceneManager.LoadScene(0);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
