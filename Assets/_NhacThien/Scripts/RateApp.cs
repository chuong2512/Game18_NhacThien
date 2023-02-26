using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateApp : MonoBehaviour
{
    public void OnClickPrivacy()
    {
        Application.OpenURL("");
    }

    public void OnClickRateUs()
    {
        Application.OpenURL("market://details?id=" + Application.identifier);
    }
}