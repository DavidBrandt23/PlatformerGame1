using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace UnityEngine.Tilemaps
{
    [Serializable]
    public class LevelTile : TileBase
    {
        public Sprite m_Sprite;
        public float leftY = 1.0f;
        public float rightY = 1.0f;

        public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
        {
           // return tileData;
            //tileData.transform = Matrix4x4.identity;
            //tileData.color = Color.white;
            //if (m_AnimatedSprites != null && m_AnimatedSprites.Length > 0)
            //{
             tileData.sprite = m_Sprite;
            //    tileData.colliderType = m_TileColliderType;
            //}
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/DABLevel Tile")]
        public static void CreateAnimatedTile()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Level Tile", "New Level Tile", "asset", "Save Level Tile", "Assets");
            if (path == "")
                return;

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<LevelTile>(), path);
        }
#endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(LevelTile))]
    public class AnimatedTileEditor : Editor
    {
        private LevelTile tile { get { return (target as LevelTile); } }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            tile.m_Sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", tile.m_Sprite, typeof(Sprite), false, null);
            tile.leftY = EditorGUILayout.FloatField("Left Y", tile.leftY);
            tile.rightY = EditorGUILayout.FloatField("Right Y", tile.rightY);
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(tile);
        }
    }
#endif
}
