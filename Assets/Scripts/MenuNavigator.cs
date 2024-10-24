using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigator : MonoBehaviour
{
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _loadMenu;
    [SerializeField] GameObject _newGameMenu;

    List<GameObject> _menuList;

    void Awake() {
        _menuList = new List<GameObject> { _mainMenu, _loadMenu, _newGameMenu};
    }
    public void ShowMainMenu() => ShowMenu(_mainMenu);
    public void ShowLoadMenu() => ShowMenu(_loadMenu);
    public void ShowNewGameMenu() => ShowMenu(_newGameMenu);
    void ShowMenu(GameObject menu) {
        _menuList.ForEach(m => m.SetActive(false));
        menu.SetActive(true);
    }
}
