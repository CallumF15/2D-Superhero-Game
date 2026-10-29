using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

    public static Image HealthBarImage;

    /// <summary>
    /// Sets the health bar value
    /// </summary>
    /// <param name="value">should be between 0 to 1</param>
    public static void SetHealthBarValue(float value)
    {
        HealthBarImage.fillAmount = value;
        ChangeHeathbarColour(value);
    }

    /// <summary>
    /// Return the health bar value
    /// </summary>
    /// <returns></returns>
    public static float GetHealthBarValue()
    {
        return HealthBarImage.fillAmount;
    }

    /// <summary>
    /// Changes the color of the health bar based on the value
    /// </summary>
    /// <param name="value"></param>
    private static void ChangeHeathbarColour(float value)
    {
        switch (value)
        {
            case <= 0.3f:
                SetHealthBarColor(Color.red);
                break;
            case <= 0.6f:
                SetHealthBarColor(Color.yellow);
                break;
            default:
                SetHealthBarColor(Color.green);
                break;
        }
    }

    /// <summary>
    /// Sets the health bar color
    /// </summary>
    /// <param name="healthColor">Color </param>
    private static void SetHealthBarColor(Color healthColor)
    {
        HealthBarImage.color = healthColor;
    }
}
