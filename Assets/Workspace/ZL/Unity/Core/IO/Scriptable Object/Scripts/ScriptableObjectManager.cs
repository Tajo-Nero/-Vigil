#if UNITY_EDITOR

using System.IO;

using UnityEditor;

using UnityEngine;

namespace ZL.Unity.IO
{
    public static partial class ScriptableObjectManager
    {
        public static TScriptableObject Load<TScriptableObject>(string directoryPath, string assetPath)

            where TScriptableObject : ScriptableObject
        {
            var asset = AssetDatabase.LoadAssetAtPath<TScriptableObject>(assetPath);

            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<TScriptableObject>();

                if (Directory.Exists(directoryPath) == false)
                {
                    Directory.CreateDirectory(directoryPath);
                }

                AssetDatabase.CreateAsset(asset, assetPath);

                AssetDatabase.SaveAssets();

                AssetDatabase.Refresh();
            }

            return asset;
        }
    }
}

#endif