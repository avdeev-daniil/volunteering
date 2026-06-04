using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound1 : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> clip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySF()
    {
        audioSource.PlayOneShot(clip[1]);
    }
}