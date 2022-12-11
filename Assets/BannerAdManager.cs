using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAdManager : MonoBehaviour {

    [SerializeField]
    string appId = "";
    [SerializeField]
    string adUnitId = "";
    [SerializeField]
    string interistialAdId = "";

    public bool isAdEnabled;

    private int adX, adY;

    //Google Admob ads
    private BannerView bannerView;

    private void Awake()
    {
        if (isAdEnabled)
        {
            MobileAds.Initialize(appId);
            RequestBanner();
            //RequestInteristialAds();
        }
    }

    private void RequestBanner()
    {
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.BottomRight);
        AdRequest adRequest = new AdRequest.Builder().Build();
        bannerView.LoadAd(adRequest);
    }

    private void RequestInteristialAds()
    {
        InterstitialAd interstitialAd = new InterstitialAd(interistialAdId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
        bool value = true;
        while (value)
        {
            if (interstitialAd.IsLoaded())
            {
                interstitialAd.Show();
                value = false;
            }
        }
    }

}
