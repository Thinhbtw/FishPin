using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticClass
{
    //FISH
    public static int startHealth = 5;
    public static string Fish_clipDie = "Fish_Die";
    public static string Fish_clipWin = "Fish_Win";
    public static string Fish_clipHurt = "Fish_hurt";
    public static string Fish_clipBurn = "FIsh_Burn";
    public static string Fish_clipTele = "Fish_Tele";
    public static string Fish_clipIdle = "Fish_Idle";

    //BOSS
    public static float Boss_speed = 1;
    public static float Boss_minRange = 0.5f;
    public static float Boss_maxRange = 1.5f;
    public static string Boss_clipDie = "Boss_Die";
    public static string Boss_clipExplo = "Boss_Explo";
    public static string Boss_clipHurt = "Boss_Hurt";
    public static string Boss_clipIdle = "Boss_Idle";

    //SNAKE
    public static string Snake_clipDie = "Snake_Die";
    public static string Snake_clipExplo = "Snake_Explo";
    public static string Snake_clipHurt = "Snake_Hurt";
    public static string Snake_clipIdle = "Snake_Idle";


    //1TimePin
    public static string OneTimePin_clip = "PullRight";

    //2WayPin
    public static string TwoWayPin_clipRightToLeft = "Ver_Left";
    public static string TwoWayPin_clipLeftToRight = "Ver_right";

    //ReusePin
    public static string ReusePin_clipPull = "ReusePin_Pull";
    public static string ReusePin_clipPush = "ReusePin_Push";

    //BOMB
    public static string Bomb_Explo = "Bomb_Explosion";

    //POISON
    public static float Poison_speed = 1;

    //WIN
    public static int WinningCondition = 7;

    //PANEL
    public static string Pannel_clip = "Panel_Animation";

}
