using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.Playables;

public class soundManager : MonoBehaviour
{

    private const string player_pref_sound_volume = "sosundVolume";
    public static soundManager instance { get; private set; }

    [SerializeField] private audioClipRefSO audioClipRefSO;

    private float volume = 1;
    void Awake()
    {
        instance = this;

        volume = PlayerPrefs.GetFloat(player_pref_sound_volume, 1);
    }
    void Start()
    {
        deliveryManager.instance.onRecipeSuccess += deliveryManager_onRecipeSuccess;
        deliveryManager.instance.onRecipeFailed += deliveryManager_onRecipeFailed;
        cuttingCounter.onAnyCut += cuttingCounter_onAnyCut;
        player.Instance.onPickSomething += player_onPickSomething;
        baseCounter.onAnyObjectPlacedHere += baseCounter_onAnyObjectPlacedHere;
        trashCounter.onANyObjectTrashed += trashCounter_onANyObjectTrashed;
    }

    void trashCounter_onANyObjectTrashed(object sender, System.EventArgs e)
    {
        trashCounter trashCounter = sender as trashCounter;
        playeSound(audioClipRefSO.trash, trashCounter.transform.position);
    }

    void baseCounter_onAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        baseCounter baseCounter = sender as baseCounter;
        playeSound(audioClipRefSO.objectDrop, baseCounter.transform.position);
    }

    void player_onPickSomething(object sender, System.EventArgs e)
    {
        playeSound(audioClipRefSO.objectPickup, player.Instance.transform.position);
    }

    void cuttingCounter_onAnyCut(object sender, System.EventArgs e)
    {
        cuttingCounter cuttingCounter = sender as cuttingCounter;
        playeSound(audioClipRefSO.chop, cuttingCounter.transform.position);
    }
    void deliveryManager_onRecipeSuccess(object sender, System.EventArgs e)
    {
        deliveryCounter deliveryCounter = deliveryCounter.instance;
        playeSound(audioClipRefSO.deliverySuccess, deliveryCounter.transform.position);
    }
    void deliveryManager_onRecipeFailed(object sender, System.EventArgs e)
    {
        deliveryCounter deliveryCounter = deliveryCounter.instance;
        playeSound(audioClipRefSO.deliveryFail, deliveryCounter.transform.position);
    }
    void playeSound(AudioClip audioClip, Vector3 position, float volume = 1)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    void playeSound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1)
    {
        playeSound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier * volume);
    }

    public void playFootStepSound(Vector3 position, float volume)
    {
        playeSound(audioClipRefSO.footstep, position, volume);
    }
    public void playCountDownSound()
    {
        playeSound(audioClipRefSO.warning, Vector3.zero);
    }
        public void playWarningSound(Vector3 position)
    {
        playeSound(audioClipRefSO.warning, position);
    }
    public void changeVolume()
    {
        volume += .1f;

        if (volume > 1)
        {
            volume = 0;
        }

        PlayerPrefs.SetFloat(player_pref_sound_volume, volume);
        PlayerPrefs.Save();
    }

    public float getVolume()
    {
        return volume;
    }
}
