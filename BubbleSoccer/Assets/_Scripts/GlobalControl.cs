using System;
using UnityEngine;

public class GlobalControl : MonoBehaviour 
{
	public static GlobalControl Instance;
	public String username = null;
	public String password = null;
	public String rank = null;

	void Awake ()   
	{
		if (Instance == null)
		{
			DontDestroyOnLoad (gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy (gameObject);
		}
	}
}