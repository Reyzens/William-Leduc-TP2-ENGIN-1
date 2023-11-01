using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatch : MonoBehaviour
{
    public CharacterControllerStateMachine m_CharacterRef;
    // Start is called before the first frame update
    public void EnableHitCollider()
    {

        m_CharacterRef.EnableAttackHitBox();

    }
    public void DisableHitCollider()
    {
        m_CharacterRef.DisableAttackHitBox();
    }
    public void EnableHitSound()
    {
        m_CharacterRef.EnableHitSound();
    }
    public void EnableHitVFX()
    {
        m_CharacterRef.EnableHitVFX();
    }
    public void EnableSlowMotion()
    {
        m_CharacterRef.EnableSlowMotion();
    }
    public void DisableSlowMotion() 
    {
        m_CharacterRef.DisableSlowMotion();
    }
}
