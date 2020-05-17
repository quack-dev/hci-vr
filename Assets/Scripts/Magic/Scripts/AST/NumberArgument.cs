using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NumberArgument : Argument<float>
{
    public TMPro.TMP_InputField input;
    public void UpdateValue()
    {
        float value = 0f;
        bool success = float.TryParse(input.text, out value);
        if (success)
        {
            Value = value;
        }
    }
    public void SetValue(float value)
    {
        input.text = value.ToString();
        Value = value;
    }
    public void Select()
    {
        EventSystem.current.SetSelectedGameObject(input.gameObject);
    }
}
