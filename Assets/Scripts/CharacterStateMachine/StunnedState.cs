using UnityEngine;

public class StunnedState : CharacterState
{
    private const float STATE_EXIT_TIMER = 3f;
    private float m_currentStateTimer = 0.0f;

    public override void OnEnter()
    {

        Debug.Log("Enter state: Stun\n");
        
        m_stateMachine.Animator.SetBool("STUN",true);
        m_currentStateTimer = STATE_EXIT_TIMER;

    }

    public override void OnExit()
    {
        Debug.Log("Exit state: Stun\n");
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
        if(m_currentStateTimer < 0.0f)
        {
            m_stateMachine.Animator.SetBool("STUN", false);
        }
    }

    public override bool CanEnter(IState currentState)
    {
        if (Input.GetKeyDown(KeyCode.E))
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
