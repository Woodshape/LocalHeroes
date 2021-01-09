using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
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
        MoveInput.x = Input.GetAxisRaw("Horizontal");
        MoveInput.y = Input.GetAxisRaw("Vertical");
        Movement = Mathf.Clamp(MoveInput.magnitude, 0.0f, 1.0f);
        MoveInput.Normalize();

        if (Input.GetMouseButtonDown(0))
        {
            weaponAnimator.SetTrigger("Attack");
        }
    }
    
    private void ProcessFacing()
    {
        var facing = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        pivot.localScale = facing.x >= 0 ? new Vector3(1f, 1, 1) : new Vector3(1f, -1, 1);
    }
}
