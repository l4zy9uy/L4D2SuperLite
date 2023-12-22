using System;
using TMPro;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
#endif

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool shoot;
    public bool reload;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    private static bool cursorLocked = true;
    public bool cursorInputForLook = true;

    public bool primaryWeapon;
    public bool secondaryWeapon;
    public bool ternaryWeapon;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        InputManager.SetCursorState(true);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!IsGamePaused())
        {
            MoveInput(context.ReadValue<Vector2>());
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        if (!IsGamePaused() && cursorInputForLook)
        {
            LookInput(context.ReadValue<Vector2>());
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            JumpInput(true);
        }
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started || context.phase == InputActionPhase.Performed)
        {
            SprintInput(true);
        }
        else
        {
            SprintInput(false);
        }
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                ShootInput(true);
                break;
            case InputActionPhase.Performed:
                break;
            case InputActionPhase.Canceled:
                ShootInput(false);
                break;
            default:
                break;
        }
    }
    public void OnPrimary(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            PrimaryWeaponInput(true);
        }
    }

        

    public void OnSecondary(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            SecondaryWeaponInput(true);
        }
    }

    public void OnTernary(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            TernaryWeaponInput(true);
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started || context.phase == InputActionPhase.Performed)
        {
            ReloadInput(true);
        }
        else { ReloadInput(false); }
    }
#endif
    private void ReloadInput(bool isPressed)
    {
        reload = isPressed;
    }

    private void ShootInput(bool state)
    {
        shoot = state;
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }
    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }
    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }
    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }
    public static void SetCursorState(bool newState)
    {
        cursorLocked = newState;
        Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }
    public static bool IsCursorLocked()
    {
        return cursorLocked;
    }
    private void TernaryWeaponInput(bool v)
    {
        ternaryWeapon = v;
    }
    private void SecondaryWeaponInput(bool v)
    {
        secondaryWeapon = v;
    }
    private void PrimaryWeaponInput(bool v)
    {
        primaryWeapon = v;
    }
    private static bool IsGamePaused()
    {
        return Time.timeScale == 0;
    }
}