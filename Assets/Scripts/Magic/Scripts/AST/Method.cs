using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Method : Node
{
    //Subnodes
    public NodeListOptional<Statement> Statements = new NodeListOptional<Statement>();

    //Buildup
    public Method(GameObject Model) : base(Model)
    {
        nextStatementPosition = .052f;
        Statements.SetParent(this);
    }

    public override void Erase()
    {
        base.Erase();
    }

    //Unique methods
    public Shard GetShard()
    {
        return (Shard)Parent;
    }

    public override bool Invoke()
    {
        Debug.Log("Invoking method");
        receivingFromOwnBreak = Statements.Invoke();
        return receivingFromOwnBreak;
    }

    float nextStatementPosition;
    public void AcceptStatement(Statement s)
    {
        Transform sTform = s.transform;
        sTform.parent = transform;
        sTform.localPosition = new Vector3(0, -nextStatementPosition, 0);
        Statements.AddNode(s);
        nextStatementPosition += .052f;
    }
}
