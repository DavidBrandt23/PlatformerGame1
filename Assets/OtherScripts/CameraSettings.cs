using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[Serializable]
public class CameraSettings
{
    public int CameraMode;
    public FloatReference leftXBound;
    public FloatReference rightXBound;
    public FloatReference bottomYBound, topYBound;
    public Vector3Reference targetPos; 

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void assignSettings(CameraSettings other)
    {
        CameraMode = other.CameraMode;
        leftXBound = other.leftXBound;
        rightXBound = other.rightXBound;
        bottomYBound = other.bottomYBound;
        topYBound = other.topYBound;
        targetPos = other.targetPos;
    }
#if UNITY_EDITOR
    //[CustomEditor(typeof(CameraSettings))]
    //public class CameraSettingsEditor : Editor
    //{
    //    private CameraSettings settings { get { return (target as CameraSettings); } }

    //    public override void OnInspectorGUI()
    //    {
    //        EditorGUI.BeginChangeCheck();

    //        settings.a = EditorGUILayout.FloatField("aa", settings.a);
    //        settings.b = EditorGUILayout.FloatField("bb", settings.b);
    //        if (EditorGUI.EndChangeCheck())
    //            EditorUtility.SetDirty(settings);
    //    }
    //}
#endif
}
