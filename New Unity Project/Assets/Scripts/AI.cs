using UnityEngine;
using System.Collections;
//
//[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(CharacterController))]
public class AI : MonoBehaviour 
{
	NavMeshAgent navMesh;
	AudioSource Source;
	public AudioClip RandomClip;
	AudioClip Speech;
	// Use this for initialization
	void Start () 
	{
//		navMesh = GetComponent<NavMeshAgent> ();
		Source = GetComponent<AudioSource> ();
		Utils.LoadAudioClip ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void PlayRandomSpeech()
	{
		if(!Source.isPlaying)
		{
			Source.PlayOneShot (Utils.PlayRandomClip());
		}
	}
}
