using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASTNodeMonoTranslateSigil : ASTNodeMono
{
    public TranslateSigil translateSigil;
    public NumberArgument x;
    public NumberArgument y;
    public NumberArgument z;

    public override Method ContainingMethod()
    {
        return translateSigil.FindMethod();
    }
    public override Statement ContainingStatement()
    {
        return translateSigil;
    }

    // Start is called before the first frame update
    public void Initialize()
    {
        translateSigil = new TranslateSigil(gameObject);
        translateSigil.x = x;
        translateSigil.y = y;
        translateSigil.z = z;
    }

    void Start()
    {
        if(translateSigil is null)
            Initialize();
    }

    public void UpdateValue()
    {
        translateSigil.x.UpdateValue();
        translateSigil.y.UpdateValue();
        translateSigil.z.UpdateValue();
    }
}
