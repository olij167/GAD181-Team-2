using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    //public Mis1eader.MediaPlayer.AudioClipPlayer mediaPlayer = null;
    public Mis1eader.MediaPlayer.AudioSourcePlayer mediaPlayer = null;
    //public Mis1eader.MediaPlayer.VideoClipPlayer mediaPlayer = null;
    //public Mis1eader.MediaPlayer.VideoSourcePlayer mediaPlayer = null;
    public UnityEngine.KeyCode backward = UnityEngine.KeyCode.E;
    public UnityEngine.KeyCode forward = UnityEngine.KeyCode.Q;
    private void Update()
    {
        if (mediaPlayer)
        {
            if (UnityEngine.Input.GetKeyDown(backward)) mediaPlayer.SkipBackward();
            else if (UnityEngine.Input.GetKeyDown(forward)) mediaPlayer.SkipForward();
        }
    }
}

