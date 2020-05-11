using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : ScriptableObject
{
    //fake GameObject things for convenience. some nodes will use this
    public GameObject gameObject;
    public virtual Transform transform
    {
        get { return gameObject.transform;  }
    }

    //Node doesn't have Subnodes, but every Node has a Parent (or a null, if you're the ritual master)
    public Node Parent;

    //Execution tracking
    public bool receivingFromOwnBreak;

    //Buildup
    public Node(GameObject Model) : this()
    {
        gameObject = Model;
    }
    public Node()
    {
        receivingFromOwnBreak = false;
    }

    //Teardown
    public virtual void Erase()
    {
        receivingFromOwnBreak = false;
    }

    //Unique methods
    public void SetParent(Node parent)
    {
        Parent = parent;
    }

    //The magix
    public abstract bool Invoke();
}
