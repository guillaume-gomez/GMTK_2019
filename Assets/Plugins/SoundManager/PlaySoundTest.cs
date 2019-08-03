using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySoundTest : MonoBehaviour
{
    public bool playMusicNotSound = false;
    public bool stopMusicInstead = false;
    public string sound = "";
    public bool play = false;

    private void Start()
    {
        var button = GetComponent<Button>();
        if (button) button.onClick.AddListener(Play);
    }


    void Update()
    {
        if (play)
        {
            Play();
            play = false;
        }
    }

    void Play()
    {
        if (playMusicNotSound && stopMusicInstead) SoundManager.StopMusic();
        else if (playMusicNotSound) SoundManager.PlayMusic(sound);
        else SoundManager.PlaySound(sound);
    }
}
