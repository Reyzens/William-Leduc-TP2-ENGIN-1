using UnityEngine;
using UnityEngine.EventSystems;

public class FreeState : CharacterState
{
    public override void OnEnter()
    {
        Debug.Log("Enter state: Cinematic\n");
        
    }

    public override void OnUpdate()
    {
        // Reset the move direction vector.
        var vectorOnFloorFront = Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, Vector3.up);
        var vectorOnFloorBack = Vector3.ProjectOnPlane(-m_stateMachine.Camera.transform.forward, Vector3.down);
        var vectorOnFloorLeft = Vector3.ProjectOnPlane(-m_stateMachine.Camera.transform.right, Vector3.down);
        var vectorOnFloorRigth = Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.right, Vector3.up);
        

        vectorOnFloorFront.Normalize();
        vectorOnFloorBack.Normalize();
        vectorOnFloorLeft.Normalize();
        vectorOnFloorRigth.Normalize();
        
        if(m_stateMachine.RB.velocity.magnitude > 0)
        m_stateMachine.m_movementPositionVector = Vector3.zero;

        
        if (Input.GetKey(KeyCode.W))
        {
            m_stateMachine.m_movementPositionVector += vectorOnFloorFront;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_stateMachine.m_movementPositionVector += vectorOnFloorBack;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_stateMachine.m_movementPositionVector += vectorOnFloorLeft;
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_stateMachine.m_movementPositionVector += vectorOnFloorRigth;
        }
        if (m_stateMachine.m_movementPositionVector != Vector3.zero)
        {
            m_stateMachine.m_movementPositionVector.Normalize();
        }
    }

    public override void OnFixedUpdate()
    {    
        m_stateMachine.RB.AddForce(m_stateMachine.m_movementPositionVector * m_stateMachine.AccelerationValue, ForceMode.Acceleration);
        m_stateMachine.RB.velocity -= m_stateMachine.SlowingValue * m_stateMachine.RB.velocity;

        if (m_stateMachine.RB.velocity.magnitude > m_stateMachine.MaxFowardVelocity)
        {
            m_stateMachine.RB.velocity = m_stateMachine.RB.velocity.normalized;
            m_stateMachine.RB.velocity *= m_stateMachine.MaxFowardVelocity;
        }
        m_stateMachine.UpdateMovementAnimationValues();
        
        //TODO 31 AOÛT:
        //Appliquer les déplacements relatifs à la caméra dans les 3 autres directions
        //Avoir des vitesses de déplacements maximales différentes vers les côtés et vers l'arrière
        //Lorsqu'aucun input est mis, décélérer le personnage rapidement

        //Debug.Log(m_stateMachine.RB.velocity.magnitude);
    }

    public override void OnExit()
    {
        Debug.Log("Exit state: Cinematic\n");
    }

    public override bool CanEnter(IState currentState)
    {
        if (currentState is JumpState)
        {
            if (m_stateMachine.IsInContactWithFloor() && m_stateMachine.CharacterJumpDistance() <= 3.0f) 
            {  return true; }
        }
        if (currentState is AttackingState)
        {
            //CONDITIONS
            return m_stateMachine.IsAttack();
        }
        if (currentState is OnHitState)
        {
            //CONDITIONS
            return m_stateMachine.IsHit();
        }
        if (currentState is FallingState) 
        {
            //CONDITIONS
            return m_stateMachine.IsInContactWithFloor();
        }
        if(currentState is OnGettingUpState) 
        {
            //CONDITIONS
            return true;
        }
        if(currentState is StunnedState) 
        {
            //CONDITIONS
            return m_stateMachine.IsStunned();
        }
       
        return false;
    }
    
    public override bool CanExit()
    {
        return true;
    }
}
