﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour 
{
	NavMeshAgent navMesh;
	public AudioSource Source;
	public AudioClip RandomClip;
	AudioClip Speech;
	Player _Player;
	GameObject SpawnParent;
	List<GameObject> _Paths = new List<GameObject>();
	Vector3 Direction;
	Quaternion LookRotation;
	Animator Anim;
	public MoveStates _CurrentState;
	float pauseTimer = 2.0f;
	public GameObject _Ragdoll;
	public GameObject[] Hatz;
	float walkRadius = 10.0f;
	bool DoWaypoints = true;
	// Use this for initialization
	void Start () 
	{
		SpawnParent = GameObject.Find("SpawnPoints");
		AddNodeS ();
		navMesh = GetComponent<NavMeshAgent> ();
		_Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		Source = GetComponent<AudioSource> ();
		Utils.LoadAudioClip ();
		Utils.LoadAllPlayerTextures ();
		Anim = GetComponent<Animator> ();
		_CurrentState = MoveStates.Walk;
		transform.GetChild (0).renderer.material.mainTexture = Utils.PickRandomPlayerTexture ();
		Hatz[Random.Range(0,Hatz.Length)].SetActive(true);
	}
	
	// Update is called once per frame
	void Update () 
	{

		if(!DoWaypoints)
		{
			pauseTimer -= Time.deltaTime;
			if( pauseTimer <= 0.0f)
			{
				DoWaypoints = true;
			}
		}

		if (DoWaypoints == true)
		{
			float dist = GetComponent<NavMeshAgent>().remainingDistance; 
			if (dist!=Mathf.Infinity && GetComponent<NavMeshAgent>().pathStatus== NavMeshPathStatus.PathComplete && GetComponent<NavMeshAgent>().remainingDistance==0)//Arrived.
			{
				pauseTimer = Random.Range(3, 15);
				DoWaypoints = false;
				Wonder();
			}
		}

		UpdateAnimations ();
	}

	void Wonder()
	{
		Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
		randomDirection += transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
		Vector3 finalPosition = hit.position;
		GetComponent<NavMeshAgent>().destination = finalPosition;
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
		if(!DoWaypoints)
		{
			Anim.SetBool("CanWalk",false);
		}
		else
		{
			Anim.SetBool("CanWalk",true);
		}
	}

	void UpdateIdle()
	{
		if(pauseTimer <= 0.0f)
		{
			Direction = (navMesh.pathEndPosition - transform.position).normalized;
			LookRotation = Quaternion.LookRotation(Direction);
			transform.rotation = Quaternion.Slerp(transform.rotation, LookRotation, Time.deltaTime * 2);

			if(Vector3.Dot(navMesh.pathEndPosition - transform.position, transform.forward) < 0.1f)
			{
				_CurrentState = MoveStates.Walk;
			}
		}
	}

	void UpdateWalk()
	{
		if(navMesh.destination == transform.position)
		{
			pauseTimer = Random.Range(1,4);
			_CurrentState = MoveStates.Idle;
		}
		else
		{
			//navMesh.SetDestination(_Paths[Random.Range(0, _Paths.Count)].transform.position);
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
			_CurrentState = MoveStates.Talk;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			navMesh.Resume();
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Player")
		{
			if(other.gameObject.GetComponent<Player>().Sprint)
			{
				GameObject doll = (GameObject)Instantiate(_Ragdoll);
				Ragdoll ragdoll = doll.GetComponent<Ragdoll>();
				ragdoll.SetUpRagdoll(transform.GetChild(0).renderer.material.mainTexture,transform);
				for(int i = 0; i < Hatz.Length; i++)
				{
					if(Hatz[i].activeSelf == true)
					{
						Hatz[i].transform.parent = null;
						Hatz[i].rigidbody.useGravity = true;
						Hatz[i].rigidbody.isKinematic = false;
						Hatz[i].GetComponent<BoxCollider>().isTrigger = false;
					}
				}
				gameObject.SetActive(false);
			}
		}
	}

	public enum MoveStates
	{
		Idle,
		Walk,
		Talk
	}

}
