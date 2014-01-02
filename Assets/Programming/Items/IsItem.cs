using UnityEngine;
using System.Collections;

public class IsItem : MonoBehaviour {
	
	public Item itemType;
	
	public IsItem(Item item)
	{
		itemType = item;
	}
	
	
	// Use this for initialization
	void Awake ()
	{
		CreateObject();
	}
	void OnValidate ()
	{
		CreateObject();
	}
	void CreateObject ()
	{
		// Are we a weapon?
		if (itemType.GetType() == typeof(Weapon))
		{
			Weapon weapon = (Weapon)itemType;
			// Do we have a mesh collider?
			if (GetComponent<MeshCollider>())
			{
				GetComponent<MeshCollider>().sharedMesh = weapon.meshCollider;
			} else {
				gameObject.AddComponent<MeshCollider>();
				GetComponent<MeshCollider>().sharedMesh = weapon.meshCollider;
			}
		} else {
			GetComponent<MeshCollider>().sharedMesh = Resources.GetBuiltinResource(typeof(Mesh),"Cube.fbx") as Mesh;
		}
		
		// Get the Renderer
		SpriteRenderer r = GetComponentInChildren<SpriteRenderer>();
		if (itemType == null || r == null)
			return;
		r.sprite = itemType.Graphic;
		// Set specular or diffuse	
		r.sharedMaterial = itemType.SpriteMaterial;
		
		// Scale the sprite
		r.gameObject.transform.localScale = itemType.Scale;
		
		// Handle billboarding
		BillBoard bb = r.gameObject.GetComponent<BillBoard>();
		bb.enabled = itemType.billboard;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
