using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASTNodeMonoWaitSigil : ASTNodeMono
{
    public WaitSigil waitSigil;
    public NumberArgument seconds;

    public override Method ContainingMethod()
    {
        return waitSigil.FindMethod();
    }
    public override Statement ContainingStatement()
    {
        return waitSigil;
    }

    // Start is called before the first frame update
    public void Initialize()
    {
        waitSigil = new WaitSigil(gameObject);
        waitSigil.seconds = seconds;
    }

    void Start()
    {
        if(waitSigil is null)
            Initialize();
    }

    private void Update()
    {
        
    }

    public void UpdateValue()
    {
        waitSigil.seconds.UpdateValue();
    }
}
