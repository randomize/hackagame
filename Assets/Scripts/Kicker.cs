using UnityEngine;

public class Kicker : MonoBehaviour, ITouchTargetedDelegate
{
    public enum KickerState
    {
        Idle,
        Focus,
        Fire
    }

    public GameController gameController;
    public GameObject bootObject;

    public float focusRadius = 0.01f;
    public KickerState state = KickerState.Idle;
    public int focusSpeed = 1;
    public int minPower = 10;
    public int maxPower = 1000;
    public int power = 10;
    private Vector2 beginFocusPosition;

    // Use this for initialization
    void Awake()
    {
        TouchDispatcher.Instance.addTargetedDelegate(this, 0, false);
    }

    // Update is called once per frame
    void Update()
    {

        if (state == KickerState.Focus)
        {
            power += focusSpeed;
        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other)
        {

            if (power > minPower)
            {
                Vector2 directionVector2 = other.transform.position - transform.position;
                other.gameObject.rigidbody2D.AddForce(directionVector2.normalized*(power > maxPower ? maxPower : power));

                gameController.addPoints(power/20);
                gameController.hen.GetComponent<Animator>().SetTrigger("Kick");
                bootObject.GetComponent<Animator>().SetTrigger("Kick");
            }
            else
            {
                bootObject.GetComponent<Animator>().SetTrigger("Cancel");
            }

            state = KickerState.Idle;
            collider2D.enabled = false;
        }

    }

    public bool TouchBegan(Vector2 position, int fingerId)
    {

        if (gameController.state != GameController.GameState.InGame)
            return false;

        power = 0;
        state = KickerState.Focus;
        beginFocusPosition = position.ToWorldVector2() - Camera.main.transform.position.ToVector2();
        collider2D.enabled = false;
        gameObject.transform.position = position;
        this.transform.position = position.ToWorldVector2();
        bootObject.GetComponent<Animator>().SetTrigger("Ready");
        return true;
    }

    public void TouchMoved(Vector2 position, int fingerId)
    {
        if (state == KickerState.Focus)
        {

            if ((position.ToWorldVector2() - Camera.main.transform.position.ToVector2() - beginFocusPosition).magnitude > focusRadius)
            {
                collider2D.enabled = true;
                state = KickerState.Fire;
            }

            this.transform.position = position.ToWorldVector2();
        }
        else if (state == KickerState.Fire)
        {
            this.transform.position = position.ToWorldVector2();
        }

    }

    public void TouchEnded(Vector2 position, int fingerId)
    {
        state = KickerState.Idle;
        collider2D.enabled = false;
        bootObject.GetComponent<Animator>().SetTrigger("Cancel");
    }

    public void TouchCanceled(Vector2 position, int fingerId)
    {
        state = KickerState.Idle;
        collider2D.enabled = false;
        bootObject.GetComponent<Animator>().SetTrigger("Cancel");
    }

}
