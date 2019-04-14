//using UnityEngine;
//using UnityEditor;

//public class TestEditor1 : ScriptableObject
//{
//    [CustomEditor(typeof(EditorScriptTest1))]
//    [CanEditMultipleObjects]
//    public class EditorScriptTest1Editor : Editor
//    {
//        SerializedProperty lookAtPoint;
//        SerializedProperty prefab;
//        SerializedProperty sprite;

//        void OnEnable()
//        {
//            lookAtPoint = serializedObject.FindProperty("lookAtPoint");
//            prefab = serializedObject.FindProperty("prefab");
            
//             sprite = serializedObject.FindProperty("sprite");
//        }

//        public override void OnInspectorGUI()
//        {
//            serializedObject.Update();
//            EditorGUILayout.PropertyField(lookAtPoint);
//            EditorGUILayout.PropertyField(prefab);
//            EditorGUILayout.ObjectField(sprite.objectReferenceValue, typeof(Sprite), false);
//            if (lookAtPoint.vector3Value.y > (target as EditorScriptTest1).transform.position.y)
//            {
//                EditorGUILayout.LabelField("(Above this object)");
//            }
//            if (lookAtPoint.vector3Value.y < (target as EditorScriptTest1).transform.position.y)
//            {
//                EditorGUILayout.LabelField("(Below this object)");
//            }


//            serializedObject.ApplyModifiedProperties();
//        }

//        public void OnSceneGUI()
//        {
//            var t = (target as EditorScriptTest1);
//            Handles.BeginGUI();
//            if (GUILayout.Button("Press Me"))
//                Debug.Log("Got it to work.");
//            EditorGUI.DrawRect(new Rect(2, 2, 5, 5), Color.red);
//            //GUI.DrawTexture(new Rect(2, 2, 5, 5), (sprite.objectReferenceValue as Texture2D), ScaleMode.ScaleToFit);

//            Handles.EndGUI();
//            EditorGUI.BeginChangeCheck();
//            Vector3 pos = Handles.PositionHandle(t.lookAtPoint, Quaternion.identity);
//            // Handles.      

//            if (EditorGUI.EndChangeCheck())
//            {
//                Undo.RecordObject(target, "Move point");
//                t.lookAtPoint = pos;
//                t.Update();
//               // t.createTest();
//            }
//        }
//    }
//}