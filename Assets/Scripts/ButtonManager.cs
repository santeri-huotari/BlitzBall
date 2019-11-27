using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A dummy script that just forwards functions to Game manager
/// </summary>
public class ButtonManager : MonoBehaviour
{
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {

    }

    public void StartGame()
    {
        gm.StartGame();
    }

    public void NextLevel()
    {
        gm.NextLevel();
    }

    public void ToMenu()
    {
        gm.ToMenu();
    }

    public void ExitGame()
    {
        gm.ExitGame();
    }

    public void TogglePause()
    {
        gm.TogglePause();
    }

    public void GameOver()
    {
        gm.GameOver();
    }

    public void RestartLevel()
    {
        gm.RestartLevel();
    }
}
