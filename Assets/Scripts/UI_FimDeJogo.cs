using UnityEngine;
using UnityEngine.UI;
public class UI_FimDeJogo : MonoBehaviour
{
    public Text message;

    GameManager gm;
    private void OnEnable()
    {
        gm = GameManager.GetInstance();

        if (gm.vidas > 0)
        {
            message.text = "Level Complete";
        }
        else
        {
            message.text = "Maybe Next Time...";
        }
    }

    public void Voltar()
    {
        gm.ChangeState(GameManager.GameState.MENU);
    }

    void Start() { }
    void Update() { }
}