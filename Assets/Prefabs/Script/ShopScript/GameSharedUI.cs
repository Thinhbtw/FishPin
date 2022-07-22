using UnityEngine;
using UnityEngine.UI;

public class GameSharedUI : MonoBehaviour
{

    #region Singleton class: GameSharedUI

    public static GameSharedUI instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;

    }

    #endregion

    [SerializeField] Text[] coinUIText;
    [SerializeField] Text[] gemsUIText;


    public void UpdateCoinsUIText()
    {
        for(int i = 0; i < coinUIText.Length; i++)
        {
            SetCoinsText(coinUIText[i], GameData.GetCoin());

        }
    }

    void SetCoinsText(Text text, int value)
    {
        if (value >= 10000)
        {
            text.text = ((value >= 10550 ? value - 50 : value) / 1000D).ToString("0.#k");
        }
        else if (value >= 1000)
            text.text = value >= 1000 ? ((value >= 1005 ? value - 5 : value) / 1000D).ToString("0.##k") : value.ToString("#,0");
        else if (value % 1000 == 0)
            text.text = string.Format("{0}K", (value / 1000), GetFirstDigitFromNumber(value % 1000));
        else
            text.text = value.ToString();
    }

    public void UpdateGemsUIText()
    {
        for (int i = 0; i < gemsUIText.Length; i++)
        {
            SetCoinsText(gemsUIText[i], GameData.GetGems());
        }
    }


    int GetFirstDigitFromNumber(int num)
    {
        return int.Parse(num.ToString() [0].ToString());
    }


}
