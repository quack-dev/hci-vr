using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Arguments come from editor vars
public abstract class Argument<T> : MonoBehaviour
{
    public string Name;
    public T Value;
    //public abstract T GetValue();
}
