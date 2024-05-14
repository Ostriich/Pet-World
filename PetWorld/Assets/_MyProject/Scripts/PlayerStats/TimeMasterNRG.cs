using UnityEngine;
using System;
using YG;
using UnityEngine.SceneManagement;

public class TimeMasterNRG : MonoBehaviour
{
    private DateTime currentDate;
    private DateTime oldDate;

    public static TimeMasterNRG instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Check the current time against saved

    public float CheckDate()
    {
        currentDate = System.DateTime.Now;

        long tempLong = Convert.ToInt64(YandexGame.savesData.LastTimeLoad);

        oldDate = DateTime.FromBinary(tempLong);

        if (YandexGame.savesData.LastTimeLoad == "0") { oldDate = currentDate; }

        TimeSpan difference = currentDate.Subtract(oldDate);

        return (float)difference.TotalSeconds;
    }

    public void SaveDate()
    {
        YandexGame.savesData.LastTimeLoad = DateTime.Now.ToBinary().ToString();
        YandexGame.SaveProgress();
    }
}
