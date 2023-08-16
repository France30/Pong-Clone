using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum MenuOption
{
    Start,
    Exit,
    MainMenu
}

public class MenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform _arrowSelectUI;
    [SerializeField] private float _arrowMoveOffset;
    [SerializeField] private MenuOption[] _menuOptions;

    private Vector2 _initialPosition;
    private Vector2 _maxPosition;
    private int _currentSelectIndex;

    private void Awake()
    {
        _initialPosition = _arrowSelectUI.anchoredPosition;

        Vector2 maxPosition = _initialPosition;
        for(int i = 0; i < _menuOptions.Length - 1; i++)
        {
            maxPosition.y -= _arrowMoveOffset; 
        }
        _maxPosition = maxPosition;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            _currentSelectIndex = (_currentSelectIndex >= _menuOptions.Length - 1) ? 0 : _currentSelectIndex + 1;
            float arrowSelectYPosition = (_currentSelectIndex > 0) ? _arrowSelectUI.anchoredPosition.y - _arrowMoveOffset : _initialPosition.y;
            _arrowSelectUI.anchoredPosition = new Vector2(_arrowSelectUI.anchoredPosition.x, arrowSelectYPosition);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _currentSelectIndex = (_currentSelectIndex <= 0) ? _menuOptions.Length - 1 : _currentSelectIndex - 1;
            float arrowSelectYPosition = (_currentSelectIndex < _menuOptions.Length - 1) ? _arrowSelectUI.anchoredPosition.y + _arrowMoveOffset : _maxPosition.y;
            _arrowSelectUI.anchoredPosition = new Vector2(_arrowSelectUI.anchoredPosition.x, arrowSelectYPosition);
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            MenuSelect(_menuOptions[_currentSelectIndex]);
        }
    }

    private void MenuSelect(MenuOption menuOption)
    {
        switch(menuOption)
        {
            case MenuOption.Start:
                SceneManager.LoadScene(1);
                break;
            case MenuOption.Exit:
                Application.Quit();
                break;
            case MenuOption.MainMenu:
                DisableGameManagers();
                SceneManager.LoadScene(0);
                break;
        }
    }

    private void DisableGameManagers()
    {
        GameUIManager.Instance.gameObject.SetActive(false);
        GameController.Instance.gameObject.SetActive(false);
    }
}
