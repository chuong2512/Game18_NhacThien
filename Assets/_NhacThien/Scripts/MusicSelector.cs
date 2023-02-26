using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicSelector : MonoBehaviour
{
    public int currentMusic;
    public MusicItem[] musicItems;
    private PlayerData playerData; //todo delete
    private GameDataManager gameData; //todo delete
    public TextMeshProUGUI diamonds;
    public GameObject inApp;

    public GameObject popupUnlock;
    public Button yesBtn, noBtn;

    void Start()
    {
        for (int i = 0; i < musicItems.Length; i++)
        {
            musicItems[i].Init(gameData.songSo.GetSongWithID(i).icon, i, this, gameData.songSo.GetSongWithID(i).name);

            if (playerData.listSongs[i])
            {
                musicItems[i].Unlock();
            }
        }

        noBtn.onClick.AddListener(() => { popupUnlock.SetActive(false); });

        yesBtn.onClick.AddListener(() => { popupUnlock.SetActive(false); });
    }

    void OnEnable()
    {
        gameData = GameDataManager.Instance;
        playerData = gameData.playerData;

        diamonds.text = "" + playerData.intDiamond;
        currentMusic = playerData.currentSong;
        if (currentMusic > -1)
        {
            musicItems[currentMusic].Choose();
            AudioManager.Instance.PlaySong(currentMusic);
            AudioManager.Instance.Play();
        }
    }

    public void ChooseMusic(int index)
    {
        if (currentMusic == index)
        {
            return;
        }

        if (playerData.listSongs[index] == false)
        {
            if (!playerData.CheckCanUnlock())
            {
                inApp.SetActive(true);
                return;
            }

            popupUnlock.SetActive(true);

            yesBtn.onClick.AddListener(() =>
            {
                UnlockSkin(index);
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(() => { popupUnlock.SetActive(false); });
            });

            return;
        }

        if (currentMusic > -1)
        {
            musicItems[currentMusic].UnChoose();
        }

        musicItems[index].Choose();
        currentMusic = index;
        playerData.ChooseSong(currentMusic);

        AudioManager.Instance.PlaySong(currentMusic);
    }

    public void AddDiamonds(int value)
    {
        IAPManager.OnPurchaseSuccess = () =>
        {
            playerData.AddDiamond(value);
            diamonds.text = playerData.intDiamond.ToString();
        };

        switch (value)
        {
            case 10:
                IAPManager.Instance.BuyProductID(IAPKey.PACK1);
                break;
            case 20:
                IAPManager.Instance.BuyProductID(IAPKey.PACK2);
                break;
            case 50:
                IAPManager.Instance.BuyProductID(IAPKey.PACK3);
                break;
            case 100:
                IAPManager.Instance.BuyProductID(IAPKey.PACK4);
                break;
        }
    }

    public void UnlockSkin(int index)
    {
        if (!playerData.listSongs[index])
        {
            playerData.SubDiamond(Constant.priceUnlockSong);
        }

        diamonds.text = playerData.intDiamond.ToString();
        musicItems[index].Unlock();
        playerData.listSongs[index] = true;
    }
}