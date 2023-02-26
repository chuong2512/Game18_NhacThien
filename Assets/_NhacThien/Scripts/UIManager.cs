using Jackal;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject panelSetting, panelApp;
    /// <summary>
    /// ////////////////////////////////////////////
    /// </summary>
    void Start()
    {
        OpenApp();
    }
    /// <summary>
    /// ////////////////////////////////////////////
    /// </summary>
    public void BuyPack()
    {
        IAPManager.OnPurchaseSuccess =
            () =>
            {
                GameDataManager.Instance.playerData.UnlockPack();
                OpenApp();
            };
    }
/// <summary>
/// ////////////////////////////////////////////
/// </summary>
    public void OpenApp()
    {
        panelApp.SetActive(true);
    }
}