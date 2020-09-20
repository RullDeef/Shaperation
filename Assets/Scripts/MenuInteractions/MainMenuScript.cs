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

    public void MainMenuClose()
    {
        LeanTween.move(MainMenu.GetComponent<RectTransform>(), movePos[0], moveTime);
    }

    public void MainMenuOpen()
    {
        LeanTween.move(MainMenu.GetComponent<RectTransform>(), movePos[1], moveTime);
    }
}
