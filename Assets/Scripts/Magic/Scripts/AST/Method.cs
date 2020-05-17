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

    public void UpdateStatementPosition()
    {
        float nextStatementPosition = .052f;
        for(LinkedListNode<Statement> s = Statements.Children.First; !(s is null); s = s.Next)
        {
            Transform sTform = s.Value.transform;
            sTform.localPosition = new Vector3(0, -nextStatementPosition, 0);
            nextStatementPosition += .052f;
        }
    }

    public void AcceptFirstStatement(Statement s)
    {
        if (s is null)
        {
            Debug.Log("Well this shouldn't happen -- attemping to add null first statement to method");
            return;
        }
        Transform sTform = s.transform;
        sTform.parent = transform;
        Statements.AddFirst(s);
        UpdateStatementPosition();
    }

    public void AcceptStatement(Statement s)
    {
        if(s is null)
        {
            Debug.Log("Well this shouldn't happen -- attemping to add null statement to method");
            return;
        }
        Transform sTform = s.transform;
        sTform.parent = transform;
        Statements.AddNode(s);
        UpdateStatementPosition();
    }
}
