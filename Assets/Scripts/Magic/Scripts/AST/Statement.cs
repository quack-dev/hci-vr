using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Statement : Node
{
    public Statement(GameObject Model) : base(Model)
    {
    }

    public Method FindMethod()
    {
        Node n = this.Parent;
        Debug.Log("Starting with parent " + n.GetType().ToString());
        while (!(n is null))
        {
            Debug.Log("Checking " + n.GetType().ToString());
            if (n is Method)
            {
                Debug.Log("Found a method!");
                return (Method)n;
            }
            else
            {
                Debug.Log("No method -- found " + n.GetType().ToString());
            }
            n = n.Parent;
        }
        return null;
    }

    public Shard FindShard()
    {
        Node n = this.Parent;
        while(!(n is null))
        {
            if(n is Shard)
            {
                return (Shard)n;
            }
            else
            {
            }
            n = n.Parent;
        }
        return null;
    }

    public void RemoveSelf()
    {
        if(Parent is NodeListOptional<Statement>)
        {
            ((NodeListOptional<Statement>)Parent).RemoveNode(this);
        }
    }

    public void AddStatementAfter(Statement s)
    {
        if (s is null)
        {
            Debug.Log("Well this shouldn't happen -- attemping to add null statement to statement");
            return;
        }
        if (Parent is NodeListOptional<Statement>)
        {
            ((NodeListOptional<Statement>)Parent).AddAfterNode(this, s);
        }
    }

    public void ReplaceWith(Statement s)
    {
        AddStatementAfter(s);
        RemoveSelf();
    }

    public abstract override bool Invoke();
}
