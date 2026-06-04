using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound3 : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> clip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        audioSource.PlayOneShot(clip[5]);
    }
}