using UnityEngine;

public class Kicker : MonoBehaviour, ITouchTargetedDelegate {


    public enum KickerState
    {
        Idle,
        Focus,
        Fire
    }

    public float focusRadius = 0.01f;
    public KickerState state = KickerState.Idle;
    public int focusSpeed = 1;
    public int minPower = 10;
    public int maxPower = 1000;
    public int power = 10;
    private Vector2 beginFocusPosition;

    public SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake() {
        TouchDispatcher.Instance.addTargetedDelegate(this, 0, false);
	}
	
	// Update is called once per frame
	void Update () {

	    if (state == KickerState.Focus)
	    {
            power += focusSpeed;
	    }

        
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other)
        {
            Vector2 directionVector2 = other.transform.position - transform.position;
            other.gameObject.rigidbody2D.AddForce(directionVector2.normalized * (power > maxPower? maxPower: power));
            state = KickerState.Idle;
            collider2D.enabled = false;
            spriteRenderer.enabled = false;
        }
        
    }

    public bool TouchBegan(Vector2 position, int fingerId)
    {
        power = minPower;
        state = KickerState.Focus;
        beginFocusPosition = position.ToWorldVector2();
        collider2D.enabled = false;
        spriteRenderer.enabled = true;
        this.transform.position = position.ToWorldVector2();
        return true;
    }

    public void TouchMoved(Vector2 position, int fingerId)
    {
        if (state == KickerState.Focus)
        {
            if ((position.ToWorldVector2() - beginFocusPosition).magnitude > focusRadius)
            {
                collider2D.enabled = true;
                state = KickerState.Fire;
            }   
        }
        else
        {
            this.transform.position = position.ToWorldVector2();
        }
        
    }

    public void TouchEnded(Vector2 position, int fingerId)
    {
        state = KickerState.Idle;
        collider2D.enabled = false;
        spriteRenderer.enabled = false;
    }

    public void TouchCanceled(Vector2 position, int fingerId)
    {
        state = KickerState.Idle;
        collider2D.enabled = false;
        spriteRenderer.enabled = false;
    }
}
