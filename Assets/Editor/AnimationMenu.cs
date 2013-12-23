using UnityEngine;
using UnityEditor;
using System.Collections;

public class AnimationMenu : MonoBehaviour {

[MenuItem("Animations/Create Animation")]
	public static void CreateAnimation ()
	{
		SpriteAnimation asset = SpriteAnimation.CreateInstance<SpriteAnimation>();
		AssetDatabase.CreateAsset(asset, "Assets/Animations/New Animation.asset");
		AssetDatabase.SaveAssets();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}
}
