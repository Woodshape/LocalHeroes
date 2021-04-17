using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAction : Action
{
    private Enemy self;
    private PlayerCharacter target;

    private float attackTime;
    private Vector2 normalPosition;
    
    public MeleeAction(Enemy enemy, PlayerCharacter character)
    {
        this.self = enemy;
        this.target = character;
    }
    
    public override void Perform()
    {
        if (Time.time >= attackTime)
        {
            Debug.Log($"Melee: {self} -> {target}");
            
            attackTime = Time.time + self.AttackSpeed;
            normalPosition = self.transform.position;

            var targetPosition = target.transform.position;
            
            float percent = 0f;
            while (percent <= 1)
            {
                Debug.Log($"Attacking: {self} -> {target}");
                
                percent += Time.deltaTime * self.AttackSpeed;
                var formula = (-Mathf.Pow(percent, 2) + percent) * 4;
                self.Body.velocity = Vector2.Lerp(normalPosition, targetPosition, formula);
            }
        }
    }
}
