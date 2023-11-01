using Unity.VisualScripting;
using UnityEngine;

public class AttackingState : CharacterState
{
    private float m_currentStateTimer = 0.0f;

    public override void OnEnter()
    {
        Debug.Log("Enter state: Attacking\n");
        m_stateMachine.Animator.SetTrigger("OnAttack");
        


    }

    public override void OnExit()
    {
        m_stateMachine.DisableAttackHitBox();
        Debug.Log("Exit state: Attacking\n");


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
        if (currentState is FreeState)
        {
            return Input.GetMouseButtonDown(0);
        }
        return false;
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0;
    }

    
}
