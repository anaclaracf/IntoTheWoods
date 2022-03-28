using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_FimDeJogo : MonoBehaviour
{
    public Text message;

    GameManager gm;
    public GameObject canvas;

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
        DontDestroyOnLoad(canvas);
        SceneManager.LoadScene("Level1");
        gm.ChangeState(GameManager.GameState.MENU);

    }

    void Start() { }
    void Update() { }
}