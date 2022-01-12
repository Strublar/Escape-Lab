using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public AudioClip[] playlist;
    public AudioClip grabSound;
    public AudioClip pourSound;
    public AudioSource audioSource;
    private int musicIndex = 0;

    public AudioMixerGroup musicMixer;
    public AudioMixerGroup soundMixer;

    private static AudioManager instance;
    private AudioManager() { } //block the use of new()

    // Access point
    public static AudioManager Instance { get => instance; }

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    private void Start() {
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    private void Update() {
        if (!audioSource.isPlaying) {
            PlayNextSong();
        }
    }

    private void PlayNextSong() {
        musicIndex = (musicIndex + 1) % playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    public AudioSource PlayMusic(AudioClip clip) {
        AudioSource tempAudioSource = gameObject.AddComponent<AudioSource>();
        tempAudioSource.clip = clip;
        tempAudioSource.outputAudioMixerGroup = musicMixer;
        tempAudioSource.Play();
        Destroy(tempAudioSource, clip.length);
        return audioSource;
    }
    public AudioSource PlayClipAtGO(AudioClip clip, GameObject go, float volume =1) {
        AudioSource tempAudioSource = go.AddComponent<AudioSource>();
        tempAudioSource.clip = clip;
        tempAudioSource.volume = volume;
        tempAudioSource.spatialBlend = 1;
        tempAudioSource.outputAudioMixerGroup = soundMixer;
        tempAudioSource.Play();
        Destroy(tempAudioSource, clip.length);
        return audioSource;
    }

    public void PlayGrabSound(GameObject go) {
        AudioManager.Instance.PlayClipAtGO(grabSound, go);
    }
    public void PlayLiquidSound(GameObject go) {
        AudioManager.Instance.PlayClipAtGO(pourSound, go, 0.2f);
    }
    public void StopLiquidSound(GameObject go) {
        AudioSource tempAudioSource = go.GetComponent<AudioSource>();
        if (tempAudioSource != null) {
            Destroy(tempAudioSource);
        }
    }

}
