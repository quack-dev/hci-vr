using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : Node
{
    //Prefabs & editor vars
    public GameObject InvokePrefab;
    public GameObject ChantPrefab;

    //Subnodes
    public Method InvokeMethod;
    public Method ChantMethod;

    //Execution tracking
    public bool Initialized;

    //Buildup
    public Shard(GameObject Model) : base(Model)
    {
        Initialized = false;
    }

    //Teardown
    public override void Erase()
    {
        //subnodes...
        if (InvokeMethod != null) InvokeMethod.Erase();
        if (ChantMethod != null) ChantMethod.Erase();
        //this node...
        Initialized = false;
        //base node...
        base.Erase();
    }

    //Unique methods
    public void CreateMainMethods()
    {
        GameObject InvokeGO = Instantiate(InvokePrefab, Vector3.zero, Quaternion.identity);
        GameObject ChantGO = Instantiate(ChantPrefab, Vector3.zero, Quaternion.identity);
        ASTNodeMonoMethod InvokeNodeMono = InvokeGO.GetComponent<ASTNodeMonoMethod>();
        ASTNodeMonoMethod ChantNodeMono = ChantGO.GetComponent<ASTNodeMonoMethod>();
        InvokeNodeMono.Initialize();
        ChantNodeMono.Initialize();
        InvokeMethod = InvokeNodeMono.method;
        ChantMethod = ChantNodeMono.method;
        InvokeMethod.gameObject = InvokeGO;
        ChantMethod.gameObject = ChantGO;
        InvokeMethod.SetParent(this);
        ChantMethod.SetParent(this);

        InvokeMethod.transform.parent = transform.Find("MethodSpace");
        ChantMethod.transform.parent = transform.Find("MethodSpace");
        InvokeMethod.transform.localPosition = new Vector3(-.511f, .554f, 0f);
        ChantMethod.transform.localPosition = new Vector3(0f, .554f, 0f);
    }

    public void Move(Vector3 position) //world coords
    {
        transform.Find("Model").position = position;
    }

    public void select()
    {
        transform.Find("MethodSpace").gameObject.SetActive(true);
    }

    public void unselect()
    {
        transform.Find("MethodSpace").gameObject.SetActive(false);
    }

    public override bool Invoke()
    {
        if (!Initialized)
        {
            Debug.Log("Invoking Invocation");
            Initialized = !InvokeMethod.Invoke();
            return true;
        }
        return Chant();
    }

    public bool Chant()
    {
        Debug.Log("Invoking Chant");
        if(ChantMethod.Statements.Count == 0)
        {
            Initialized = false;
            return false;
        }
        ChantMethod.Invoke();
        return true;
    }
}
