using Cinemachine;
using UnityEngine;

public class OnHitState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.7f;
    private float m_currentStateTimer = 0.0f;
    protected Cinemachine.CinemachineVirtualCamera m_camera;

    public OnHitState(Cinemachine.CinemachineVirtualCamera camera)
    {
        m_camera = camera;
    }


    public override void OnEnter()
    {
        Debug.Log("Enter state: Hit\n");
        m_currentStateTimer = STATE_EXIT_TIMER;
        m_stateMachine.Animator.SetTrigger("OnHit");
        m_camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1;
        m_camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
        AudioManager.Instance.PlaySFX("Punch");


    }

    public override void OnExit()
    {
        m_camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        m_camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        Debug.Log("Exit state: Hit\n");
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
        if(currentState is FreeState)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                return true;
            }
            m_stateMachine.IsHit();
        }
        if(currentState is AttackingState)   
        {
            m_stateMachine.IsHit();
        }
        return false;
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0;
    }
}
