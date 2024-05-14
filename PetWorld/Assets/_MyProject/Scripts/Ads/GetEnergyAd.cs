using UnityEngine;

public class GetEnergyAd : MonoBehaviour
{
    [SerializeField] private GameObject panelClose;

    public void WatchRewarded(int i)
    {
        RewardedAds.instance.SkipCaseTime(i);

        panelClose.SetActive(false);
    }
}
