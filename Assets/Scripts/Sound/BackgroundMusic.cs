using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    private const string PlayerPrefsBackgroundMusicVolume = "BackgroundMusicVolume";

    private AudioSource _audioSource;
    private float _volume = 0.3f;

    public static BackgroundMusic Instanse { get; private set; }

    private void Awake()
    {
        Instanse = this;

        _audioSource = GetComponent<AudioSource>();

        _volume = PlayerPrefs.GetFloat(PlayerPrefsBackgroundMusicVolume, 0.3f);

        _audioSource.volume = _volume;
    }

    public void ChangeVolume()
    {
        _volume += 0.1f;

        if (_volume > 1f)
            _volume = 0f;

        _audioSource.volume = _volume;

        PlayerPrefs.SetFloat(PlayerPrefsBackgroundMusicVolume, _volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return _volume;
    }
}
