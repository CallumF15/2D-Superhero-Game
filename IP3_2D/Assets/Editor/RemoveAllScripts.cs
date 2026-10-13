using UnityEditor;
using UnityEngine;

public static class RemoveAllScripts
{
    [MenuItem("Tools/Remove All Scripts From Prefab")]
    private static void Remove()
    {
        GameObject prefab = Selection.activeObject as GameObject;

        if (prefab == null)
        {
            Debug.LogWarning("Select a prefab in the Project window.");
            return;
        }

        string path = AssetDatabase.GetAssetPath(prefab);

        if (!path.EndsWith(".prefab"))
        {
            Debug.LogWarning("Selected object is not a prefab.");
            return;
        }

        GameObject instance = PrefabUtility.LoadPrefabContents(path);

        MonoBehaviour[] components = instance.GetComponentsInChildren<MonoBehaviour>(true);

        foreach (MonoBehaviour component in components)
        {
            Object.DestroyImmediate(component);
        }

        PrefabUtility.SaveAsPrefabAsset(instance, path);
        PrefabUtility.UnloadPrefabContents(instance);

        Debug.Log("Removed all scripts from prefab: " + prefab.name);
    }
}