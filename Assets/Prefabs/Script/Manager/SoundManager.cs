using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip click, bombExplo, purchased, confeti, fail, success, fizz, bonk, reward;
    static AudioSource audioSrc;
    [SerializeField] GameObject backgroundAudio;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        click = Resources.Load<AudioClip>("Audio/Click");
        bombExplo = Resources.Load<AudioClip>("Audio/BOMB 1");
        purchased = Resources.Load<AudioClip>("Audio/BuyItem");
        confeti = Resources.Load<AudioClip>("Audio/PHAO HOA");
        fail = Resources.Load<AudioClip>("Audio/FAIL");
        success = Resources.Load<AudioClip>("Audio/SUCCESS");
        fizz = Resources.Load<AudioClip>("Audio/fizz");
        bonk = Resources.Load<AudioClip>("Audio/bonk");
        reward = Resources.Load<AudioClip>("Audio/rewardSound");
    }

    private void Update()
    {
        if(!PlayerPrefs.GetString("SaveSettings").Contains("0"))
        {
            backgroundAudio.SetActive(false);
        }
        else
        {
            backgroundAudio.SetActive(true);
        }
    }

    public static void PlaySound(string clip)
    {
        if (PlayerPrefs.GetString("SaveSettings").Contains("0"))
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
                case "fail":
                    audioSrc.PlayOneShot(fail);
                    break;
                case "success":
                    audioSrc.PlayOneShot(success);
                    break;
                case "fizz":
                    audioSrc.PlayOneShot(fizz);
                    break;
                case "bonk":
                    audioSrc.PlayOneShot(bonk);
                    break;
                case "reward":
                    audioSrc.PlayOneShot(reward);
                    break;
            }
        }
    }

}
