using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Playables;

public class GameManagerSM : BaseStateMachine<IState>
{
    [SerializeField]
    protected Cinemachine.CinemachineVirtualCamera m_gameplayCamera;
    [SerializeField]
    protected Cinemachine.CinemachineVirtualCamera m_cinematicCamera;
    [SerializeField]
    protected Cinemachine.CinemachineDollyCart m_cinematicCart;
    [SerializeField]
    protected CharacterControllerStateMachine m_CharacterRef;
    [SerializeField]
    protected PlayableDirector m_intro;

    protected override void CreatePossibleStates()
    {
        m_possibleStates = new List<IState>();
        m_possibleStates.Add(new CinematicState(m_cinematicCamera, m_cinematicCart, m_CharacterRef,m_intro));
        m_possibleStates.Add(new GameplayState(m_gameplayCamera));
        
        
    }

    public void EnableIntro()
    {
        m_intro.Play();
    }

    public void EndIntro()
    {
        m_intro.Play();
        
    }
}