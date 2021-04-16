using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Health>() != null)
        {
            var damage = 3;
            
            other.GetComponent<Health>().TakeDamage(damage);
            Debug.Log($"{other} takes {damage} damage");
        }
    }
}
