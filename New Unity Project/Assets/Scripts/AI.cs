using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterController))]
public class AI : MonoBehaviour 
{
	NavMeshAgent navMesh;
	public AudioSource Source;
	public AudioClip RandomClip;
	AudioClip Speech;
	Player _Player;
	public GameObject SpawnParent;
	List<GameObject> _Paths = new List<GameObject>();
	Vector3 Direction;
	Quaternion LookRotation;
	// Use this for initialization
	void Start () 
	{
		AddNodeS ();
		navMesh = GetComponent<NavMeshAgent> ();
		_Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		Source = GetComponent<AudioSource> ();
		Utils.LoadAudioClip ();
		transform.position = _Paths[Random.Range(0,_Paths.Count)].transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!navMesh.hasPath)
		{
			navMesh.SetDestination(_Paths[Random.Range(0,_Paths.Count)].transform.position);
		}
	}

	public void PlayRandomSpeech()
	{
		if(!Source.isPlaying)
		{
			Source.clip = Utils.PlayRandomClip();
			Source.Play();
		}
	}

	void AddNodeS()
	{
		for(int i = 0; i < SpawnParent.transform.childCount; i++)
		{
			_Paths.Add(SpawnParent.transform.GetChild(i).gameObject);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")
		{
			navMesh.Stop(true);
			Direction = (other.transform.position - transform.position).normalized;
			LookRotation = Quaternion.LookRotation(Direction);
			transform.rotation = Quaternion.Slerp(transform.rotation, LookRotation, Time.deltaTime * 2);
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			navMesh.Resume();
		}
	}

}
