using UnityEngine;

public class PhaseObject : MonoBehaviour
{
    [Header("Settings")]
    public PhaseColor myColor; // Select Red, Blue, or Yellow in Inspector

    private BoxCollider2D myCollider;
    private SpriteRenderer mySprite;

    void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // Subscribe to the event
        PhaseManager.OnPhaseChanged += UpdateState;

        // Force an update at start so we match the Manager's current state
        PhaseManager manager = Object.FindFirstObjectByType<PhaseManager>();
        if (manager != null)
        {
            UpdateState(manager.currentPhase);
        }
    }

    void OnDestroy()
    {
        // Always unsubscribe when destroyed to prevent errors
        PhaseManager.OnPhaseChanged -= UpdateState;
    }

    // This function runs automatically whenever the Phase changes
    void UpdateState(PhaseColor activePhase)
    {
        if (myColor == activePhase)
        {
            // STATE: SOLID (Active)
            myCollider.enabled = true;
            SetAlpha(1f);
        }
        else
        {
            // STATE: GHOST (Inactive)
            myCollider.enabled = false;
            SetAlpha(0.1f);
        }
    }

    void SetAlpha(float alpha)
    {
        if (mySprite == null) return;

        Color c = mySprite.color;
        c.a = alpha;
        mySprite.color = c;
    }
}