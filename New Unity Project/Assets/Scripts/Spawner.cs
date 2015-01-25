using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour 
{

	public GameObject ParentSpawner;
	public GameObject AI;
	List<GameObject> AIPlayer = new List<GameObject>();


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void PopulateList()
	{
		for(int i = 0; i < ParentSpawner.transform.childCount; i++)
		{
			AIPlayer.Add(ParentSpawner.transform.GetChild(i).gameObject);
		}
	}
}
