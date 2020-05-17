using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASTNodeMonoMethod : ASTNodeMono
{
    //Method
    public Method method;

    public override Method ContainingMethod()
    {
        return method;
    }
    public override Statement ContainingStatement()
    {
        return null;
    }

    public void Initialize()
    {
        method = new Method(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(method is null)
        {
            Initialize();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
