using Jackal;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject panelSetting, panelApp;

    void Start()
    {
        OpenApp();
    }

    public void BuyPack()
    {
        IAPManager.OnPurchaseSuccess =
            () =>
            {
                GameDataManager.Instance.playerData.UnlockPack();
                OpenApp();
            };
    }

    public void OpenApp()
    {
        panelApp.SetActive(true);
    }
}