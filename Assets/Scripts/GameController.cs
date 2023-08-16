using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    private int _playerOneScore = 0;
    private int _playerTwoScore = 0;

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
}
