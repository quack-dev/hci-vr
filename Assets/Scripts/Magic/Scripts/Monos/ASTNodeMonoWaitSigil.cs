using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASTNodeMonoWaitSigil : MonoBehaviour
{
    public WaitSigil waitSigil;
    public NumberArgument seconds;

    // Start is called before the first frame update
    public void Initialize()
    {
        waitSigil = new WaitSigil(gameObject);
        waitSigil.seconds = seconds;
    }

    void Start()
    {
        Initialize();
    }

    private void Update()
    {
        
    }

    public void UpdateValue()
    {
        waitSigil.seconds.UpdateValue();
    }
}
