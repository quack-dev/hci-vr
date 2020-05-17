using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASTNodeMonoMoveAboutSigil : ASTNodeMono
{
    public MoveAboutSigil moveAboutSigil;
    public NumberArgument x;
    public NumberArgument y;
    public NumberArgument z;
    public NumberArgument axis;
    public NumberArgument amount;

    public override Method ContainingMethod()
    {
        return moveAboutSigil.FindMethod();
    }
    public override Statement ContainingStatement()
    {
        return moveAboutSigil;
    }

    // Start is called before the first frame update
    public void Initialize()
    {
        moveAboutSigil = new MoveAboutSigil(gameObject);
        moveAboutSigil.x = x;
        moveAboutSigil.y = y;
        moveAboutSigil.z = z;
        moveAboutSigil.axis = axis;
        moveAboutSigil.amount = amount;
    }

    void Start()
    {
        if (moveAboutSigil is null)
            Initialize();
    }

    public void UpdateValue()
    {
        moveAboutSigil.x.UpdateValue();
        moveAboutSigil.y.UpdateValue();
        moveAboutSigil.z.UpdateValue();
        moveAboutSigil.axis.UpdateValue();
        moveAboutSigil.amount.UpdateValue();
    }
}
