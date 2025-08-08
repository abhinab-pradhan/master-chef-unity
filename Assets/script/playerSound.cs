using UnityEngine;

public class playerSound : MonoBehaviour
{
    private float footStepTimer;
    private float footStepTimerMax = .1f;
    private player player;
    void Awake()
    {
        player = GetComponent<player>();
    }
    void Update()
    {
        footStepTimer -= Time.deltaTime;
        if (footStepTimer < 0)
        {
            footStepTimer = footStepTimerMax;

            if (player.IsWalking())
            {
                float volume = 1;
                soundManager.instance.playFootStepSound(player.transform.position, volume);
            }
        }
    }
}
