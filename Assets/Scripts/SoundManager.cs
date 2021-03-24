using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    
    public AudioSource GameBGM;
    public AudioSource AttackAudio;
    public AudioSource DamagedAudio;
    public AudioSource OKAudio;
    public AudioSource QuitAudio;
    public AudioSource SwitchAudio;
    public AudioSource CompleteAudio;

    private void Awake()
    {
        instance = this;

        GameBGM.volume = 1.0f;
        AttackAudio.volume = 1.0f;
        DamagedAudio.volume = 1.0f;
        OKAudio.volume = 1.0f;
        QuitAudio.volume = 1.0f;
        SwitchAudio.volume = 1.0f;
    }

    public void PlayQuitSound()
    {
        QuitAudio.Play();
    }

    public void PlayAttackSound()
    {
        AttackAudio.Play();
    }

    public void PlayDamagedSound()
    {
        DamagedAudio.Play();
    }

    public void PlayOKSound()
    {
        OKAudio.Play();
    }

    public void PlaySwitchSound()
    {
        SwitchAudio.Play();
    }

    public void PlayCompleteSound()
    {
        CompleteAudio.Play();
    }
}
