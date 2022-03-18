﻿using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SKCell
{
    public class SKSpriteTools : EditorWindow
    {
        private static Color color = new Color(1,1,1,0);
        private static Texture2D texture;
        private static string texturePath;

        [MenuItem("SKCell/Sprite Colorer")]
        public static void Initialize()
        {
            GetWindow<SKSpriteTools>("Sprite Colorer");
        }
        private void OnGUI()
        {
            color = EditorGUILayout.ColorField(color);
            texture = EditorGUILayout.ObjectField(texture, typeof(Texture2D), false) as Texture2D;
            texturePath = AssetDatabase.GetAssetPath(texture);

            if(GUILayout.Button("Assign Color"))
            {
                AssignColor();
            }
        }
        public static void AssignColor()
        {
            Color c;
            for (int i = 0; i < texture.width; i++)
            {
                for (int j = 0; j < texture.height; j++)
                {
                     c = texture.GetPixel(i, j);
                    if(c.a>0.1f)
                        texture.SetPixel(i, j, color);
                }
            }
            texture.Apply();

            byte[] itemBGBytes = texture.EncodeToPNG();
            File.WriteAllBytes(texturePath, itemBGBytes);
        }
    }
}
