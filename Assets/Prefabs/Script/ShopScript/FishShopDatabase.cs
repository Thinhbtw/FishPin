using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishShopDatabase", menuName ="Shopping/Fish shop database")]
public class FishShopDatabase : ScriptableObject
{
    public Fish[] fish;
    public int FishCount
    {
        get { return fish.Length; }
    }

    public Fish GetFish(int index)
    {
        return fish[index];
    }

    public void PurchaseFish(int index)
    {
        fish[index].isPurchased = true;
    }
}
