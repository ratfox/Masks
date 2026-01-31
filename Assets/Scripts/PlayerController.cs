using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;
    // Static instance of the controller that can be accessed from any script

    public static PlayerController Instance;
    [Header("Settings")]
    public float moveSpeed = 5f;
    private Animator anim;

    [Header("Physics")]
    private Rigidbody2D rb;
    private Vector2 moveInput;
    void Start() {
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        // Persistence Logic
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 1. Get Input (WASD or Arrow Keys)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // 2. Store the input as a direction
        moveInput = new Vector2(moveX, moveY).normalized;
        anim.SetBool("Walking", moveInput.magnitude > 0.01);
        if (moveInput.x > 0)
        {
            // Facing Right (Normal)
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < 0)
        {
            // Facing Left (Mirrored)
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        // 3. Apply movement to the Rigidbody physics
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
    public SceneTransition.TransitionSide lastTransitionSide;
    public bool shouldSetDestination = false;
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If this is the start of the game, DON'T run the teleport logic
        if (!shouldSetDestination) 
        {
            return; 
        }
        SceneTransition[] transitions = FindObjectsOfType<SceneTransition>();
        float offset = 1.5f; // How far to push the player out of the door

        foreach (SceneTransition tr in transitions)
        {
            if (IsOppositeSide(tr.entranceSide, lastTransitionSide))
            {
                Vector3 newPos = tr.transform.position;

                if (tr.entranceSide == SceneTransition.TransitionSide.Left)
                {
                    newPos.x += offset; // Move player slightly to the right
                    newPos.y = transform.position.y;
                }
                else if (tr.entranceSide == SceneTransition.TransitionSide.Right)
                {
                    newPos.x -= offset; // Move player slightly to the left
                    newPos.y = transform.position.y;
                }
                // Add similar logic for Top/Bottom if needed

                transform.position = newPos;
                shouldSetDestination = false;
                break;
            }
        }
    }

    private bool IsOppositeSide(SceneTransition.TransitionSide a, SceneTransition.TransitionSide b)
    {
        if (a == SceneTransition.TransitionSide.Left && b == SceneTransition.TransitionSide.Right) return true;
        if (a == SceneTransition.TransitionSide.Right && b == SceneTransition.TransitionSide.Left) return true;
        if (a == SceneTransition.TransitionSide.Top && b == SceneTransition.TransitionSide.Bottom) return true;
        if (a == SceneTransition.TransitionSide.Bottom && b == SceneTransition.TransitionSide.Top) return true;
        return false;
    }
}

