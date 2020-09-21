using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public float moveTime;
    public Vector2[] movePos;

    public GameObject DeathMenu;
    public float DeathMenuScale;

    public GameObject MainMenu;
    public float MainMenuScale;

    public GameObject PauseMenu;
    public float PauseMenuScale;

    public void MainMenuOpen()
    {
        LeanTween.move(MainMenu.GetComponent<RectTransform>(), movePos[0], moveTime);
    }

    public void MainMenuClose()
    {
        LeanTween.move(MainMenu.GetComponent<RectTransform>(), movePos[1], moveTime);
    }

    public void PauseMenuOpen()
    {
        LeanTween.move(PauseMenu.GetComponent<RectTransform>(), movePos[0], moveTime);
    }

    public void PauseMenuClose()
    {
        LeanTween.move(PauseMenu.GetComponent<RectTransform>(), movePos[2], moveTime);
    }
}
