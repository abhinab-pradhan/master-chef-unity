using System;
using UnityEngine;

public class kitchenGameManager : MonoBehaviour
{

    public static kitchenGameManager instance { get; private set; }
    private enum State
    {
        waitingToStart,
        countdownToStart,
        gamePlaying,
        gameOver,
    }

    public event EventHandler onStateChange;
    public event EventHandler onGamePause;
    public event EventHandler onGameUnpause;

    private State state;
    //private float waitingToStartTimer = 1;
    private float countdownToStartTimer = 3;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 20;
    private bool isGamePause = false;

    void Awake()
    {
        instance = this;
        state = State.waitingToStart;
    }
    void Start()
    {
        gameInput.instance.onPauseAction += gameInput_onPauseAction;
        gameInput.instance.OnInteractAction += gameInput_onInteraction;
    }

    void gameInput_onInteraction(object sender, EventArgs e)
    {
        if (state == State.waitingToStart)
        {
            state = State.countdownToStart;
            onStateChange?.Invoke(this, EventArgs.Empty);
        }
    }

    void gameInput_onPauseAction(object sender, EventArgs e)
    {
        pauseGame();
    }

    void Update()
    {
        switch (state)
        {
            case State.waitingToStart:

                break;
            case State.countdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0)
                {
                    state = State.gamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    onStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.gamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0)
                {
                    state = State.gameOver;
                    onStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.gameOver:
                break;
        }
       // Debug.Log(state);
    }

    public bool isGamePlaying()
    {
        return state == State.gamePlaying;
    }

    public bool isCountDownToStartActive()
    {
        return state == State.countdownToStart;
    }

    public float getCountDownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public bool isGameOver()
    {
        return state == State.gameOver;
    }

    public float getGamePlayingTimerNormalize()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void pauseGame()
    {
        isGamePause = !isGamePause;
        if (isGamePause)
        {
            Time.timeScale = 0f;
            onGamePause?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1;
            onGameUnpause?.Invoke(this, EventArgs.Empty);
        }
    }
}
