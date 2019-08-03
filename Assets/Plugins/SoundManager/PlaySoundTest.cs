using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundTest : MonoBehaviour
{

    public string sound = "";
    public bool play = false;

    void Update()
    {
        if (play)
        {
            SoundManager.PlaySound(sound);
            play = false;
        }
    }
}
