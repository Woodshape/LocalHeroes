using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Timeline;

public class Character : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float MoveSpeed = 4.0f;
    [SerializeField] protected Vector2 MoveInput = Vector2.zero;
    protected float Movement = 1f;
    protected Rigidbody2D Body;
    
    [Header("GameObject")]
    [SerializeField] protected Transform pivot;

    [SerializeField] protected GameObject damageArea;
    
    [Header("Animation")]
    [SerializeField] protected Animator weaponAnimator;

    [SerializeField] protected Transform sprite;
    
    
    
    
    // Start is called before the first frame update
    protected void Start()
    {
        Body = GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate()
    {
        Body.velocity = MoveInput * (Movement * MoveSpeed);

        if (MoveInput.x < 0)
        {
            sprite.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            sprite.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
