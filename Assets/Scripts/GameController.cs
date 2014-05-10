using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController instance
    {
        get
        {
            if (!_instance)
            {
                _instance = Transform.FindObjectOfType<GameController>();
            }

            return _instance;
        }
    }

    private static GameController _instance;

    public enum GameState
    {
        InGame,
        GameOver
    }

    public GameState state;
    public GameObject hen;

    public TextMesh gameOverLabel;
    public TextMesh gameOverScoreLabel;
    public TextMesh scoreLabel;

    private int gameScore;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 henPosition = hen.transform.position;
        Vector3 toScreen = Camera.main.WorldToScreenPoint(henPosition);

        if (toScreen.x > Screen.width)
        {
            toScreen.x = 0;
            hen.transform.position = toScreen;
            hen.transform.position = Camera.main.ScreenToWorldPoint(toScreen);
        }
        else if (toScreen.x < 0)
        {
            toScreen.x = Screen.width;
            hen.transform.position = Camera.main.ScreenToWorldPoint(toScreen);
        }

        scoreLabel.text = string.Format("Score: {0:D4}", gameScore);

        if (toScreen.y < 0 && state == GameState.InGame)
        {
            state = GameState.GameOver;
            gameOverScoreLabel.gameObject.SetActive(true);
            gameOverLabel.gameObject.SetActive(true);
            gameOverScoreLabel.text = string.Format("Your score {0}", gameScore);
        }

    }

    public void addPoints(int value)
    {
        if (state == GameState.InGame)
        {
            gameScore += value;
        }
    }
}
