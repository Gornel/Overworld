//
// Author:
//   Andreas Suter (andy@edelweissinteractive.com)
//
// Copyright (C) 2013 Edelweiss Interactive (http://edelweissinteractive.com)
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Edelweiss.DecalSystem;

public class BulletExampleDynamicObjectCS : MonoBehaviour {

		// The prefab which contains the DS_Decals script with already set material and
		// uv rectangles.
	public GameObject decalsPrefab;
	
		// The reference to the instantiated prefab's DS_Decals instance.
	private DS_Decals m_Decals;

		// All the projectors that were created at runtime.
	private List <DecalProjector> m_DecalProjectors = new List <DecalProjector> ();
	
		// Intermediate mesh data. Mesh data is added to that one for a specific projector
		// in order to perform the cutting.
	private DecalsMesh m_DecalsMesh;
	private DecalsMeshCutter m_DecalsMeshCutter;
	
		// The raycast hits a collider at a certain position. This value indicated how far we need to
		// go back from that hit point along the ray of the raycast to place the new decal projector. Set
		// this value to 0.0f to see why this is needed.
	public float decalProjectorOffset = 0.5f;
	
		// The size of new decal projectors.
	public Vector3 decalScale = new Vector3 (0.2f, 2.0f, 0.2f);
	public float cullingAngle = 180.0f;
	public float meshOffset = 0.001f;
	
		// We iterate through all the defined uv rectangles. This one indices which index we are using at
		// the moment.
	private int m_UVRectangleIndex = 0;

		// Move on to the next uv rectangle index.
	private void NextUVRectangleIndex () {
		m_UVRectangleIndex = m_UVRectangleIndex + 1;
		if (m_UVRectangleIndex >= m_Decals.uvRectangles.Length) {
			m_UVRectangleIndex = 0;
		}
	}
	
	private void Start () {
	
			// Instantiate the prefab and get its decals instance.
		GameObject l_Instance = UnityEngine.Object.Instantiate (decalsPrefab) as GameObject;
		m_Decals = l_Instance.GetComponentInChildren <DS_Decals> ();
		
			// Make sure the decals move with this dynamic object.
		m_Decals.transform.parent = transform;
		m_Decals.transform.localPosition = Vector3.zero;
		m_Decals.transform.localScale = Vector3.one;
		
		if (m_Decals == null) {
			Debug.LogError ("The 'decalsPrefab' does not contain a 'DS_Decals' instance!");
		} else {
		
				// Create the decals mesh (intermediate mesh data) for our decals instance.
				// Further we need a decals mesh cutter instance and the world to decals matrix.
			m_DecalsMesh = new DecalsMesh (m_Decals);
			m_DecalsMeshCutter = new DecalsMeshCutter ();
		}
	}

	public void AddDecalProjector (Ray a_Ray, RaycastHit a_RaycastHit) {
			// Make sure there are not too many projectors.
		if (m_DecalProjectors.Count >= 50) {
		
				// If there are more than 50 projectors, we remove the first one from
				// our list and certainly from the decals mesh (the intermediate mesh
				// format). All the mesh data that belongs to this projector will
				// be removed.
			DecalProjector l_DecalProjectorForRemoval = m_DecalProjectors [0];
			m_DecalProjectors.RemoveAt (0);
			m_DecalsMesh.RemoveProjector (l_DecalProjectorForRemoval);
		}
		
			// Calculate the position and rotation for the new decal projector.
		Vector3 l_ProjectorPosition = a_RaycastHit.point - (decalProjectorOffset * a_Ray.direction.normalized);
		Quaternion l_ProjectorRotation = ProjectorRotationUtility.ProjectorRotation (Camera.main.transform.forward, Vector3.up);

			// Randomize the rotation.
		Quaternion l_RandomRotation = Quaternion.Euler (0.0f, Random.Range (0.0f, 360.0f), 0.0f);
		l_ProjectorRotation = l_ProjectorRotation * l_RandomRotation;		
		
		
			// We hit a collider. Next we have to find the mesh that belongs to the collider.
			// That step depends on how you set up your mesh filters and collider relative to
			// each other in the game objects. It is important to have a consistent way in order
			// to have a simpler implementation.
		
		MeshCollider l_MeshCollider = a_RaycastHit.collider.GetComponent <MeshCollider> ();
		MeshFilter l_MeshFilter = a_RaycastHit.collider.GetComponent <MeshFilter> ();
		
		if (l_MeshCollider != null || l_MeshFilter != null) {
			Mesh l_Mesh = null;
			if (l_MeshCollider != null) {
			
					// Mesh collider was hit. Just use the mesh data from that one.
				l_Mesh = l_MeshCollider.sharedMesh;
			} else if (l_MeshFilter != null) {
			
					// Otherwise take the data from the shared mesh.
				l_Mesh = l_MeshFilter.sharedMesh;
			}
			
			if (l_Mesh != null) {
			
					// Create the decal projector.
				DecalProjector l_DecalProjector = new DecalProjector (l_ProjectorPosition, l_ProjectorRotation, decalScale, cullingAngle, meshOffset, m_UVRectangleIndex, m_UVRectangleIndex);
				
					// Add the projector to our list and the decals mesh, such that both are
					// synchronized. All the mesh data that is now added to the decals mesh
					// will belong to this projector.
				m_DecalProjectors.Add (l_DecalProjector);
				m_DecalsMesh.AddProjector (l_DecalProjector);
				
					// Get the required matrices.
				Matrix4x4 l_WorldToMeshMatrix = a_RaycastHit.collider.renderer.transform.worldToLocalMatrix;
				Matrix4x4 l_MeshToWorldMatrix = a_RaycastHit.collider.renderer.transform.localToWorldMatrix;
				
					// Add the mesh data to the decals mesh, cut and offset it before we pass it
					// to the decals instance to be displayed.
				m_DecalsMesh.Add (l_Mesh, l_WorldToMeshMatrix, l_MeshToWorldMatrix);						
				m_DecalsMeshCutter.CutDecalsPlanes (m_DecalsMesh);
				m_DecalsMesh.OffsetActiveProjectorVertices ();
				m_Decals.UpdateDecalsMeshes (m_DecalsMesh);
				
				NextUVRectangleIndex ();
			}
		}
	}
}
