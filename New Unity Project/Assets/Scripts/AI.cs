using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterController))]
public class AI : MonoBehaviour 
{
	NavMeshAgent navMesh;

	// Use this for initialization
	void Start () 
	{
		navMesh = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
