using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASTNodeMonoRitual : MonoBehaviour
{
    //Ritual
    public Ritual ritual;
    public GameObject ShardPrefab;
    public Vector3 DefaultLocation;
    //temp
    public GameObject MovePrefab;
    public GameObject WaitPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ritual = new Ritual(gameObject);
        ritual.ShardPrefab = ShardPrefab;
        ritual.DefaultLocation = DefaultLocation;
        Vector3 center = new Vector3(-.071f, .51f, .67f);
        float angle;
        float n = 8;
        float resolution = 80f;
        float rotateTime = 3f;
        for (int i = 0; i < n; i++)
        {
            Shard s = ritual.NewShard();
            angle = ((6.28f / n) * i);
            Vector3 position = new Vector3(Mathf.Cos(angle), .6f, Mathf.Sin(angle)) + center;

            ASTNodeMonoTranslateSigil initialMove = Instantiate(MovePrefab).GetComponent<ASTNodeMonoTranslateSigil>();
            initialMove.x.SetValue(position.x);
            initialMove.y.SetValue(position.y);
            initialMove.z.SetValue(position.z);
            initialMove.Initialize();
            s.InvokeMethod.AcceptStatement(initialMove.translateSigil);

            for (int j = 0; j < resolution; j++)
            {
                float newAngle = angle + ((6.28f / resolution) * j);
                position = new Vector3(Mathf.Cos(newAngle), .6f, Mathf.Sin(newAngle)) + center;
                ASTNodeMonoTranslateSigil newMove = Instantiate(MovePrefab).GetComponent<ASTNodeMonoTranslateSigil>();
                newMove.x.SetValue(position.x);
                newMove.y.SetValue(position.y);
                newMove.z.SetValue(position.z);
                newMove.Initialize();
                s.ChantMethod.AcceptStatement(newMove.translateSigil);

                ASTNodeMonoWaitSigil wait = Instantiate(WaitPrefab).GetComponent<ASTNodeMonoWaitSigil>();
                wait.seconds.SetValue(rotateTime/resolution);
                wait.Initialize();
                s.ChantMethod.AcceptStatement(wait.waitSigil);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ritual.OnAir)
        {
            bool finished = !ritual.Invoke();
            if (finished)
            {
                ritual.OnAir = false;
                Debug.Log("Clean exit.");
            }
        }
    }

    public void BeginIncantation()
    {
        ritual.InvokeRitualMaster();
    }
}
