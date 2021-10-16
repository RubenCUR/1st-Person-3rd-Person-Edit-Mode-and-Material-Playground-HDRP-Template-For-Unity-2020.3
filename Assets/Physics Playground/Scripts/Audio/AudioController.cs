using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [Tooltip("The time used in the audio transitions.")]
    public float transitionTime = .05f;
    [Tooltip("The audio source used for the background music.")]
    public AudioSource backgroundMusicSource;
    [Tooltip("The audio source used for the reverse background music.")]
    public AudioSource reverseMusicSource;
    [Tooltip("The audio source used for the random note.")]
    public AudioSource noteClipSource;
    [Space]

    [Tooltip("The default audio mixer.")]
    public AudioMixer mainMixer;
    [Tooltip("The audio mixer snapshot used for the default mode.")]
    public AudioMixerSnapshot defaultAudioMix;
    [Tooltip("The audio mixer snapshot used for the low pass mode.")]
    public AudioMixerSnapshot lowPassSnapshot;

    [Tooltip("The notes used when an element is spawned.")]
    public AudioClip[] forwardNoteClips;
    [Tooltip("The reverse notes used when an element is destroyed.")]
    public AudioClip[] reverseNoteClips;


    //Reset our mix when the game starts
    private void Start()
    {
        FullSpeedAudio();
        AudioForward();
    }

    /// <summary>
    /// Play random note from "forwardNoteClips" and the click sound.
    /// </summary>
    public void PlayRandomClip(AudioClip[] audioClips)
    {
        noteClipSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        noteClipSource.Play();
    }

    /// <summary>
    /// Makes a transition to the default audio snapshot.
    /// </summary>
    public void RemoveLowpassEffect()
    {
        defaultAudioMix.TransitionTo(transitionTime);    
    }

    /// <summary>
    /// Makes a transition to the low pass audio snapshot.
    /// </summary>
    public void LowpassAudio()
    {
        lowPassSnapshot.TransitionTo(transitionTime);
    }
    
    /// <summary>
    /// Rescale the sounds pitch to 1.
    /// </summary>
    public void FullSpeedAudio()
    {
        backgroundMusicSource.pitch = 1f;
        reverseMusicSource.pitch = 1f;
        noteClipSource.pitch = 1f;
    }

    /// <summary>
    /// Rescale the sounds pitch to .5.
    /// </summary>
    public void HalfPitchAudio()
    {
        backgroundMusicSource.pitch = .5f;
        reverseMusicSource.pitch = .5f;
        noteClipSource.pitch = .5f;
    }
    /// <summary>
    ///Turn up the mixer group for the loop of the music playing forwards and turn down backwards audio
    /// </summary>
    public void AudioForward()
    {
        //SetFloat is independent of audio mixer snapshots, so this can change even when snapshots are changing
        mainMixer.SetFloat("ForwardMusicVolume", 0f);
        mainMixer.SetFloat("ReversedMusicVolume", -80f);
    }

    /// <summary>
    /// Turn up the backwards audio and turn down the forwards loop group in the mixer
    /// </summary>
    public void AudioReverse()
    {
        //SetFloat is independent of audio mixer snapshots, so this can change even when snapshots are changing
        mainMixer.SetFloat("ForwardMusicVolume", -80f);
        mainMixer.SetFloat("ReversedMusicVolume", 0f);
    }
}
