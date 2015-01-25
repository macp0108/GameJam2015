using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour 
{

	public GameObject ParentSpawner;
	public GameObject AI;
	List<GameObject> AIPlayer = new List<GameObject>();
	int Point;

	// Use this for initialization
	void Start () 
	{
		PopulateList ();
		for(int i = 0; i < 50; i++)
		{
			Point = Random.Range(0,AIPlayer.Count);
			if(!AIPlayer[Point].GetComponent<PathNodes>().HasPlayer)
			{
				Instantiate(AI,AIPlayer[Point].transform.position,AIPlayer[i].transform.rotation);
			}
			else
			{
				i--;
			}
		}
	}

	void PopulateList()
	{
		for(int i = 0; i < ParentSpawner.transform.childCount; i++)
		{
			AIPlayer.Add(ParentSpawner.transform.GetChild(i).gameObject);
		}
	}

	void SpawnAtUnOccupiedPoint()
	{



		
	}

}
