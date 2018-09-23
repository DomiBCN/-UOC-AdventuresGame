using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteScript : MonoBehaviour {

    public Sprite musicOn;
    public Sprite musicOff;

	// Use this for initialization
	void Start () {
        UpdateMusicSound();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleSound()
    {
        if (PlayerPrefs.GetInt("SoundOff", 0) == 0)
        {
            PlayerPrefs.SetInt("SoundOff", 1);
        }
        else
        {
            PlayerPrefs.SetInt("SoundOff", 0);
        }
        UpdateMusicSound();
    }

    void UpdateMusicSound()
    {
        if (PlayerPrefs.GetInt("SoundOff", 0) == 0)
        {
            this.GetComponent<Image>().sprite = musicOn;
            AudioListener.volume = 1;
        }
        else
        {
            this.GetComponent<Image>().sprite = musicOff;
            AudioListener.volume = 0;
        }
    }
}
