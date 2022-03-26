using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Clock : MonoBehaviour
{

    // Start is called before the first frame update

    Text textClock;
    // private float seconds = 1;
    // private int minute = 2;
    GameManager gm;

    void Start()
    {
        gm = GameManager.GetInstance();
        textClock = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameState == GameManager.GameState.GAME)
        {
            gm.seconds -= Time.deltaTime;

        }
        textClock.text = "0" + gm.minute + ":" + (int)gm.seconds;
        if (gm.minute < 0)
        {
            textClock.text = "00:00";
            gm.vidas = 0;
            gm.ChangeState(GameManager.GameState.ENDGAME);

        }
        if (gm.seconds <= 0)
        {
            gm.minute--;
            gm.seconds = 59f;
        }


    }
}
