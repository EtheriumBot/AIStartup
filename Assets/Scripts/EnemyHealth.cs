using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int health = 10;

    public void TakeDamage(int damage)
    {
        // Here you can implement health deduction logic
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
