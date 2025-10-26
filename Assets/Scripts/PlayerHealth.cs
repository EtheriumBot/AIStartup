using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{

    public int health = 10;

    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Player has died!");
            SceneManager.LoadScene("Game Over Scene");
        }
    }

}
