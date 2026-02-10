using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Cards")]
    public RectTransform cardRed;
    public RectTransform cardBlue;
    public RectTransform cardYellow;

    [Header("Settings")]
    public float activeScale = 1.2f;
    public float inactiveScale = 1.0f;
    public float inactiveAlpha = 0.4f;

    private Image imgRed, imgBlue, imgYellow;

    void Start()
    {
        // Get the Image components from the RectTransforms automatically
        imgRed = cardRed.GetComponent<Image>();
        imgBlue = cardBlue.GetComponent<Image>();
        imgYellow = cardYellow.GetComponent<Image>();

        // Subscribe to event
        PhaseManager.OnPhaseChanged += UpdateUI;

        // Force update immediately so it starts correct
        UpdateUI(PhaseColor.Red);
    }

    void OnDestroy()
    {
        PhaseManager.OnPhaseChanged -= UpdateUI;
    }

    void UpdateUI(PhaseColor activePhase)
    {
        // Reset all to "Inactive" state first
        SetState(cardRed, imgRed, false);
        SetState(cardBlue, imgBlue, false);
        SetState(cardYellow, imgYellow, false);

        // Highlight the "Active" one
        switch (activePhase)
        {
            case PhaseColor.Red: SetState(cardRed, imgRed, true); break;
            case PhaseColor.Blue: SetState(cardBlue, imgBlue, true); break;
            case PhaseColor.Yellow: SetState(cardYellow, imgYellow, true); break;
        }
    }

    // Helper function to handle the visuals
    void SetState(RectTransform card, Image img, bool isActive)
    {
        if (isActive)
        {
            // Make it Big and Bright
            card.localScale = Vector3.one * activeScale;
            SetAlpha(img, 1f);
        }
        else
        {
            // Make it Normal size and Dim
            card.localScale = Vector3.one * inactiveScale;
            SetAlpha(img, inactiveAlpha);
        }
    }

    void SetAlpha(Image img, float alpha)
    {
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
}