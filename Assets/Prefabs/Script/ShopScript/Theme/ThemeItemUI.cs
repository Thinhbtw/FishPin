
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ThemeItemUI : MonoBehaviour
{

    [Space(20f)]
    [SerializeField] Image themeImage;
    [SerializeField] Text themePrice;
    [SerializeField] Button purchaseBtn;
    [SerializeField] Button itemButton;
    [SerializeField] Text curCondition;
    [SerializeField] Image currencyImage;
    public void SetThemeImage(Sprite sprite)
    {
        themeImage.sprite = sprite;
    }

    public void SetThemeCurrency(Sprite sprite)
    {
        currencyImage.sprite = sprite;
    }
    public void SetThemePrice(int price)
    {
        if (price >= 1000)
            themePrice.text = string.Format("{0}K", (price / 1000), GetFirstDigitFromNumber(price % 1000));
        else
            themePrice.text = price.ToString();

    }

    public void SetThemeAsPurchased()
    {
        purchaseBtn.gameObject.SetActive(false);
        itemButton.interactable = true;
    }

    public void OnItemPurchased(int itemIndex, UnityAction<int> action)
    {
        purchaseBtn.onClick.RemoveAllListeners();
        purchaseBtn.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    public void OnItemSelected(int itemIndex, UnityAction<int> action)
    {
        itemButton.interactable = true;
        itemButton.onClick.RemoveAllListeners();
        itemButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    public void SelectItem()
    {
        currencyImage.enabled = false;
        themePrice.text = "Using";
        itemButton.interactable = false;
    }

    public void DeselectItem()
    {
        themePrice.text = "Owned";
        itemButton.interactable = true;
    }

    int GetFirstDigitFromNumber(int num)
    {
        return int.Parse(num.ToString()[0].ToString());
    }
}
