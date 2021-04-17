using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlayerCharacter : Character
{
    private float normalSpeed;
    private float evadeTimer;
    private bool evading;
    private float evadeCooldown;
    [SerializeField] private float evadeCooldownTime;
    [SerializeField] private float evadeTime;
    [SerializeField] private float evadeDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        
        SceneContents.Instance.AddPlayerCharacter(this);
        
        //damageArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        ProcessFacing();
    }
    
    private void ProcessInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weaponAnimator.SetTrigger("Attack");
        }

        
        if (!evading && evadeCooldown == 0 && Input.GetButtonDown("Jump"))
        {
            evading = true;
            evadeTimer = evadeTime;
            evadeCooldown = evadeCooldownTime;
            normalSpeed = moveSpeed;
            //animator.SetTrigger("Evading");
        }

        if (evading)
        {
            Debug.Log("evade");
            evadeTimer = Mathf.Max(0f, evadeTimer - Time.deltaTime);

            //moveInput = transform.forward;
            moveSpeed = evadeDistance / evadeTime;

            if (evadeTimer == 0)
            {
                evading = false;
                moveSpeed = normalSpeed;
            }
        }
        else
        {
            evadeCooldown = Mathf.Max(0f, evadeCooldown - Time.deltaTime);
            
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            
            Movement = Mathf.Clamp(moveInput.magnitude, 0.0f, 1.0f);
            moveInput.Normalize();
        }
    }
    
    private void ProcessFacing()
    {
        var facing = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        pivot.localScale = facing.x >= 0 ? new Vector3(1f, 1f, 1f) : new Vector3(1f, -1f, 1f);
        sprite.localScale = facing.x >= 0 ? new Vector3(1f, 1f, 1f) : new Vector3(-1f, 1f, 1f);
    }
}
