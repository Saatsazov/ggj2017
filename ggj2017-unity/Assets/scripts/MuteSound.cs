using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteSound : MonoBehaviour {

	public void onSoundMute()
	{
		AudioListener.volume = 1 - AudioListener.volume;
	}
}
