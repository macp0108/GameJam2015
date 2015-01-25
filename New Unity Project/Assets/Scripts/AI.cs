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
	Animator Anim;
	MoveStates _CurrentState;
	float pauseTimer = 2.0f;
	// Use this for initialization
	void Start () 
	{
		AddNodeS ();
		navMesh = GetComponent<NavMeshAgent> ();
		_Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		Source = GetComponent<AudioSource> ();
		Utils.LoadAudioClip ();
		transform.position = _Paths[Random.Range(0,_Paths.Count)].transform.position;
		Anim = GetComponent<Animator> ();
		_CurrentState = MoveStates.Walk;
	}
	
	// Update is called once per frame
	void Update () 
	{

		switch(_CurrentState)
		{
		case MoveStates.Idle:
			UpdateIdle();
			break;
		case MoveStates.Talk:
			UpdateTalk();
			break;
		case MoveStates.Walk:
			UpdateWalk();
			break;

		}


		UpdateAnimations ();
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

	void UpdateAnimations()
	{
//		if(!navMesh.hasPath)
//		{
//			Anim.SetBool("CanWalk",false);
//		}
//		else
//		{
//			Anim.SetBool("CanWalk",true);
//		}
//
//		if(!Anim.GetBool("CanWalk"))
//		{
//			Anim.SetFloat("Idle",Random.Range(0,2));
//		}
//		else
//		{
//			Anim.SetFloat("Idle", -);
//		}
	}

	void UpdateIdle()
	{

	}

	void UpdateWalk()
	{
		if(navMesh.pathEndPosition == transform.position)
		{
			pauseTimer = Random.Range(1,4);
		}
	}

	void UpdateTalk()
	{

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

	enum MoveStates
	{
		Idle,
		Walk,
		Talk
	}

}
