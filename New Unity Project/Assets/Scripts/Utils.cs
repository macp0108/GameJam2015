using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Utils : MonoBehaviour
{
	static List<AudioClip> AllAudioClips = new List<AudioClip> ();


	public static void LoadAudioClip()
	{
		string ResoursesPath = "WavVoices/Clip";
		for(int i = 0; i < 51; i++)
		{
			Debug.Log("AddedAudioClip");
			AudioClip clip = (AudioClip)Resources.Load(ResoursesPath + i.ToString(), typeof(AudioClip)); 
			AllAudioClips.Add(clip);
			ResoursesPath = "WavVoices/Clip";
			Debug.Log(clip);
		}
	}

	public static AudioClip PlayRandomClip()
	{
		int number = Random.Range (0, AllAudioClips.Count);
		return AllAudioClips[number];
	}
}
