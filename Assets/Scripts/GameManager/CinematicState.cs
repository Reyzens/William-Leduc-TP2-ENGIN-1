using UnityEngine;
using UnityEngine.Playables;

public class CinematicState : IState
{
    protected Cinemachine.CinemachineVirtualCamera m_camera;
    
    protected Cinemachine.CinemachineDollyCart m_cinematicCart;

    public CharacterControllerStateMachine m_CharacterRef;

    public PlayableDirector m_intro;

    public CinematicState(Cinemachine.CinemachineVirtualCamera camera, Cinemachine.CinemachineDollyCart cart, CharacterControllerStateMachine CCSM, PlayableDirector intro)
    {
        if (camera == null)
        {
            Debug.LogError("CinemachineVirtualCamera is null.");
            return;
        }

        if (cart == null)
        {
            Debug.LogError("CinemachineDollyCart is null.");
            return;
        }

        if (CCSM == null)
        {
            Debug.LogError("CinemachineDollyCart is null.");
            return;
        }
        m_camera = camera;
        m_cinematicCart = cart;
        m_CharacterRef = CCSM;
        m_intro = intro;
    }

    public bool CanEnter(IState currentState)
    {
        return Input.GetKeyDown(KeyCode.G);
    }

    public bool CanExit()
    {
        return Input.GetKeyDown(KeyCode.G);
    }

    public void OnEnter()
    {
        m_intro.Play();
        m_CharacterRef.InCinematic = true;
        Debug.Log("On Enter CinematicState");
        if (m_cinematicCart.m_Position != 0)
        {
            m_cinematicCart.m_Position = 0;
        }
        m_camera.enabled = true;
        
    }

    public void OnExit()
    {
        m_intro.Stop();
        Debug.Log("On Exit CinematicState");
        m_CharacterRef.InCinematic = false;
        m_camera.enabled = false;
    }

    public void OnFixedUpdate()
    {
    }

    public void OnStart()
    {
    }

    public void OnUpdate()
    {
    }
}
