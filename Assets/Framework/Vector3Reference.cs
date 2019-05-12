using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Vector3Reference
{
    public bool UseConstant = true;
    public Vector3 ConstantValue;
    public Vector3Variable Variable;

    public Vector3 Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value;  }
    }
}
