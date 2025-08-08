using UnityEngine;

public class musicManager : MonoBehaviour
{

    private const string Player_pref_music_volume = "musicVolume";
    public static musicManager instance { get; private set; }
    private AudioSource audioSource;
    private float volume=.3f;

    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat(Player_pref_music_volume, .3f);
        audioSource.volume = volume;
    }
    public void changeVolume()
    {
        volume += .1f;

        if (volume > 1)
        {
            volume = 0;
        }
        audioSource.volume = volume;

        PlayerPrefs.SetFloat(Player_pref_music_volume, volume);
        PlayerPrefs.Save();
    }

    public float getVolume()
    {
        return volume;
    }
}
