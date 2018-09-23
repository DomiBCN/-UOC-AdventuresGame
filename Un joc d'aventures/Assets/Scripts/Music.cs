using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    static Music instance = null;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    public void ToggleMusic()
    {
        if(PlayerPrefs.GetInt("SoundOff", 0) == 0)
        {
            PlayerPrefs.SetInt("SoundOff", 1);
        }
        else
        {
            PlayerPrefs.SetInt("SoundOff", 0);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
