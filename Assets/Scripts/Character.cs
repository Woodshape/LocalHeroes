using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.Timeline;

public class Character : MonoBehaviour
{
    [FormerlySerializedAs("MoveSpeed")]
    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 4.0f;
    [FormerlySerializedAs("MoveInput")]
    [SerializeField] protected Vector2 moveInput = Vector2.zero;
    
    protected float Movement = 1f;

    public float AttackSpeed { get; set; } = 1f;

    public Rigidbody2D Body;
    
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
        Body.velocity = moveInput * (Movement * moveSpeed);

        if (moveInput.x < 0)
        {
            sprite.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            sprite.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
