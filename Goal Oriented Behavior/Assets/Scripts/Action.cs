using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public float insistence;
    public ActionType type;

    private bool mInitted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void init(float i, ActionType t)
    {
        if (mInitted == false)
        {
            insistence = i;
            type = t;
            mInitted = true;
        }
    }
}
