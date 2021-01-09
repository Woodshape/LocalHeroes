using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEvaluator
{
    private readonly float[] strategyWeights;
        
    public ActionEvaluator()
    {
        var numStrategies = Enum.GetValues(typeof(Strategy)).Length;
            
        //    initialize strategy weights
        strategyWeights = new float[numStrategies];
        for (int i = 0; i < numStrategies; i++)
        {
            strategyWeights[i] = 0;
        }
    }

    public void Update()
    {
        //    slowly decrease all weights of all strategies
        float decrease = Time.deltaTime * 0.1f;
        for (int i = 0; i < strategyWeights.Length; i++)
        {
            strategyWeights[i] = Mathf.Max(strategyWeights[i] - decrease, 0);
        }
    }

    public Action TrySelectAction(Enemy enemy)
    {
        List<(Strategy Strategy, float Score, Action Action)> options = enemy.EvaluateAllAbilities();

        float bestScore = 0.5f;
        Action bestAction = null;
        Strategy bestStrategy = Strategy.NONE;

        foreach (var option in options)
        {
            var score = option.Score - strategyWeights[(int) option.Strategy];

            if (score > bestScore)
            {
                bestScore = score;
                bestAction = option.Action;
                bestStrategy = option.Strategy;
            }
        }
            
        if (bestAction == null)
            return null;

        for (int i = 0; i < strategyWeights.Length; i++)
        {
            if (i == (int)bestStrategy)
            {
                strategyWeights[i] += 1;
            }
            else
            {
                strategyWeights[i] *= 0.75f;
            }
                
            //strategyWeights[(int)Strategy.NONE] = 1f;
        }

        return bestAction;
    }
}
