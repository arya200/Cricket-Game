using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_script : MonoBehaviour
{
    public static audio_script instance {get; private set; }
    public GameObject hit;
    private AudioSource hit_audio;

    void Awake()
    {
        instance=this;
    }
    void Start()
    {
        hit_audio = hit.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void play_hit_audio()
    {
        hit_audio.Play();
    }   

}
