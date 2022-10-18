using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoalType
{
    INVALID = 0,
    ATTACK,
    RELOAD
}

public enum ActionType
{
    INVALID = 0,
    RELOAD_GUN,
    OPEN_FIRE,
    CHASE
}

public class GoalSystem : MonoBehaviour
{
    public List<Goal> currentGoals;
    public Enemy enemy;

    private void Start()
    {
        //goal system
        currentGoals = new List<Goal>();
        Goal attack = gameObject.AddComponent<Goal>();
        attack.init(0f, GoalType.ATTACK);
        currentGoals.Add(attack);

        Goal reload = gameObject.AddComponent<Goal>();
        reload.init(0f, GoalType.RELOAD);
        currentGoals.Add(reload);
    }

    public void DetermineInsistenceValues()
    {
        Vector3 m = transform.position - Target.position;
        float range = Mathf.Sqrt((m.x * m.x) + (m.z * m.z));

        //calculate goal's insistence
        for (int i = 0; i < currentGoals.Count; i++)
        {
            Goal goal = currentGoals[i];
            goal.insistence = 0;

            if (goal.type == GoalType.ATTACK)
            {
                //ammo
                if (enemy.shotCount < 5)
                {
                    goal.insistence++;

                    if (enemy.shotCount == 0)
                        goal.insistence++;
                }

                //range
                if (range <= 10)
                    goal.insistence++;
            }
            else if (goal.type == GoalType.RELOAD)
            {
                //ammo
                if (enemy.shotCount > 3)
                {
                    goal.insistence++;

                    if (enemy.shotCount == 5)
                        goal.insistence++;
                }

                //range
                if (range > 10)
                    goal.insistence++;
            }

            //calcuate the local action's insistence's
            for (int k = 0; k < currentGoals[i].actions.Count; k++)
            {
                Action action = goal.actions[k];
                action.insistence = 0;

                if (action.type == ActionType.OPEN_FIRE)
                {
                    //ammo
                    if (enemy.shotCount < 3)
                    {
                        action.insistence++;

                        if (enemy.shotCount == 0)
                            action.insistence++;
                    }

                    //range
                    if (range <= 10)
                        action.insistence++;
                }
                else if (action.type == ActionType.RELOAD_GUN)
                {
                    //ammo
                    if (enemy.shotCount > 3)
                    {
                        action.insistence++;

                        if (enemy.shotCount == 5)
                            action.insistence++;
                    }

                    //range
                    if (range > 10)
                        action.insistence++;
                }
                else if (action.type == ActionType.CHASE)
                {
                    //range
                    if (range > 5)
                    {
                        action.insistence++;

                        if (range > 10)
                            action.insistence++;

                        if (range > 20)
                            action.insistence++;
                    }
                }
            }
        }
    }

    public Goal GetBestGoal()
    {
        float highest = 0f;
        int index = -1;

        for (int i = 0; i < currentGoals.Count; i++)
        {
            if (highest == 0f || highest < currentGoals[i].insistence)
            {
                highest = currentGoals[i].insistence;
                index = i;
            }
        }

        return currentGoals[index];
    }

    public Action GetBestAction(Goal theGoal)
    {
        float highest = 0f;
        int index = -1;

        for (int i = 0; i < theGoal.actions.Count; i++)
        {
            if (highest == 0f || highest < theGoal.actions[i].insistence)
            {
                highest = theGoal.actions[i].insistence;
                index = i;
            }
        }

        return theGoal.actions[index];
    }

    public void UpdateGoalSystem()
    {
        DetermineInsistenceValues();
        GenerateAction();
    }

    public void GenerateAction()
    {
        Goal topGoal = GetBestGoal();
        ActionType theType = GetBestAction(topGoal).type;

        //determines what should happen according to the action received
        switch (theType)
        {
            case ActionType.OPEN_FIRE:
                enemy.OpenFire();
                break;
            case ActionType.RELOAD_GUN:
                enemy.Reload();
                break;
            case ActionType.CHASE:
                enemy.Integrate();
                break;
            default:
                break;
        }
    }
}