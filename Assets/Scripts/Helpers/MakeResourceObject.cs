using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Create item object
/// </summary>
public class MakeResourceObject {

    [MenuItem("Assets/Create/Resource object")]
    public static void Create()
    {
        ResourceObject asset = ScriptableObject.CreateInstance<ResourceObject>();
        AssetDatabase.CreateAsset(asset, "Assets/NewResourceObject.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
