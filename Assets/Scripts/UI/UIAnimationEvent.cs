using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationEvent : MonoBehaviour
{
    public GameObject MihoyoLogo;
    public GameObject GameStart_;

    public GameObject FadeIn_;
    public AudioSource startAudio;
    public AudioSource QuitAudio;

    public void GameStart()
    {
        GameStart_.SetActive(true);
        MihoyoLogo.SetActive(false);
    }

    public void FadeIn()
    {
        FadeIn_.SetActive(true);
    }

    public void ToGameScene()
    {
        GameManager.Instance.SetIsStart(true);
    }

    public void OffGameStart_()
    {
        GameStart_.SetActive(false);
        ToGameScene();
    }

    public void PlayStartSound()
    {
        startAudio.Play();
    }

    public void PlayQuitSound()
    {
        QuitAudio.Play();
    }
}
