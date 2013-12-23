using UnityEngine;
using System.Collections;

public class Inventory
{
	
	public static Item[,] Bag = new Item[4,6];
	
	public static Item[] Hotbar = new Item [6];
	
	public static bool AddItem (Item item)
	{
		// For each row
		for (int x = 0; x < Bag.GetLength(0); x++)
		{
			// For each column
			for (int y = 0; y < Bag.GetLength(1); y++)
			{
				// If we find an empty space, we add the item and return
				if (Bag[x,y] == null)
				{
					Debug.Log("Adding " + item.Name + " to inventory. " + x + " " + y);
					Bag[x,y] = item;
					return true;
				}
			}
		}
		// Or we can't
		return false;
	}
	
}
