using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public float insistence;
    public GoalType type;
    public List<Action> actions;

    private bool mInitted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init(float i, GoalType t)
    {
        if (mInitted == false)
        {
            actions = new List<Action>();
            insistence = i;
            type = t;

            if (t == GoalType.ATTACK)
            {
                Action openFire = gameObject.AddComponent<Action>();
                openFire.init(0f, ActionType.OPEN_FIRE);
                actions.Add(openFire);

                Action chase = gameObject.AddComponent<Action>();
                chase.init(0f, ActionType.CHASE);
                actions.Add(chase);
            }
            else if (t == GoalType.RELOAD)
            {
                Action reload = gameObject.AddComponent<Action>();
                reload.init(0f, ActionType.RELOAD_GUN);
                actions.Add(reload);

                Action chase = gameObject.AddComponent<Action>();
                chase.init(0f, ActionType.CHASE);
                actions.Add(chase);
            }

            mInitted = true;
        }
    }
}
