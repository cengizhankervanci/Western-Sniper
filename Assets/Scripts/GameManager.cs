using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static Action OnGameStart;
    public static Action OnGameWin;
    public static Action OnGameLose;

    public static Action OpenZoom;
    public static Action CloseZoom;

    public void OpenZoomPanel()
    {
        OpenZoom?.Invoke();
    }

    public void CloseZoomPanel()
    {
        CloseZoom?.Invoke();
    }

    public void StartGame()
    {
        OnGameStart?.Invoke();
    }

    public void WinGame()
    {
        OnGameWin?.Invoke();
    }

    public void LoseGame()
    {
        OnGameLose?.Invoke();
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
