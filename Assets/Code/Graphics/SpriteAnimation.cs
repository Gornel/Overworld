using UnityEngine;
using System.Collections;

public class SpriteAnimation : ScriptableObject
{
	public Texture texture;
	public int FPS;
	public int Columns;
	public int Rows;
	public bool Loop;
	public int hitFrame;
	
	public float GetTime ()
	{
		return (Rows * Columns) / FPS;
	}
	
	public float GetFrames ()
	{
		return (Rows * Columns);
	}
}
