using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStatus : MonoBehaviour
{
    public int enemyCount = 0;
    public GameObject portal; // Assign your portal GameObject in the inspector

    // Start is called before the first frame update
    void Start()
    {
        portal.SetActive(false); // Make sure the portal is not active at the start
        enemyCount = CountEnemies(); // Count the number of enemies
    }

    // Call this function when an enemy is destroyed
    public void EnemyDestroyed()
    {
        enemyCount--;

        if (enemyCount <= 0)
        {
            OpenPortal();
        }
    }

    void OpenPortal()
    {
        portal.SetActive(true); // Activate the portal
    }

    int CountEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    // Call this function to go to the next level
    public void GoToNextLevel()
    {
        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Update()
    {

        enemyCount = CountEnemies(); // Count the number of enemies

        if (enemyCount <= 0)
        {
            OpenPortal();
        }
    }

}
