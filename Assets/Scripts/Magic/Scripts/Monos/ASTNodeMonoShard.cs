using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASTNodeMonoShard : MonoBehaviour
{
    //Ritual
    public Shard shard;
    public GameObject InvokePrefab;
    public GameObject ChantPrefab;

    public void Initialize()
    {
        shard = new Shard(gameObject);
        shard.InvokePrefab = InvokePrefab;
        shard.ChantPrefab = ChantPrefab;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(shard == null)
        {
            Initialize();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
