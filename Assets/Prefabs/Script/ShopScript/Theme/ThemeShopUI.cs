using UnityEngine;

public class ThemeShopUI : MonoBehaviour
{
    [SerializeField] GameObject itemContent;
    [SerializeField] GameObject themeItemPrefab;
    [SerializeField] ThemeShopDatabase themeDB;

    int newSelectedItemIndex = 0;
    int previousSelectedItemIndex = 0;


    private void Awake()
    {
        GenerateShopItemUI();
        SetSelectedTheme();
        SelectedItemUI(GameData.GetSelectedThemeIndex()); 
        ChangeTheme();
        if(PlayerPrefs.HasKey("Progress") == false)
        {
            Debug.Log("Set");
        }
    }

    void SetSelectedTheme()
    {
        int index = GameData.GetSelectedThemeIndex();

        GameData.SetSelectedTheme(themeDB.GetTheme(index), index);
    }

    void GenerateShopItemUI()
    {
        for(int i = 0; i < GameData.GetAllPurchasedTheme().Count; i++)
        {
            int purchasedThemeIndex = GameData.GetPurchasedTheme(i);
            themeDB.PurchaseTheme(purchasedThemeIndex);
        }
        for (int i = 0; i < themeDB.ThemesCount; i++)
        {
            int num = i;
            Theme theme = themeDB.GetTheme(i);
            ThemeItemUI uiItem = Instantiate(themeItemPrefab, itemContent.transform).GetComponent<ThemeItemUI>();

            uiItem.gameObject.name = "Item " + i;

            uiItem.SetThemeImage(theme.image);
            uiItem.SetThemeCurrency(theme.currencyImage);
            uiItem.SetThemePrice(theme.price);

            if(theme.isPurchased)
            {
                uiItem.SetThemeAsPurchased();
                uiItem.OnItemSelected(i, OnItemSelected);
            }
            else
            {
                uiItem.OnItemPurchased(i, OnItemPurchased);
            }
        }
        
    }

    void ChangeTheme()
    {
        Theme theme = GameData.GetSelectedTheme();
    }

    void OnItemSelected(int index)
    {
        SoundManager.PlaySound("click");
        SelectedItemUI(index);

        GameData.SetSelectedTheme(themeDB.GetTheme(index), index);
        ChangeTheme();
    }

    void SelectedItemUI(int itemIndex)
    {
        
        previousSelectedItemIndex = newSelectedItemIndex;
        newSelectedItemIndex = itemIndex;

        ThemeItemUI prevUiItem = GetItemUI(previousSelectedItemIndex);
        ThemeItemUI newUiItem = GetItemUI(newSelectedItemIndex);

        prevUiItem.DeselectItem();
        newUiItem.SelectItem();
    }

    ThemeItemUI GetItemUI(int itemIndex)
    {
        return itemContent.transform.GetChild(itemIndex).GetComponent<ThemeItemUI>();
    }


    void OnItemPurchased(int index)
    {

        Theme theme = themeDB.GetTheme(index);
        ThemeItemUI uiItem = GetItemUI(index);

        if (GameData.CanSpendGems(theme.price))
        {
            SoundManager.PlaySound("purchased");
            //Proceed with the purchase operation
            GameData.SpendGems(theme.price);

            //Update Coins UI text
            GameSharedUI.instance.UpdateCoinsUIText();

            //Update DB's Data
            themeDB.PurchaseTheme(index);

            uiItem.SetThemeAsPurchased();
            uiItem.OnItemSelected(index, OnItemSelected);

            //Add purchased item to Shop Data
            GameData.AddPurchasedThemes(index);

        }
        else
        {
            SoundManager.PlaySound("click");
            //No enough coins..
            Debug.Log("ko du gems");
        }
    }


}
