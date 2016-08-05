using UnityEngine;
using UnityEditor;
public class sinarioMaker : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    // Add menu named "My Window" to the Window menu
    [MenuItem("sinario/maker")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        sinarioMaker window = (sinarioMaker)EditorWindow.GetWindow(typeof(sinarioMaker));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
    }



    /// <summary>
    /// OnGUI 사례!
    /// </summary>

    /*
    void OnGUI()
    {
        if (mLastAtlas != NGUISettings.atlas)
            mLastAtlas = NGUISettings.atlas;

        bool update = false;
        bool replace = false;

        NGUIEditorTools.SetLabelWidth(84f);
        GUILayout.Space(3f);

        NGUIEditorTools.DrawHeader("Input", true);
        NGUIEditorTools.BeginContents(false);

        GUILayout.BeginHorizontal();
        {
            ComponentSelector.Draw<UIAtlas>("Atlas", NGUISettings.atlas, OnSelectAtlas, true, GUILayout.MinWidth(80f));

            EditorGUI.BeginDisabledGroup(NGUISettings.atlas == null);
            if (GUILayout.Button("New", GUILayout.Width(40f)))
                NGUISettings.atlas = null;
            EditorGUI.EndDisabledGroup();
        }
        GUILayout.EndHorizontal();

        List<Texture> textures = GetSelectedTextures();

        if (NGUISettings.atlas != null)
        {
            Material mat = NGUISettings.atlas.spriteMaterial;
            Texture tex = NGUISettings.atlas.texture;

            // Material information
            GUILayout.BeginHorizontal();
            {
                if (mat != null)
                {
                    if (GUILayout.Button("Material", GUILayout.Width(76f))) Selection.activeObject = mat;
                    GUILayout.Label(" " + mat.name);
                }
                else
                {
                    GUI.color = Color.grey;
                    GUILayout.Button("Material", GUILayout.Width(76f));
                    GUI.color = Color.white;
                    GUILayout.Label(" N/A");
                }
            }
            GUILayout.EndHorizontal();

            // Texture atlas information
            GUILayout.BeginHorizontal();
            {
                if (tex != null)
                {
                    if (GUILayout.Button("Texture", GUILayout.Width(76f))) Selection.activeObject = tex;
                    GUILayout.Label(" " + tex.width + "x" + tex.height);
                }
                else
                {
                    GUI.color = Color.grey;
                    GUILayout.Button("Texture", GUILayout.Width(76f));
                    GUI.color = Color.white;
                    GUILayout.Label(" N/A");
                }
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.BeginHorizontal();
        NGUISettings.atlasPadding = Mathf.Clamp(EditorGUILayout.IntField("Padding", NGUISettings.atlasPadding, GUILayout.Width(100f)), 0, 8);
        GUILayout.Label((NGUISettings.atlasPadding == 1 ? "pixel" : "pixels") + " between sprites");
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        NGUISettings.atlasTrimming = EditorGUILayout.Toggle("Trim Alpha", NGUISettings.atlasTrimming, GUILayout.Width(100f));
        GUILayout.Label("Remove empty space");
        GUILayout.EndHorizontal();

        bool fixedShader = false;

        if (NGUISettings.atlas != null)
        {
            Material mat = NGUISettings.atlas.spriteMaterial;

            if (mat != null)
            {
                Shader shader = mat.shader;

                if (shader != null)
                {
                    if (shader.name == "Unlit/Transparent Colored")
                    {
                        NGUISettings.atlasPMA = false;
                        fixedShader = true;
                    }
                    else if (shader.name == "Unlit/Premultiplied Colored")
                    {
                        NGUISettings.atlasPMA = true;
                        fixedShader = true;
                    }
                }
            }
        }

        if (!fixedShader)
        {
            GUILayout.BeginHorizontal();
            NGUISettings.atlasPMA = EditorGUILayout.Toggle("PMA Shader", NGUISettings.atlasPMA, GUILayout.Width(100f));
            GUILayout.Label("Pre-multiplied alpha", GUILayout.MinWidth(70f));
            GUILayout.EndHorizontal();
        }

        //GUILayout.BeginHorizontal();
        //NGUISettings.keepPadding = EditorGUILayout.Toggle("Keep Padding", NGUISettings.keepPadding, GUILayout.Width(100f));
        //GUILayout.Label("or replace with trimmed pixels", GUILayout.MinWidth(70f));
        //GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        NGUISettings.unityPacking = EditorGUILayout.Toggle("Unity Packer", NGUISettings.unityPacking, GUILayout.Width(100f));
        GUILayout.Label("or custom packer", GUILayout.MinWidth(70f));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        NGUISettings.trueColorAtlas = EditorGUILayout.Toggle("Truecolor", NGUISettings.trueColorAtlas, GUILayout.Width(100f));
        GUILayout.Label("force ARGB32 textures", GUILayout.MinWidth(70f));
        GUILayout.EndHorizontal();

        if (!NGUISettings.unityPacking)
        {
            GUILayout.BeginHorizontal();
            NGUISettings.forceSquareAtlas = EditorGUILayout.Toggle("Force Square", NGUISettings.forceSquareAtlas, GUILayout.Width(100f));
            GUILayout.Label("if on, forces a square atlas texture", GUILayout.MinWidth(70f));
            GUILayout.EndHorizontal();
        }

#if UNITY_IPHONE || UNITY_ANDROID
		GUILayout.BeginHorizontal();
		NGUISettings.allow4096 = EditorGUILayout.Toggle("4096x4096", NGUISettings.allow4096, GUILayout.Width(100f));
		GUILayout.Label("if off, limit atlases to 2048x2048");
		GUILayout.EndHorizontal();
#endif
        NGUIEditorTools.EndContents();

        if (NGUISettings.atlas != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(20f);

            if (textures.Count > 0)
            {
                update = GUILayout.Button("Add/Update");
            }
            else if (GUILayout.Button("View Sprites"))
            {
                SpriteSelector.ShowSelected();
            }

            GUILayout.Space(20f);
            GUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.HelpBox("You can create a new atlas by selecting one or more textures in the Project View window, then clicking \"Create\".", MessageType.Info);

            EditorGUI.BeginDisabledGroup(textures.Count == 0);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20f);
            bool create = GUILayout.Button("Create");
            GUILayout.Space(20f);
            GUILayout.EndHorizontal();
            EditorGUI.EndDisabledGroup();

            if (create)
            {
                string path = EditorUtility.SaveFilePanelInProject("Save As",
                    "New Atlas.prefab", "prefab", "Save atlas as...", NGUISettings.currentPath);

                if (!string.IsNullOrEmpty(path))
                {
                    NGUISettings.currentPath = System.IO.Path.GetDirectoryName(path);
                    GameObject go = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
                    string matPath = path.Replace(".prefab", ".mat");
                    replace = true;

                    // Try to load the material
                    Material mat = AssetDatabase.LoadAssetAtPath(matPath, typeof(Material)) as Material;

                    // If the material doesn't exist, create it
                    if (mat == null)
                    {
                        Shader shader = Shader.Find(NGUISettings.atlasPMA ? "Unlit/Premultiplied Colored" : "Unlit/Transparent Colored");
                        mat = new Material(shader);

                        // Save the material
                        AssetDatabase.CreateAsset(mat, matPath);
                        AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);

                        // Load the material so it's usable
                        mat = AssetDatabase.LoadAssetAtPath(matPath, typeof(Material)) as Material;
                    }

                    // Create a new prefab for the atlas
                    Object prefab = (go != null) ? go : PrefabUtility.CreateEmptyPrefab(path);

                    // Create a new game object for the atlas
                    string atlasName = path.Replace(".prefab", "");
                    atlasName = atlasName.Substring(path.LastIndexOfAny(new char[] { '/', '\\' }) + 1);
                    go = new GameObject(atlasName);
                    go.AddComponent<UIAtlas>().spriteMaterial = mat;

                    // Update the prefab
                    PrefabUtility.ReplacePrefab(go, prefab);
                    DestroyImmediate(go);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);

                    // Select the atlas
                    go = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
                    NGUISettings.atlas = go.GetComponent<UIAtlas>();
                    Selection.activeGameObject = go;
                }
            }
        }

        string selection = null;
        Dictionary<string, int> spriteList = GetSpriteList(textures);

        if (spriteList.Count > 0)
        {
            NGUIEditorTools.DrawHeader("Sprites", true);
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(3f);
                GUILayout.BeginVertical();

                mScroll = GUILayout.BeginScrollView(mScroll);

                bool delete = false;
                int index = 0;
                foreach (KeyValuePair<string, int> iter in spriteList)
                {
                    ++index;

                    GUILayout.Space(-1f);
                    bool highlight = (UIAtlasInspector.instance != null) && (NGUISettings.selectedSprite == iter.Key);
                    GUI.backgroundColor = highlight ? Color.white : new Color(0.8f, 0.8f, 0.8f);
                    GUILayout.BeginHorizontal("AS TextArea", GUILayout.MinHeight(20f));
                    GUI.backgroundColor = Color.white;
                    GUILayout.Label(index.ToString(), GUILayout.Width(24f));

                    if (GUILayout.Button(iter.Key, "OL TextField", GUILayout.Height(20f)))
                        selection = iter.Key;

                    if (iter.Value == 2)
                    {
                        GUI.color = Color.green;
                        GUILayout.Label("Add", GUILayout.Width(27f));
                        GUI.color = Color.white;
                    }
                    else if (iter.Value == 1)
                    {
                        GUI.color = Color.cyan;
                        GUILayout.Label("Update", GUILayout.Width(45f));
                        GUI.color = Color.white;
                    }
                    else
                    {
                        if (mDelNames.Contains(iter.Key))
                        {
                            GUI.backgroundColor = Color.red;

                            if (GUILayout.Button("Delete", GUILayout.Width(60f)))
                            {
                                delete = true;
                            }
                            GUI.backgroundColor = Color.green;
                            if (GUILayout.Button("X", GUILayout.Width(22f)))
                            {
                                mDelNames.Remove(iter.Key);
                                delete = false;
                            }
                            GUI.backgroundColor = Color.white;
                        }
                        else
                        {
                            // If we have not yet selected a sprite for deletion, show a small "X" button
                            if (GUILayout.Button("X", GUILayout.Width(22f))) mDelNames.Add(iter.Key);
                        }
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndScrollView();
                GUILayout.EndVertical();
                GUILayout.Space(3f);
                GUILayout.EndHorizontal();

                // If this sprite was marked for deletion, remove it from the atlas
                if (delete)
                {
                    List<SpriteEntry> sprites = new List<SpriteEntry>();
                    ExtractSprites(NGUISettings.atlas, sprites);

                    for (int i = sprites.Count; i > 0; )
                    {
                        SpriteEntry ent = sprites[--i];
                        if (mDelNames.Contains(ent.name))
                            sprites.RemoveAt(i);
                    }
                    UpdateAtlas(NGUISettings.atlas, sprites);
                    mDelNames.Clear();
                    NGUIEditorTools.RepaintSprites();
                }
                else if (update) UpdateAtlas(textures, true);
                else if (replace) UpdateAtlas(textures, false);

                if (NGUISettings.atlas != null && !string.IsNullOrEmpty(selection))
                {
                    NGUIEditorTools.SelectSprite(selection);
                }
                else if (update || replace)
                {
                    NGUIEditorTools.UpgradeTexturesToSprites(NGUISettings.atlas);
                    NGUIEditorTools.RepaintSprites();
                }
            }
        }

        if (NGUISettings.atlas != null && textures.Count == 0)
            EditorGUILayout.HelpBox("You can reveal more options by selecting one or more textures in the Project View window.", MessageType.Info);

        // Uncomment this line if you want to be able to force-sort the atlas
        //if (NGUISettings.atlas != null && GUILayout.Button("Sort Alphabetically")) NGUISettings.atlas.SortAlphabetically();
    }



    
     */
}