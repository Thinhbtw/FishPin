using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip click, bombExplo, purchased, confeti;
    static AudioSource audioSrc;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        click = Resources.Load<AudioClip>("Audio/Click");
        bombExplo = Resources.Load<AudioClip>("Audio/BOMB 1");
        purchased = Resources.Load<AudioClip>("Audio/BuyItem");
        confeti = Resources.Load<AudioClip>("Audio/PHAO HOA");
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "click":
                audioSrc.PlayOneShot(click);
                break;
            case "bombExplo":
                audioSrc.PlayOneShot(bombExplo);
                break;
            case "purchased":
                audioSrc.PlayOneShot(purchased);
                break;
            case "confeti":
                audioSrc.PlayOneShot(confeti);
                break;
        }
    }

}
