using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeListOptional<T> : Node
    where T : Node
{
    //Subnodes
    public LinkedList<T> Children;
    public int Count { get { return Children.Count; } }

    //Execution tracking
    public LinkedListNode<T> PC; //Program Counter -- next instruction to execute in linear
    public LinkedList<T> RevisitNodes;

    //Buildup
    public NodeListOptional() : base()
    {
        PC = null;
        Children = new LinkedList<T>();
    }

    //Teardown
    public override void Erase()
    {
        //subnodes...
        for (PC = Children.First; !(PC is null); PC = PC.Next)
        {
            PC.Value.Erase();
        }
        //this node...
        RevisitNodes = null;
        PC = null;
        //base node...
        base.Erase();
    }
    
    //Unique methods
    public void AddNode(T node)
    {
        node.SetParent(this);
        Children.AddLast(node);
    }

    public void AddFirst(T node)
    {
        node.SetParent(this);
        Children.AddFirst(node);
    }

    public void RemoveNode(T node)
    {
        Children.Remove(node);
        node.SetParent(null);
        if (Parent is Method)
        {
            ((Method)Parent).UpdateStatementPosition();
        }
    }

    public void AddAfterNode(T nodeRef, T newNode)
    {
        newNode.SetParent(this);
        Children.AddAfter(Children.Find(nodeRef), newNode);
        if (Parent is Method)
        {
            ((Method)Parent).UpdateStatementPosition();
        }
    }

    //Magix
    public bool InvokeLinear()
    {
        Debug.Log("Invoking linear NodeListOptional" + typeof(T).ToString() + " ( " + Children.Count + ")");
        if (Children.Count == 0)
            return false;
        //Start at the beginning if this Node wasn't interrupted, otherwise start at the child that interrupted
        if (!receivingFromOwnBreak)
        {
            PC = Children.First;
        }
        for (; PC != null; PC = PC.Next)
        {
            //If this child says to revisit it next pass, that's where PC will be
            if (PC.Value.Invoke())
            {
                receivingFromOwnBreak = true;
                return true;
            }
        }
        //finished without interruption, start at the beginning next time
        receivingFromOwnBreak = false;
        return false;
    }

    //InvokeAll to Tick on Rituals and Shards
    public bool InvokeAll()
    {
        Debug.Log("Invoking all NodeListOptional " + typeof(T).ToString() + " (" + Children.Count + ")");
        if (Children.Count == 0)
            return false;
        if (RevisitNodes is null)
        {
            RevisitNodes = new LinkedList<T>(Children);
        }
        LinkedList<T> NodesToRevisit = new LinkedList<T>();
        for (PC = RevisitNodes.First; PC != null; PC = PC.Next)
        {
            //If this child says to revisit it next pass, that's where PC start next pass
            if (PC.Value.Invoke())
            {
                NodesToRevisit.AddLast(PC.Value);
            }
        }
        RevisitNodes = new LinkedList<T>(NodesToRevisit);
        receivingFromOwnBreak = RevisitNodes.Count > 0;
        if (!receivingFromOwnBreak)
        {
            RevisitNodes = null;
        }
        return receivingFromOwnBreak;
    }

    private bool DoInvokeAll()
    {
        return WorksAs(typeof(Ritual)) | WorksAs(typeof(Shard));
    }
    private bool WorksAs(System.Type t)
    {
        return t == typeof(T) || t.IsSubclassOf(typeof(T));
    }
    
    public override bool Invoke()
    {
        if(DoInvokeAll())
        {
            return InvokeAll();
        }
        else
        {
            return InvokeLinear();
        }
    }
}
