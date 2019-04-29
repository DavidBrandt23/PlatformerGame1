using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class FloatReference : ScriptableObject
{
    public bool UseConstant = true;
    public float ConstantValue;
    public FloatVariable Variable;

    public float Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value;  }
    }
}
