using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpritePivotAdjustment : EditorWindow
{
    public Texture texture;
    public SpriteAlignment pivotType;
    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/SpritePivotAdjustment")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        SpritePivotAdjustment window = (SpritePivotAdjustment)EditorWindow.GetWindow(typeof(SpritePivotAdjustment));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Sprite");
        texture = (Texture)EditorGUILayout.ObjectField(texture, typeof(Texture), true);
        GUILayout.EndHorizontal();
        pivotType = (SpriteAlignment)EditorGUILayout.EnumPopup("SpriteAlignment:", pivotType);
        if (GUILayout.Button("Set Pivot") && texture != null)
        {
            SetSpritePivot(texture);
            Debug.Log("Set Pivot to " + pivotType);
        }
        GUILayout.EndVertical();
    }

    private void SetSpritePivot(Texture texture)
    {
        string path = AssetDatabase.GetAssetPath(texture);
        TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

        //do this becuase unity has some buggies.
        textureImporter.isReadable = false;
        textureImporter.isReadable = true;
        //

        List<SpriteMetaData> newData = new List<SpriteMetaData>();

        for (int i = 0; i < textureImporter.spritesheet.Length; i++)
        {
            SpriteMetaData spriteData = textureImporter.spritesheet[i];
            spriteData.alignment = 9;
            spriteData.pivot = ConvertAlignmentToVector2(pivotType);
            newData.Add(spriteData);
        }
        textureImporter.spritesheet = newData.ToArray();
        EditorUtility.SetDirty(texture);
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
    }

    private Vector2 ConvertAlignmentToVector2(SpriteAlignment alignment)
    {
        switch (alignment)
        {
            case SpriteAlignment.TopLeft:
                return new Vector2(0, 1);
            case SpriteAlignment.TopCenter:
                return new Vector2(0.5f, 1);
            case SpriteAlignment.TopRight:
                return new Vector2(1, 1);
            case SpriteAlignment.LeftCenter:
                return new Vector2(0, 0.5f);
            case SpriteAlignment.Center:
                return new Vector2(0.5f, 0.5f);
            case SpriteAlignment.RightCenter:
                return new Vector2(1, 0.5f);
            case SpriteAlignment.BottomLeft:
                return new Vector2(0, 0);
            case SpriteAlignment.BottomCenter:
                return new Vector2(0.5f, 0);
            case SpriteAlignment.BottomRight:
                return new Vector2(1, 0);
            default:
                return new Vector2(0.5f, 0.5f);
        }
    }
}
