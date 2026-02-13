using UnityEngine;
using UnityEngine.InputSystem;
using System;

// Define the three states
public enum PhaseColor { Red, Blue, Yellow }

public class PhaseManager : MonoBehaviour
{
    // The Event that platforms listen to
    public static event Action<PhaseColor> OnPhaseChanged;

    [Header("Debug Info")]
    public PhaseColor currentPhase = PhaseColor.Red;
    
    [Header("Camera Info")]
    public Camera cam;

    [Header("Color Info")] 
    public Color RedColor;
    public Color BlueColor;
    public Color YellowColor;

    void Start()
    {
        // Initialize the world to Red at the start
        SwitchPhase(PhaseColor.Red);
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // J = Switch to Red
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            SwitchPhase(PhaseColor.Red);
            cam.backgroundColor = RedColor;
        }
        // K = Switch to Blue
        else if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            SwitchPhase(PhaseColor.Blue);
            cam.backgroundColor = BlueColor;
        }
        // L = Switch to Yellow
        else if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            SwitchPhase(PhaseColor.Yellow);
            cam.backgroundColor = YellowColor;
        }
    }

    private void SwitchPhase(PhaseColor newPhase)
    {
        // Optimization: Don't switch if we are already in this phase
        if (currentPhase == newPhase) return;

        currentPhase = newPhase;

        // Trigger the event for all listening objects
        OnPhaseChanged?.Invoke(currentPhase);

        Debug.Log($"Phase Switched: {newPhase}");
    }
}