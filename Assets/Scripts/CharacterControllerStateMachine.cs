using System.Collections.Generic;
using UnityEngine;


public class CharacterControllerStateMachine : BaseStateMachine<CharacterState>
{
    public Camera Camera { get; private set; }
    [field: SerializeField]
    public Rigidbody RB { get; private set; }
    [field: SerializeField]
    public Animator Animator { get; private set; }
    [field: SerializeField]
    public float AccelerationValue { get; private set; }
    [field: SerializeField]
    public float SlowingValue { get; private set; }
    [field: SerializeField]
    public float MaxFowardVelocity { get; private set; }
    [field: SerializeField]
    public float MaxSidedVelocity { get; private set; }
    [field: SerializeField]
    public float MaxBackward { get; private set; }
    [field: SerializeField]
    public float JumpIntensity { get; private set; }
    [field: SerializeField]
    public bool InCinematic;
    [SerializeField]
    public Cinemachine.CinemachineVirtualCamera m_camera;

    public HitBox m_Box;
    public Vector3 m_movementPositionVector = Vector3.zero;
    public Vector3 m_playerCharacterPositionBeforeJump = Vector3.zero;
    public Vector3 m_playerCharacterPositionAfterJump = Vector3.zero;
    public float m_airControlSpeed;
    public bool m_EnableAttackEvent;

    [SerializeField]
    private CharacterFloorTrigger m_floorTrigger;




    protected override void CreatePossibleStates()
    {
        m_possibleStates = new List<CharacterState>();
        m_possibleStates.Add(new FreeState());
        m_possibleStates.Add(new JumpState());
        m_possibleStates.Add(new FallingState());
        m_possibleStates.Add(new OnHitState(m_camera));
        m_possibleStates.Add(new OnGroundState());
        m_possibleStates.Add(new AttackingState());
        m_possibleStates.Add(new OnGettingUpState());
        m_possibleStates.Add(new StunnedState());
        

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        foreach (CharacterState state in m_possibleStates)
        {
            state.OnStart(this);
        }
        m_currentState = m_possibleStates[0];
        m_currentState.OnEnter();

        Camera = Camera.main;

        InCinematic = true;
    }

    protected override void Update()
    {
        if(InCinematic == true)
        {

            return;
        }
        else
        {
            Animator.SetBool("TouchGround", m_floorTrigger.IsOnFloor);
            m_currentState.OnUpdate();
            CharacterJumpDistance();
            TryStateTransition();
            base.Update();
        }
        
        //UpdateAnimatorValues();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        //SetDirectionalInputs();
        base.FixedUpdate();
        //Set2dRelativeVelocity();
    }



    public bool IsInContactWithFloor()
    {
        return m_floorTrigger.IsOnFloor;
    }

    public bool IsAttacking()
    {
        return Animator.GetBool("IsAttacking");
    }

    public bool IsStunned()
    {
        return true;
    }

    public bool IsHit()
    {

        return true;
    }

    public bool IsAttack()
    {
        return true;
    }

    public float CharacterJumpDistance()
    {
        float dist = m_playerCharacterPositionBeforeJump.y - m_playerCharacterPositionAfterJump.y;

        return dist;

    }

    public void UpdateMovementAnimationValues()
    {
        Animator.SetFloat("MoveX", RB.velocity.x);
        Animator.SetFloat("MoveY", RB.velocity.z);

    }

    public void EnableAttackHitBox()
    {

        m_Box.EnableHitCollider();
    }
    public void DisableAttackHitBox()
    {

        m_Box.DisableHitCollider();
    }
    public void EnableHitSound()
    {
        m_Box.EnableHitSound();
    }
    public void EnableHitVFX()
    {
        m_Box.EnableHitVFX();
    }
    public void EnableSlowMotion()
    {
        m_Box.EnableSlowMotion();
    }
    public void DisableSlowMotion()
    {
        m_Box.DisableSlowMotion();
    }

}

 
