using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scrolling : MonoBehaviour
{

    public float scrollSpeed = 2.0f; // Adjust the speed in the Inspector
    private bool isScrolling = false;
    private Vector3 initialPosition;


    public void startScrollingTrue()
    {
        isScrolling = true;
    }

    public void ResetPosition()
    {
        isScrolling = false;
        transform.position = initialPosition;

    }

    void Start()
    {
        initialPosition = transform.position;
    }



    void Update()
    {
        if (isScrolling)
        {
            // Move the object upward
            transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
        }
    }


}
