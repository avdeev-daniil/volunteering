using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> clip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX()
    {
        audioSource.PlayOneShot(clip[4]);
    }
}