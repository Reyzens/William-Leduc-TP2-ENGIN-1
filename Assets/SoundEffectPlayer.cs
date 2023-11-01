using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField]
    private List<SpecialEffectsSystems> specialEffectsSystems = new List<SpecialEffectsSystems>();
    [SerializeField]
    public AudioSource source;
    [SerializeField]
    public AudioClip sfxOnAttack, sfxOnDamaged, sfxOnJump, sfxOnLand;

    public void OnAttackHit()
    {
        source.clip = sfxOnAttack;
        source.Play();
    }
    public void OnDamagedTakend()
    {
        source.clip = sfxOnDamaged;
        source.Play();
    }

    public void OnJump()
    {
        source.clip = sfxOnJump;
        source.Play();
    }
    public void OnLanding()
    {
        source.clip = sfxOnLand;
        source.Play();
    }

    public void InstantiateFX(EFX_Type sfxType)
    {
        switch(sfxType)
        {
            case EFX_Type.Attack:
                OnAttackHit();
                break;
            case EFX_Type.Hit:
                OnDamagedTakend();
                break;
            case EFX_Type.Jump:
                OnJump();
                break;
            case EFX_Type.Land:
                OnLanding();
                break;
            default: 
                break;
        }
    }

    public enum EFX_Type
    {
        Attack,
        Hit,
        Jump,
        Land,
        Count
    }

    [System.Serializable]

    public struct SpecialEffectsSystems
    {
        public EFX_Type type;
        public List<AudioClip> clipList;
        public List<ParticleSystem> particleSystems;
    }
}

   
