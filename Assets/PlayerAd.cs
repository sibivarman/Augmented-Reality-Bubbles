using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class PlayerAd : MonoBehaviour {

    public void ShowAd()
    {
        Advertisement.Show("menuScene", new ShowOptions() { resultCallback = HandleAdResult});
    }

    private void HandleAdResult(ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished:
                Debug.Log("Ad finished successfullay");
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad is skipped");
                break;
            case ShowResult.Failed:
                Debug.Log("Ad failed to load");
                break;
        }
    }
}
