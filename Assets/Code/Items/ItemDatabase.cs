using UnityEngine;
using System.Collections;

public class ItemDatabase  {
	
	public static Item GetItem(int id)
	{
		Item[] db = Resources.LoadAll("Items/") as Item[];
		
		foreach (Item i in db)
		{
			if (i.itemID == id)
			{
				return i;
			} 
		}
		return null;
	}
	
	public static int GenerateID()
	{
		Object[] db = Resources.LoadAll("Items/", typeof(Item));
		return db.Length + 1;
	}
	
	public static GameObject DropItem(Item item)
	{
		GameObject nu =  Resources.Load("Objects/ItemObject") as GameObject;
		IsItem i = nu.GetComponent<IsItem>();
		i.itemType = item;
		return nu;
	}
}

