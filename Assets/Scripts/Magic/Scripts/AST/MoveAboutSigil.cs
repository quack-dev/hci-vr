using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAboutSigil : Statement
{
    //public bool updateAfterTranslation;
    public NumberArgument x;
    public NumberArgument y;
    public NumberArgument z;
    public NumberArgument axis;
    public NumberArgument amount;
    public MoveAboutSigil(GameObject Model) : base(Model)
    {
    }
    public MoveAboutSigil(GameObject Model, float xv, float yv, float zv, float axisv, float amountv) : base(Model)
    {
        x.SetValue(xv);
        y.SetValue(yv);
        z.SetValue(zv);
        x.SetValue(axisv);
        y.SetValue(amountv);
    }

    public override bool Invoke()
    {
        if (!receivingFromOwnBreak)
        {
            receivingFromOwnBreak = true;
            return true;
        }
        Debug.Log("Invoke MoveAboutSigil");
        Shard s = FindShard();
        if (s is null)
        {
            Debug.Log("Couldn't find Shard for instruction :(");
        }
        Vector3 center = new Vector3(x.Value, y.Value, z.Value);
        Vector3 centerline = s.Position() - center;
        Debug.Log(s.Position());
        Debug.Log(centerline);
        if (axis.Value == 0)
        {
            centerline = Quaternion.Euler(amount.Value, 0, 0) * centerline;
        }
        else if (axis.Value == 1)
        {
            centerline = Quaternion.Euler(0, amount.Value, 0) * centerline;
        }
        else if(axis.Value == 2)
        {
            centerline = Quaternion.Euler(0, 0, amount.Value) * centerline;
        }
        Debug.Log(centerline);
        s.Move(center + centerline);
        Debug.Log("Moving shard to " + s.transform.Find("Model").position.ToString());
        return false;
    }
}
