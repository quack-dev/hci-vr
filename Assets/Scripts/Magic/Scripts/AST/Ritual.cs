using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ritual : Node
{
    //Prefabs & editor vars
    public GameObject ShardPrefab;
    public Vector3 DefaultLocation;

    //Subnodes
    NodeListOptional<Shard> Shards;

    //Execution tracking
    Shard SelectedShard;
    public bool OnAir;
    public bool Paused;

    //Buildup
    public Ritual(GameObject Model) : base(Model)
    {
        //Initialize children to have this as Parent
        Shards = new NodeListOptional<Shard>();
        Shards.SetParent(this);
        OnAir = false;
        Paused = false;
    }

    //Teardown
    public override void Erase()
    {
        //subnodes...
        Shards.Erase();
        //this node...
        OnAir = false;
        Paused = false;
        //base node...
        base.Erase();
    }

    //Unique methods
    public Shard NewShard()
    {
        ASTNodeMonoShard sgo = Instantiate(ShardPrefab, Vector3.zero, Quaternion.identity).GetComponent<ASTNodeMonoShard>();
        sgo.Initialize();
        Shard s = sgo.shard;
        s.CreateMainMethods();
        s.transform.parent = transform;
        s.Move(DefaultLocation);
        Shards.AddNode(s);
        SelectShard(s);
        return s;
    }

    public void Pause()
    {
        OnAir = false;
        Paused = true;
        SelectShard();
    }

    public void Resume()
    {
        OnAir = true;
        Paused = false;
        UnselectShard();
    }

    //this will happen when going into execution
    public void UnselectShard()
    {
        if(SelectedShard != null)
        {
            SelectedShard.unselect();
        }
    }
    //this will happen when coming out of execution
    public void SelectShard()
    {
        if(SelectedShard != null)
        {
            SelectedShard.select();
        }
    }

    public void SelectShard(Shard s)
    {
        UnselectShard();
        SelectedShard = s;
        s.select();
        //load grimoire for shard
    }

    //Magix
    public override bool Invoke()
    {
        Debug.Log("Invoking Ritual");
        Debug.Log("children: " + Shards.Count);
        bool revisit = Shards.Invoke();
        OnAir = revisit && Parent == null;
        return revisit;
    }

    public void InvokeRitualMaster()
    {
        UnselectShard();
        Invoke();
    }
}
