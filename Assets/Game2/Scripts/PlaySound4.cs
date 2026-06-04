using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound4 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public List<AudioClip> clip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(int a)
    {
        if (a == 5)
        {
            audioSource2.PlayOneShot(clip[a]);
        }
        else{
            audioSource.PlayOneShot(clip[a]);
        }
    }
}