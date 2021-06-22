using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxSliderTestAudio : MonoBehaviour
{
    public AudioSource source;
    public AudioClip poof;
    private void OnMouseDown()
    {
        source.PlayOneShot(poof);
    }

}