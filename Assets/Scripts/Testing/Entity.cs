using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Entity : MonoBehaviour
{
    public string entityName;

    [Header("Controls")]
    public Rigidbody2D rb;
    private Vector2 movementVelocity;
    public Vector2 facingDirection { get; private set; }
    public InputActionReference interact;
    public InputActionReference dash;
    public InputActionReference movement;
    public InputActionReference normal;
    public InputActionReference special;
    public InputActionReference ultimate;

    [Header("Stats")]
    public int hitPoints;
    public float speed;
    public int attack;
    public float interruptDuration;

    [Header("Dash")]
    private Vector2 dashVelocity;
    public float dashSpeed;
    public int dashCount;
    public int maxDashCount;
    public float dashDuration;
    public float dashRechargeTime;

    [Header("Charge")]
    public float charge;
    public float recharge; // Multiplier on charge received

    public enum ActionState
    {
        Idle,
        Dash,
        Windup,
        Recovery,
        Interrupted,
    }

    [Header("Action State")]
    public ActionState state;
    public AbilityObject activeAbility;
    public float actionTimer; // Remaining time for current action state that is not idle

    public bool canMoveDuringWindup;
    public bool canMoveDuringRecovery;
    public bool OutOfCombat;

    [Header("Abilities")]
    public AbilityObject normalAbility;
    public AbilityObject specialAbility;
    public AbilityObject ultimateAbility;

    [Header("Cooldowns")]
    public float normalCooldown;
    public float specialCooldown;
    public float ultimateCooldown;
    public float dashRechargeTimer;

    [Header("Effects")]
    public List<PassiveObject> passives = new();
    public List<StatusObject> statuses = new();

    /* ---------------- INPUT METHODS ---------------- */

    public void OnInteract()
    {
        Debug.Log($"{entityName} interacted.");

        if (!CanStartAbility()) return;
        // TODO
        return;
    }

    public void OnDash()
    {
        if (state == ActionState.Recovery || state == ActionState.Interrupted) return;
        if (dashCount <= 0) return;
        if (state == ActionState.Dash) return;

        state = ActionState.Dash;
        activeAbility = null;
        actionTimer = dashDuration;
        dashVelocity = facingDirection * speed * dashSpeed;
        dashCount--;

        if (dashRechargeTimer <= 0f) dashRechargeTimer = dashRechargeTime;
    }

    public void OnNormal()
    {
        if (normalAbility == null) return;
        if (!CanStartAbility()) return;
        if (normalCooldown > 0f) return;

        StartAbility(normalAbility);
        normalCooldown = normalAbility.cooldown;
    }

    public void OnSpecial()
    {
        if (specialAbility == null) return;
        if (!CanStartAbility()) return;
        if (specialCooldown > 0f) return;

        StartAbility(specialAbility);
        specialCooldown = specialAbility.cooldown;
    }

    public void OnUltimate()
    {
        if (ultimateAbility == null) return;
        if (!CanStartAbility()) return;
        if (ultimateCooldown > 0f) return;
        //if (charge < ultimateAbility.chargeCost) return;

        StartAbility(ultimateAbility);
        ultimateCooldown = ultimateAbility.cooldown;
        charge = 0f;
    }

    bool CanStartAbility()
    {
        return (state == ActionState.Idle || state == ActionState.Dash);
    }

    /* ---------------- PLAYER INPUT ---------------- */

    private void OnEnable()
    {
        interact.action.started += OnInteractCallback;
        normal.action.started += OnNormalCallback;
        special.action.started += OnSpecialCallback;
        ultimate.action.started += OnUltimateCallback;
        dash.action.started += OnDashCallback;
    }

    private void OnDisable()
    {
        interact.action.started -= OnInteractCallback;
        normal.action.started -= OnNormalCallback;
        special.action.started -= OnSpecialCallback;
        ultimate.action.started -= OnUltimateCallback;
        dash.action.started -= OnDashCallback;
    }

    private void OnInteractCallback(InputAction.CallbackContext context)
    {
        OnInteract();
    }

    private void OnNormalCallback(InputAction.CallbackContext context)
    {
        OnNormal();
    }

    private void OnSpecialCallback(InputAction.CallbackContext context)
    {
        OnSpecial();
    }

    private void OnUltimateCallback(InputAction.CallbackContext context)
    {
        OnUltimate();
    }

    private void OnDashCallback(InputAction.CallbackContext context)
    {
        OnDash();
    }

    /* ---------------- CORE LOGIC ---------------- */

    private void Start()
    {
        facingDirection = Vector2.up;
    }

    void StartAbility(AbilityObject ability)
    {
        if (ability == null) return;

        activeAbility = ability;
        state = ActionState.Windup;
        //actionTimer = ability.windup;
    }

    void ExecuteActiveAbility()
    {
        if (activeAbility == null) return;

        activeAbility.Execute();
    }

    public void Interrupt()
    {
        //if (!activeAbility.canBeInterrupted) return;
        if (state == ActionState.Interrupted) return;
        if (state == ActionState.Dash) return;

        state = ActionState.Interrupted;
        activeAbility = null;
        actionTimer = interruptDuration;
    }

    /* ---------------- UPDATE ---------------- */

    void Update()
    {
        float deltaTime = Time.deltaTime;

        // Move Entity
        movementVelocity = movement.action.ReadValue<Vector2>();
        bool hasMovementInput = movementVelocity.sqrMagnitude > 0.01f;
        if (hasMovementInput) facingDirection = movementVelocity.normalized;
        rb.linearVelocity = (state == ActionState.Dash)
            ? new Vector2(dashVelocity.x, dashVelocity.y)
            : new Vector2(movementVelocity.x * speed, movementVelocity.y * speed);

        // Update Player Inputs
        UpdateActionState(deltaTime);
        UpdateCooldowns(deltaTime);
        UpdateDashRecharge(deltaTime);
    }

    void UpdateActionState(float deltaTime)
    {
        if (state == ActionState.Idle) return;

        actionTimer = Mathf.Max(0f, actionTimer - deltaTime);
        if (actionTimer > 0f) return;

        switch (state)
        {
            case ActionState.Windup:
                ExecuteActiveAbility();
                state = ActionState.Recovery;
                //actionTimer = activeAbility.recovery;
                break;

            case ActionState.Recovery:
                state = ActionState.Idle;
                activeAbility = null;
                break;

            case ActionState.Interrupted:
                state = ActionState.Idle;
                break;

            case ActionState.Dash:
                state = ActionState.Idle;
                break;
        }
    }

    void UpdateDashRecharge(float deltaTime)
    {
        dashRechargeTimer = Mathf.Max(0f, dashRechargeTimer-deltaTime);
        if (dashRechargeTimer > 0f) return;

        if (dashCount < maxDashCount)
            dashCount = Mathf.Min(dashCount + 1, maxDashCount);

        if (dashCount < maxDashCount)
            dashRechargeTimer = dashRechargeTime;
    }

    void UpdateCooldowns(float deltaTime)
    {
        normalCooldown = Mathf.Max(0f, normalCooldown - deltaTime);
        specialCooldown = Mathf.Max(0f, specialCooldown - deltaTime);
        ultimateCooldown = Mathf.Max(0f, ultimateCooldown - deltaTime);
    }
}