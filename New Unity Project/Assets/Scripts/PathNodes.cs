using UnityEngine;
using System.Collections;

public class PathNodes : MonoBehaviour {
	
	bool HasPlayer;
	// Use this for initialization
	void Start () 
	{
		GetComponent<MeshRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetComponent<MeshRenderer> ().enabled = false;
	}
}
