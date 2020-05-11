using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASTNodeMonoMethod : MonoBehaviour
{
    //Method
    public Method method;

    public void Initialize()
    {
        method = new Method(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(method == null)
        {
            Initialize();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
