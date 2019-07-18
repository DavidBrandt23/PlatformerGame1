using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FinalBossController : EnemyCommonController
{
    private OverlordAction CurrentAction;
    private float riseHeight = 8.0f;
    private float riseSpeed = 0.08f;
    private float stompPositioningSpeed = 0.08f;
    private float stompSpeed = 0.2f;
    private const float moveSpeed = 0.2f;
    public Vector3Variable targetPos;

    public GameObject leftPoint, leftAttack, midPoint, midAttack, rightPoint, rightAttack;
    public GameObject attackParent;

    private bool attackedThisPhase;

    private enum OverlordAction
    {
        Left,
        Mid, //go l next
        MidR, //go r next
        Right
    };

    protected override void Start()
    {
        base.Start();
        CurrentAction = OverlordAction.Right; //actual first will be mid left
        switchPhase();
        InvokeRepeating("switchPhase", 6.0f, 6.0f);
        m_HealthScript.onDeathDelegate += onDeath;
    }

    private void onDeath()
    {
        Destroy(attackParent);
       // Destroy(this.gameObject);
    }
    
    private void switchPhase()
    {
        if (m_HealthScript.IsDead())
        {
            return;
        }
        OverlordAction nextPhase = getNextPhase(CurrentAction);
        CurrentAction = nextPhase;

        GameObject attackOb = getAttackOb(CurrentAction);
        GameObject newAttack = Instantiate(attackOb, attackOb.GetComponentInParent<Transform>());
        newAttack.SetActive(true);
    }

    private GameObject getAttackOb(OverlordAction curAction)
    {
        switch (curAction)
        {
            case (OverlordAction.Mid):
            case (OverlordAction.MidR):
                return midAttack;
            case (OverlordAction.Left):
                return leftAttack;
            case (OverlordAction.Right):
                return rightAttack;
        }
        return null;
    }
    private OverlordAction getNextPhase(OverlordAction curAction)
    {
        switch (curAction)
        {
            case (OverlordAction.Mid):
                return OverlordAction.Left;
            case (OverlordAction.Left):
                return OverlordAction.MidR;
            case (OverlordAction.MidR):
                return OverlordAction.Right;
            case (OverlordAction.Right):
                return OverlordAction.Mid;
        }
        return OverlordAction.Mid;
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = transform.position;
        Vector3 oldPos = transform.position;
        Vector3 oldLocalPos = transform.localPosition;

        Vector3 moveTarget = new Vector3();
        switch (CurrentAction) { 
            case (OverlordAction.Mid):
            case (OverlordAction.MidR):
                moveTarget = midPoint.transform.position;
                break;
            case (OverlordAction.Left):
                moveTarget = leftPoint.transform.position;
                break;
            case (OverlordAction.Right):
                moveTarget = rightPoint.transform.position;
                   break;
            default:
                    break;
        }
        MoveTowardPoint(moveTarget);

    }
    private void MoveTowardPoint(Vector3 endPoint)
    {
        Vector3 curPoint = transform.position;
        Vector3 dirToEnd = (endPoint - curPoint).normalized;
        float distanceToEnd = (endPoint - curPoint).magnitude;
        float speed = moveSpeed;
        if(distanceToEnd < speed)
        {
            speed = distanceToEnd;
        }
        Vector3 velocity = dirToEnd * speed;

        Vector3 oldPos = transform.position;
        Vector2 newPos = new Vector3(oldPos.x + velocity.x, oldPos.y + velocity.y, oldPos.z);
        transform.position = newPos;
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