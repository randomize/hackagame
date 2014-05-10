using UnityEngine;

public class GameController : MonoBehaviour, ITouchTargetedDelegate
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
    void Awake()
    {
        TouchDispatcher.Instance.addTargetedDelegate(this, 1, false);
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

        if (toScreen.y < 0)
        {
            GameOver();
        }

    }

    public void GameOver()
    {
        if (state != GameState.GameOver)
        {
            Debug.Log("Game Over");
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

    public bool TouchBegan(Vector2 position, int fingerId)
    {
        if (state == GameState.GameOver)
        {
            Application.LoadLevel(0);
        }

        return false;
    }

    void OnDestroy()
    {
        TouchDispatcher.Instance.removeDelegate(this);
    }

    public void TouchMoved(Vector2 position, int fingerId)
    {
    }

    public void TouchEnded(Vector2 position, int fingerId)
    {
    }

    public void TouchCanceled(Vector2 position, int fingerId)
    {
    }
}
