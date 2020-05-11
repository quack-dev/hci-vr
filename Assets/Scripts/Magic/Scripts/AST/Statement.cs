using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Statement : Node
{
    public Statement(GameObject Model) : base(Model)
    {
    }

    public Shard FindShard()
    {
        Node n = this.Parent;
        Debug.Log("Starting with parent " + n.GetType().ToString());
        while(!(n is null))
        {
            Debug.Log("Checking " + n.GetType().ToString());
            if(n is Shard)
            {
                Debug.Log("Found a shard!");
                return (Shard)n;
            }
            else
            {
                Debug.Log("No shard -- found " + n.GetType().ToString());
            }
            n = n.Parent;
        }
        return null;
    }

    public abstract override bool Invoke();
}
