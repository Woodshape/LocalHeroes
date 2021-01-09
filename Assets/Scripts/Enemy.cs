using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Header("AI")] public ActionEvaluator brain;

    private Action currentAction;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        brain = new ActionEvaluator();
        
        SceneContents.Instance.AddEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        brain.Update();
        Action action = brain.TrySelectAction(this);
        
        MoveInput = GetMovementIntention(this);

        if (action != null)
        {
            currentAction = action;
            //currentAction.Perform();
        }

        currentAction?.Perform();
    }

    public void Remove()
    {
        SceneContents.Instance.Enemies.Remove(this);
    }
    
    private const float CUTOFF_THRESHOLD = 0.5f;
    private const float TARGET_DISTANCE = 0.5f;
    
    public Vector3 GetMovementIntention(Enemy enemy) {
        Vector3 intention = Vector3.zero;
        
        foreach (PlayerCharacter player in SceneContents.Instance.PlayerCharacters) {
            //  chase player characters
            Vector3 direction = GetDirection(enemy, player);
            float distance = GetDistance(enemy, player);

            float springStrength = distance - TARGET_DISTANCE;

            intention += direction * springStrength; 
        }

        foreach (Enemy otherEnemy in SceneContents.Instance.Enemies)
        {
            if (otherEnemy == enemy)
            {
                continue;
            }
            
            Vector3 direction = GetDirection(enemy, otherEnemy);
            float distance = GetDistance(enemy, otherEnemy);
                
            float springStrength = 2.5f / (1f + Mathf.Pow(distance, 3));

            intention -= direction * springStrength;
        }

        if (intention.magnitude < CUTOFF_THRESHOLD)
        {
            return Vector3.zero;
        }

        return intention.normalized;
    }
    
    private float GetDistance(Enemy enemy, Character character) {
        return Vector3.Distance(enemy.transform.position, character.transform.position);
    }

    private Vector3 GetDirection(Enemy enemy, Character character) {
        return character.transform.position - enemy.transform.position;
    }
    
    public List<(Strategy Strategy, float Score, Action Action)> EvaluateAllAbilities()
    {
        List<(Strategy Strategy, float Score, Action Action)> abilities = new List<(Strategy Strategy, float Score, Action Action)>();

        foreach (PlayerCharacter character in SceneContents.Instance.PlayerCharacters)
        {
            abilities.Add(EvaluateMelee(this, character));
            abilities.Add(EvaluateChase(this, character));
        }

        return abilities;
    }
    
    private float FindAttackScore(Enemy enemy, PlayerCharacter character, float targetDistance) {
        float distance = GetDistance(enemy, character);
        float farness = Mathf.Abs(targetDistance - distance);
        float closeness = 1 - farness;

        return Mathf.Clamp01(closeness);
    }

    public (Strategy strategy, float score, Action action) EvaluateMelee(Enemy enemy, PlayerCharacter character)
    {
        float targetDistance = 1.5f;
        float score = FindAttackScore(enemy, character, targetDistance);

        return (Strategy.PRIMARY, score, new MeleeAction());
    }

    public (Strategy strategy, float score, Action action) EvaluateChase(Enemy enemy, PlayerCharacter character)
    {
        float score = GetDistance(enemy, character);

        return (Strategy.CHASE, score, new ChaseAction());
    }
}
