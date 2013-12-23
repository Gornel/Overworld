using UnityEngine;
using UnityEditor;
using System.Collections;

public class ItemMenu : MonoBehaviour {

[MenuItem("Items/Create Item")]
	public static void CreateItem()
	{
		Item asset = Item.genItem();
		AssetDatabase.CreateAsset(asset, "Assets/Resources/Items/New Item.asset");
		AssetDatabase.SaveAssets();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}
[MenuItem("Items/Create Weapon")]
	public static void CreateWeapon()
	{
		Weapon asset = Weapon.genItem();
		AssetDatabase.CreateAsset(asset, "Assets/Resources/Items/New Weapon.asset");
		AssetDatabase.SaveAssets();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}
}
