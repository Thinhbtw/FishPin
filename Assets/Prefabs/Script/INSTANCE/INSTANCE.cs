using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INSTANCE : MonoBehaviour
{
    public static string PinPull = "pull";
    public static string PinSlide = "slide";
    public static string waterTag = "water";
    public static string lavaTag = "lava";
    public static string fishTag = "fish";
    public static string mapTag = "map";
    public static string snakeTag = "snake";
    public static string evilFishTag = "evilFish";
    public static float interactOffset = 0.2f;
    public static float interactSoundOffset = 1.5f;
    //pin anim
    public static string pullAnim = "pullingPin";
    public static string GreenPinIn = "greenPinInAnim";
    public static string GreenPinOut = "greenPinOutAnim";
    //fish animation
    public static string IdleFish = "fishAnim";
    public static string HurtFish = "fish_Hurt";
    public static string DeadFish = "fish_Die";
    public static string WinFish = "fish_Win";
    public static string WintFishJump = "fish_Win_jumpUp";

    public static float deadfishTime = 0.583f;
    public static float winfishTime = 3f;
    public static float hurtfishTime = 0.5f;

    //evil animation
    public static string IdleEvilFish = "evilFishAnim";
    public static string evilAttack = "evilFishSpecialAttack";
    public static string evilDead = "evilDeadAnim";
    public static string evilHurt = "evilHurtAnim";

    //Panel Anim
    public static string panelAnim = "panelAnim";
    public static string panelOut = "panelOutAnim";
    public static string openingImage = "openingImgAnim";
    public static string WinText = "WinTextAnim";
    public static string LoseText = "LoseAnim";
    public static string buttonAnim = "continueButton";

    //deadPanel Anim
    public static string deadPanel = "DeadPanelAnim";

    //bombAnim
    public static string bombAnim = "explodeAnim";


    //layer
    public static int fluid = 6;
    public static int Lavafluid = 7;
    public static int Fish = 3;

    public static bool isPlayAble = true;

    //gif amount
    // gem amount
    public static int gem_high = 100;
    public static int gem_medium = 50;
    public static int gem_low = 10;

    // gold amount
    public static int gold_high = 2000;
    public static int gold_medium = 1000;
    public static int gold_low = 500;

}
