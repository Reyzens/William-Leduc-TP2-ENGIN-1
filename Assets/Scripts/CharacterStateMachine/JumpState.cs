using UnityEngine;

public class JumpState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.2f;
    private float m_currentStateTimer = 0.0f;

    private Vector3 airControlInput = Vector3.zero;

    public override void OnEnter()
    {
        Debug.Log("Enter state: JumpState\n");
        m_stateMachine.Animator.SetTrigger("Jump");
        m_stateMachine.RB.AddForce(Vector3.up * m_stateMachine.JumpIntensity, ForceMode.Acceleration);
        m_currentStateTimer = STATE_EXIT_TIMER;
        m_stateMachine.m_playerCharacterPositionBeforeJump = m_stateMachine.RB.position;
        AudioManager.Instance.PlaySFX("Jump");
        VFXManager.Instance.PlayVFX("Jump");
    }

    public override void OnExit()
    {
        AudioManager.Instance.PlaySFX("Land");
        VFXManager.Instance.PlayVFX("Land");
        Debug.Log("Exit state: JumpState\n");
    }

    public override void OnFixedUpdate()
    {
        // Apply air control input to the character's velocity during FixedUpdate
        m_stateMachine.RB.velocity += airControlInput * Time.fixedDeltaTime;
    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
        if (m_stateMachine.IsInContactWithFloor())
        {
            m_stateMachine.m_playerCharacterPositionAfterJump = m_stateMachine.RB.position;
        }

        // Check for air control input here
        airControlInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        airControlInput = m_stateMachine.transform.TransformDirection(airControlInput);
        airControlInput *= m_stateMachine.m_airControlSpeed;
    }

    public override bool CanEnter(IState currentState)
    {
        if (currentState is FreeState)
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
        // This must be run in Update absolutely
        return false;
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0;
    }
}
