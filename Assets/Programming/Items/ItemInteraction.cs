using UnityEngine;
using System.Collections;

public class ItemInteraction : MonoBehaviour {
	
	private float rectWidth = 100f;
	public float rectHeight = 30f;
	private Vector3 tooltipPos;
	private GameObject selected;
	
	private bool showInventory = false;
	
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonUp("Interact") && selected != null)
		{
			IsItem item = selected.GetComponent<IsItem>();
			if (!Inventory.AddItem(item.itemType))
				{
					Debug.Log("Inventory full");
					return;
				};
			Destroy(selected);
			selected = null;
		}
		
		if (Input.GetKeyUp(KeyCode.I))
		{
			showInventory = !showInventory;
		}
	}
	
	void OnGUI ()
	{
		Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 6f) && hit.collider.gameObject.GetComponent<IsItem>())
		{
			// Select object
			selected = hit.collider.gameObject;
			
			// Tooltip
			IsItem item = selected.GetComponent<IsItem>();
			CalcToolTipSize(item.itemType);
			GUI.Box(new Rect(tooltipPos.x, tooltipPos.y, rectWidth, rectHeight), item.itemType.Name);
			
		}
		
		if (showInventory)
		{
			// For each row
			for (int x = 0; x < Inventory.Bag.GetLength(0); x++)
			{
				// For each column
				for (int y = 0; y < Inventory.Bag.GetLength(1); y++)
				{
					if (Inventory.Bag[x,y] != null)
					{
						GUI.Label(
							new Rect (y * 80, x * 80, 80, 80),
							Inventory.Bag[x,y].Graphic.texture
							);
					}
				}
			}
		}
		
	}
	
	void CalcToolTipSize (Item i)
	{
		rectWidth = i.Name.Length * 9f;
		
		tooltipPos = Camera.main.ViewportToScreenPoint( new Vector3 (0.5f, 0.5f, 0));
		tooltipPos.x -= rectWidth / 2;
		tooltipPos.y -= rectHeight / 2;
	}
}
