using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variable<T> : Object
{
    public string Name;
    public T Value;

    public Variable(string name, T value)
    {
        Name = name;
        Value = value;
    }

    public static Variable<int> IntVar(string name, int value = 0)
    {
        return new Variable<int>(name, value);
    }
}
