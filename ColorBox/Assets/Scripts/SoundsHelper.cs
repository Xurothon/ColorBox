using UnityEngine;
using UnityEngine.Audio;

[RequireComponent (typeof (AudioSource))]
public class SoundsHelper : MonoBehaviour
{
    public static SoundsHelper Instance;
    private AudioSource _audioSource;
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private AudioClip _disappearance;
    [SerializeField] private AudioClip _changeGravitation;
    [SerializeField] private AudioClip _takeTile;

    private void Awake () { Instance = this; }

    private void Start ()
    {
        _audioSource = GetComponent<AudioSource> ();
    }

    private void PlayClip (AudioClip audio)
    {
        _audioSource.clip = audio;
        _audioSource.Play ();
    }

    public void PlayDisappearanceTile ()
    {
        PlayClip (_disappearance);
    }

    public void PlayGravitationClip ()
    {
        PlayClip (_changeGravitation);
    }

    public void PlayTakeTileClip ()
    {
        PlayClip (_takeTile);
    }
}