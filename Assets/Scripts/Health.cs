using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 10;
    
    bool isDead = false;
    public bool IsDead()
    {
        return isDead;
    }
    
    public void TakeDamage(int damage)
    {
        health = Mathf.Max(health - damage, 0);

        if (health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }
        
        isDead = true;

        GetComponent<Enemy>().Remove();
        Destroy(this.gameObject);
    }
}