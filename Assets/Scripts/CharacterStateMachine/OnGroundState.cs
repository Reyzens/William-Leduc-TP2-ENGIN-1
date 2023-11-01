using UnityEngine;

public class OnGroundState : CharacterState
{
    private const float STATE_EXIT_TIMER = 3f;
    private float m_currentStateTimer = 0.0f;

    public override void OnEnter()
    {
        Debug.Log("Enter state: Ground\n");
        m_stateMachine.Animator.SetBool("Die", true);
        m_currentStateTimer = STATE_EXIT_TIMER;
        


    }

    public override void OnExit()
    {
        Debug.Log("Exit state: Ground\n");
        m_stateMachine.Animator.SetBool("Die", false);
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
        
    }

    public override bool CanEnter(IState currentState)
    {
        if(currentState is JumpState)
        {
            if (m_stateMachine.CharacterJumpDistance() >= 3.0f)
            {
                return true;
            }
        }
        if (currentState is StunnedState)
        {
            if (m_stateMachine.IsInContactWithFloor())
            {
                return true;
            }
        }
        if (currentState is FallingState)
        {
            if (m_stateMachine.CharacterJumpDistance() >= 3.0f)
            {
                return true;
            }
        }

            return false;
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0;
    }
}
