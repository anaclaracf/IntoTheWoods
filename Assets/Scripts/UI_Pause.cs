using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Pause : MonoBehaviour
{

    GameManager gm;
    public GameObject canvas;

    private void OnEnable()
    {
        gm = GameManager.GetInstance();
    }

    public void Retornar()
    {

        gm.ChangeState(GameManager.GameState.GAME);
    }

    public void Inicio()
    {

        DontDestroyOnLoad(canvas);
        SceneManager.LoadScene("Level1");

        gm.ChangeState(GameManager.GameState.MENU);

    }

}