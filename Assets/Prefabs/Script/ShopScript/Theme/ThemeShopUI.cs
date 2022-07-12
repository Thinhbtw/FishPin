using UnityEngine;

public class ThemeShopUI : MonoBehaviour
{
    [SerializeField] GameObject itemContent;
    [SerializeField] GameObject themeItemPrefab;
    [SerializeField] ThemeShopDatabase themeDB;

    private void Awake()
    {
        GenerateShopItemUI();
    }

    void GenerateShopItemUI()
    {
        for (int i = 0; i < themeDB.ThemesCount; i++)
        {
            Theme theme = themeDB.GetTheme(i);
            ThemeItemUI uiItem = Instantiate(themeItemPrefab, itemContent.transform).GetComponent<ThemeItemUI>();

            uiItem.gameObject.name = "Item " + i;

            uiItem.SetThemeImage(theme.image);
            uiItem.SetThemeCurrency(theme.currencyImage);
            uiItem.SetThemePrice(theme.price);

            if(!theme.isPurchased)
            {
                uiItem.SetThemePrice(theme.price);
                uiItem.OnItemPurchased(i, OnItemPurchased);
            }
        }
    }

    void OnItemSelected(int index)
    {
        Debug.Log("select " + index);
    }

    void OnItemPurchased(int index)
    {
        Debug.Log("purchase " + index);
    }


}
