using UnityEngine;
using System.Collections;

public class Ragdoll : MonoBehaviour 
{
	
	public void SetUpRagdoll(Texture texture, Transform playerPos)
	{
		transform.GetChild(0).renderer.material.mainTexture = texture;
		transform.position = playerPos.position;
		transform.rotation = playerPos.rotation;
	}
}
