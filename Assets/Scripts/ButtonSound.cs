using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource buttonAudio;
    public AudioClip hoverAudio;
    public AudioClip clickAudio;

    public void HoverSound()
    {
        buttonAudio.PlayOneShot(hoverAudio);
    }

    public void ClickSound()
    {
        buttonAudio.PlayOneShot(clickAudio);
    }
}
