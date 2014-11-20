using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public class AssetBundleExporter : EditorWindow
{

    public string path = "Not Set";
    public string bundleName = "Not Set";
    public bool collectDependencies = false;

    [MenuItem("Window/AssetBundleHelper")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(AssetBundleExporter));
    }


	// Use this for initialization
	void Start () {
	    
	}

    void OnGUI()
    {
        GUILayout.Label("Asset Bundle Helper:Created By @Kinifi");
        GUILayout.Space(10);
        GUILayout.Label("Select A Location to Save The Bundle");
        GUILayout.Label(path);

        if(GUILayout.Button("Path"))
        {
            path = EditorUtility.OpenFolderPanel("Load png Textures of Directory", "", "");
        }

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Bundle Name:");
        bundleName = GUILayout.TextField(bundleName, 20);
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        GUILayout.TextArea("Select the assets you want to be in the bundle from the Project Window, at the same time. This Tool will automatically collect Dependencies");
        GUILayout.Space(10);

        collectDependencies = GUILayout.Toggle(collectDependencies, "Collect Dependencies?");

        GUILayout.Space(10);
        if(GUILayout.Button("Export", GUILayout.Height(30)))
        {
            string finalPath = path + "/" + bundleName + ".unity3d";
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            if (collectDependencies)
            {
                BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, finalPath, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
            }
            else
            {
                BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, finalPath, BuildAssetBundleOptions.None);
            }
            AssetDatabase.Refresh();
        }


    }


}
