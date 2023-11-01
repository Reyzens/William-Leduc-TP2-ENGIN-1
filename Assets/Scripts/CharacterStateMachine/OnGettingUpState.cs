using UnityEngine;

public class OnGettingUpState : CharacterState
{
    private float m_currentStateTimer = 0.0f;
    private const float STATE_EXIT_TIMER = 1f;

    public override void OnEnter()
    {
        Debug.Log("Enter state: Up\n");
        m_currentStateTimer = STATE_EXIT_TIMER;
        m_stateMachine.Animator.SetTrigger("OnUp");


    }

    public override void OnExit()
    {
        Debug.Log("Exit state: Up\n");
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
        if (currentState is OnGroundState)
        {
            return true;
        }
        

        return false;
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0;
    }
}
