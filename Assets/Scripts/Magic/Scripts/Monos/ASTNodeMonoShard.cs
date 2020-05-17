using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASTNodeMonoShard : ASTNodeMono
{
    //Ritual
    public Shard shard;
    public GameObject InvokePrefab;
    public GameObject ChantPrefab;

    public override Method ContainingMethod()
    {
        return null;
    }
    public override Statement ContainingStatement()
    {
        return null;
    }

    public void Initialize()
    {
        shard = new Shard(gameObject);
        shard.InvokePrefab = InvokePrefab;
        shard.ChantPrefab = ChantPrefab;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(shard is null)
        {
            Initialize();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
