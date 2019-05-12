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
        public bool flipX;

        public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
        {
            // return tileData;
            //tileData.transform = Matrix4x4.identity;
            //tileData.color = Color.white;
            //if (m_AnimatedSprites != null && m_AnimatedSprites.Length > 0)
            //{
             if (flipX)
             {
             var m = tileData.transform;
             m.SetTRS(Vector3.zero, Quaternion.Euler(0.0f, -180f, 0.0f), Vector3.one);
            tileData.transform = m;
            }
             tileData.sprite = m_Sprite;
            tileData.flags = TileFlags.LockTransform;
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
            tile.flipX = EditorGUILayout.Toggle("FlipX", tile.flipX);
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(tile);
        }
        //public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        //{
        //    return textureFromSprite(tile.m_Sprite);
        //}
        private Texture2D textureFromSprite(Sprite sprite)
        {
            if (sprite == null)
            {
                return null;
            }
            if (sprite.rect.width != sprite.texture.width)
            {
                Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
                Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                             (int)sprite.textureRect.y,
                                                             (int)sprite.textureRect.width,
                                                             (int)sprite.textureRect.height);
                if (tile.flipX)
                {
                    int s = (int)(sprite.rect.height * sprite.rect.width);
                    int w = (int)sprite.rect.width;
                    int h = (int)sprite.rect.height;
                    Color[] newColors2 = new Color[s];
                    int k = 0;
                    for (int j = w-1; j <= (w*h-1); j+=w)
                    {
                        for (int i = 0; i <=(w-1); i++)
                        {
                            newColors2[k] = newColors[j - i];
                            k++;
                        }
                    }
                    newColors = newColors2;

                }
                newText.SetPixels(newColors);
                newText.Apply();
                return newText;
            }
            else
                return sprite.texture;
        }
    }
#endif
}
