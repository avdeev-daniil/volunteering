using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound2 : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> clip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayS()
    {
        audioSource.PlayOneShot(clip[0]);
    }
}