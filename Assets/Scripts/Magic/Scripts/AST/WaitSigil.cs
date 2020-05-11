using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitSigil : Statement
{
    public NumberArgument seconds;
    public float stopwatch;
    public bool active;

    public WaitSigil(GameObject Model) : base(Model)
    {
        stopwatch = 0;
    }
    public WaitSigil(GameObject Model, float secondsv) : this(Model)
    {
        seconds.SetValue(secondsv);
    }

    //Teardown
    public override void Erase()
    {
        active = false;
        stopwatch = 0;
    }

    public override bool Invoke()
    {
        if (!active)
        {
            active = true;
            return true;
        }
        stopwatch += Time.deltaTime;
        if (stopwatch >= seconds.Value)
        {
            active = false;
            stopwatch = 0;
            return false;
        }
        return true;
    }
}
