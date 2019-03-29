
using UnityEngine;

public class enemyTarget : MonoBehaviour
{

    public float health = 50f;

    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Dead();
        }
    }

    void Dead()
    {
        Destroy(gameObject);
    }
}
