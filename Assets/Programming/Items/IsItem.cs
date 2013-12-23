using UnityEngine;
using System.Collections;

public class IsItem : MonoBehaviour {
	
	public Item itemType;	
	private bool instancedMat = false;
	
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
		MeshRenderer r = GetComponentInChildren<MeshRenderer>();
		if (itemType == null || r == null)
			return;
		Material mat = r.sharedMaterial;
		// Insantiate new material
		if(!instancedMat)
		{
			mat = new Material(r.sharedMaterial);
			instancedMat = true;
		}
		mat.mainTexture = itemType.Graphic;
		// Set specular or diffuse
		if(itemType.Specular)
		{
			mat.shader = Shader.Find("Transparent/Cutout/Specular");
		} else {
			mat.shader = Shader.Find("Transparent/Cutout/Diffuse");
		}		
		r.sharedMaterial = mat;
		
		// Scale the sprite
		this.transform.localScale = itemType.Scale;
		
		// Handle billboarding
		BillBoard bb = r.gameObject.GetComponent<BillBoard>();
		bb.enabled = itemType.billboard;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
