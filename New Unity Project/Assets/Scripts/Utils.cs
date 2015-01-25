using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Utils : MonoBehaviour
{
	static List<AudioClip> AllAudioClips = new List<AudioClip> ();
	static List<Texture> AllGUITexts = new List<Texture> (); 


	public static void LoadAudioClip()
	{
		string ResoursesPath = "WavVoices/Clip";
		for(int i = 0; i < 51; i++)
		{
			AudioClip clip = (AudioClip)Resources.Load(ResoursesPath + i.ToString(), typeof(AudioClip)); 
			AllAudioClips.Add(clip);
			ResoursesPath = "WavVoices/Clip";
		}
	}

	public static AudioClip PlayRandomClip()
	{
		int number = Random.Range (0, AllAudioClips.Count);
		return AllAudioClips[number];
	}

	public static void LoadGuiTextures()
	{
		string ResoursesPath = "GUITextures/PressE-"; 
		for(int i = 0; i < 3; i++)
		{
			Texture GUIE = (Texture)Resources.Load(ResoursesPath + i.ToString(), typeof(Texture)); 
			AllGUITexts.Add (GUIE); 
			ResoursesPath = "GUITextures/PressE-"; 
		}
	}

	public static Texture DisplayRandomGUI()
	{
		Debug.Log (AllGUITexts.Count);
		int numberGUI = Random.Range (0, AllGUITexts.Count); 
		return AllGUITexts [numberGUI]; 
	}
}
