using UnityEngine;
using System.Collections;

public class Item : ScriptableObject
{
	public int itemID = 0;
	public string Name;
	public int Amount = 1;
	public Sprite Graphic;
	public Vector3 Scale = new Vector3(1F,1F,1F);
	public bool billboard = true;
	public Material SpriteMaterial;
		
	public static Item genItem ()
	{
		Item i = new Item();
		i.itemID = ItemDatabase.GenerateID();
		return i;
	}

	public void Unload()
	{
		Resources.UnloadAsset(Graphic);
	}
}