using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LincolnBossController : EnemyCommonController
{
    private LincolnAction CurrentAction;
    private float riseHeight = 8.0f;
    private float riseSpeed = 0.08f;
    private float stompPositioningSpeed = 0.08f;
    private float stompSpeed = 0.2f;
    public Vector3Variable targetPos;

    private enum LincolnAction
    {
        Rising,
        Stomping,
        StompPositioning
    };
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        CurrentAction = LincolnAction.Rising;
    }
    

    private void FixedUpdate()
    {
        Vector3 newPosition = transform.position;
        Vector3 oldPos = transform.position;
        Vector3 oldLocalPos = transform.localPosition;
        switch (CurrentAction) {
            case (LincolnAction.Rising):
                //float curY = newPosition.y;
                if(oldLocalPos.y < riseHeight)
                {
                    newPosition.y += riseSpeed;
                }
                else
                {
                    CurrentAction = LincolnAction.StompPositioning;
                }
                break;
            case (LincolnAction.StompPositioning):
                int dir = 1;
                float distanceToStomp = 0.5f;
                if(Mathf.Abs(oldPos.x - targetPos.Value.x) < distanceToStomp)
                {
                    CurrentAction = LincolnAction.Stomping;
                    break;
                }

                if(targetPos.Value.x < oldPos.x)
                {
                    dir = -1;
                }
                newPosition.x += stompPositioningSpeed * dir;
                break;
            case (LincolnAction.Stomping):
                if(oldLocalPos.y > -0.2f)
                {
                    newPosition.y -= stompSpeed;
                }
                else
                {
                    CurrentAction = LincolnAction.Rising;
                }
                break;
            default:
                break;
        }

        transform.position = newPosition;

    }
}

//[CustomEditor(typeof(LincolnBossController))]
//public class TestOnInspector : Editor
//{
//    SerializedProperty A;

//    void OnEnable()
//    {
//        A = serializedObject.FindProperty("a");
//    }

//    public override void OnInspectorGUI()
//    {
//        GUILayout.Label("This is a Label in a Custom Editor");
//        EditorGUILayout.PropertyField(A);
//        serializedObject.ApplyModifiedProperties();
//    }
//}