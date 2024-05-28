using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interstitial : MonoBehaviour
{
    public static Interstitial instance;
    private InterstitialAd _interstitialAd;
    private static float lastInterstitial;
    public GameObject adLoader;
    public bool testMode = false;
#if UNITY_ANDROID
    public string IntrestitialID = "ca-app-pub-3940256099942544/1033173712";
#endif

    public int tryCount;
    public int ReloadTime = 40;
    bool isLoadingISIntrestital;
    public bool isIntrestitiallShowing = false;
    public Action<bool> _callbackIntrestital;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadInterstitialAd();
    }


    public void LoadInterstitialAd(float delay)
    {
        Invoke("LoadInterstitialAd", delay);
    }

    public void ExampleShowIntrestitialWithCallback()
    {
        ShowInterstitialTimer(TestIntrestitialWithCallback);
    }

    public void ExampleShowIntrestitialWithLoaderCallback()
    {
        ShowForceInterstitialWithLoader(TestIntrestitialWithCallback, 3);
    }

    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(IntrestitialID, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _interstitialAd = ad;
                ItrestitialCallback(ad);
            });
    }
    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    public void TestIntrestitialWithCallback(bool isCompleted)
    {

        if (isCompleted)
        {
            //Give reward here
            Debug.Log("Intrestitial  completed  Do other thing");

        }
        else
        {
            Debug.Log("Intrestitial  has issue");

            // do next step as reward not available
        }
    }

    public bool ISIntrestitialReadyToShow(bool ForceShow = false)
    {
        if (!_interstitialAd.CanShowAd())
        {
            LoadInterstitialAd();
        }
        float time = Time.time;
        bool isloadedTime = false;
        if (time - lastInterstitial >= ReloadTime)
        {
            //ShowInterstitial(f);
            //lastInterstitial = time;
            isloadedTime = true;
        }
        if (ForceShow)
        {
            isloadedTime = true;
        }
        return _interstitialAd.CanShowAd() && isloadedTime;
    }

    //-----Force------//

    public void ShowForceInterstitialWithLoader(Action<bool> onComplete, int _tryCount)
    {
        tryCount = _tryCount;
        // adLoader.SetActive(true);
        //alreay showing
        if (isIntrestitiallShowing)
        {
            return;
        }


        //Regular call

        adLoader.SetActive(true);

        isIntrestitiallShowing = true;
        _callbackIntrestital = onComplete;

        if (testMode)
        {
            isIntrestitiallShowing = false;
            adLoader.SetActive(false);
            if (_callbackIntrestital == null)
            {
                return;
            }

            _callbackIntrestital.Invoke(true);
            _callbackIntrestital = null;
            return;
        }

#if UNITY_EDITOR
        isIntrestitiallShowing = false;
        adLoader.SetActive(false);
        if (_callbackIntrestital == null)
        {
            return;
        }

        _callbackIntrestital.Invoke(true);
        _callbackIntrestital = null;
        return;
#endif

        if (ISIntrestitialReadyToShow(true))
        {
            ShowInterstitialAd();
            lastInterstitial = Time.time;
        }
        else
        {

            if (tryCount > 0)
            {
                // tryCount--;
                isIntrestitiallShowing = false;
                LoadInterstitialAd(0);
                StartCoroutine(IEShowForceInterstitialWithLoader(_callbackIntrestital));
            }
            else
            {
                adLoader.SetActive(false);
                isIntrestitiallShowing = false;
                if (_callbackIntrestital == null)
                {
                    return;
                }
                _callbackIntrestital.Invoke(false);
                _callbackIntrestital = null;
            }
            LoadInterstitialAd(0f);
        }

    }

    public IEnumerator IEShowForceInterstitialWithLoader(Action<bool> onComplete)
    {
        tryCount--;
        if (tryCount > 0)
        {
            yield return new WaitForSeconds(1f);
            ShowForceInterstitialWithLoader(onComplete, tryCount);
        }
        else
        {
            tryCount = 0;
            adLoader.SetActive(false);
            isIntrestitiallShowing = false;
            if (_callbackIntrestital != null)
            {
                _callbackIntrestital.Invoke(false);
                _callbackIntrestital = null;
            }
            LoadInterstitialAd(0f);
        }
    }

    public void ShowInterstitialTimer(Action<bool> onComplete)
    {
        tryCount = 0;
        Debug.Log("isIntrestitiallShowing => " + isIntrestitiallShowing);
        if (isIntrestitiallShowing)
        {
            return;
        }

        
        isIntrestitiallShowing = true;
        _callbackIntrestital = onComplete;

        if (testMode)
        {
            isIntrestitiallShowing = false;
            adLoader.SetActive(false);
            if (_callbackIntrestital == null)
            {
                return;
            }

            _callbackIntrestital.Invoke(true);
            _callbackIntrestital = null;
            return;
        }

#if UNITY_EDITOR
        isIntrestitiallShowing = false;
        if (_callbackIntrestital == null)
        {
            return;
        }
        _callbackIntrestital.Invoke(true);
        _callbackIntrestital = null;
        return;
#endif
        //Regular call

        adLoader.SetActive(true);
        Debug.Log("ShowInterstitialTimer");

        if (ISIntrestitialReadyToShow())
        {
            Debug.Log("Ready to show");
            ShowInterstitialAd();
            lastInterstitial = Time.time;
        }
        else
        {
            Debug.Log("can't show");
            adLoader.SetActive(false);
            isIntrestitiallShowing = false;
            if (_callbackIntrestital == null)
            {
                return;
            }
            _callbackIntrestital.Invoke(false);
            _callbackIntrestital = null;
            LoadInterstitialAd(0f);
        }
    }

    private void ItrestitialCallback(InterstitialAd interstitialAd)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(string.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
            adLoader.SetActive(false);
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            tryCount = 0;
            adLoader.SetActive(false);
            LoadInterstitialAd(0f);
            isIntrestitiallShowing = false;
            if (_callbackIntrestital == null)
            {
                return;
            }
            _callbackIntrestital.Invoke(true);
            _callbackIntrestital = null;

            Debug.Log("heheheheheh");
            // Debug.Log("Interstitial ad full screen content closed.");
            //LoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
            isLoadingISIntrestital = false;
            LoadInterstitialAd();
            isIntrestitiallShowing = false;
        };
    }

}
