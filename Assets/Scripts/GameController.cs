using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    private int _playerOneScore = 0;
    private int _playerTwoScore = 0;

    private bool _isPause = false;

    public delegate void OnPause(bool isPause);
    public event OnPause OnPauseEvent;

    public void AddScore(PaddleType paddle)
    {
        switch(paddle)
        {
            case PaddleType.RightPaddle:
                _playerTwoScore += 1;
                GameUIManager.Instance.UpdateScoreUI(paddle, _playerTwoScore);
                break;
            case PaddleType.LeftPaddle:
                _playerOneScore += 1;
                GameUIManager.Instance.UpdateScoreUI(paddle, _playerOneScore);
                break;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        _isPause = !_isPause;
        Time.timeScale = (_isPause) ? 0 : 1;

        OnPauseEvent?.Invoke(_isPause);
    }
}
