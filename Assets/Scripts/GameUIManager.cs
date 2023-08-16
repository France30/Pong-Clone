using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : Singleton<GameUIManager>
{
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private TextMeshProUGUI _playerOneScoreUI;
    [SerializeField] private TextMeshProUGUI _playerTwoScoreUI;

    public void UpdateScoreUI(PaddleType paddle, int score)
    {
        switch (paddle)
        {
            case PaddleType.RightPaddle:
                _playerTwoScoreUI.text = score.ToString();
                break;
            case PaddleType.LeftPaddle:
                _playerOneScoreUI.text = score.ToString();
                break;
        }
    }

    private void OnEnable()
    {
        GameController.Instance.OnPauseEvent += EnablePauseUI;
    }

    private void OnDisable()
    {
        if (GameController.Instance == null) return;

        GameController.Instance.OnPauseEvent -= EnablePauseUI;
    }

    private void EnablePauseUI(bool isEnable)
    {
        _pauseUI.SetActive(isEnable);
    }
}
