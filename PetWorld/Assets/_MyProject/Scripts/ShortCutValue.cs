using UnityEngine;

public class ShortCutValue: MonoBehaviour
{
    private string[] shortcutCount = new string[7] { "K", "M", "B", "T", "q", "Q", "s" };

    public static ShortCutValue shortCutValue;

    // Singleton

    private void Awake()
    {
        if (!shortCutValue)
        {
            shortCutValue = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public string CutIntValue(double intValue, int order)
    {
        string value = "";

        for (int i = 0; i < shortcutCount.Length; i++)
        {
            if (intValue / 1000 < 1)
            {
                if (i == 0) { value = intValue.ToString(); break; }
                else { value = intValue.ToString() + shortcutCount[i - 1]; break; }
            }
            else { intValue = System.Math.Round(intValue / 1000f, order); }
        }

        return value;
    }
}
