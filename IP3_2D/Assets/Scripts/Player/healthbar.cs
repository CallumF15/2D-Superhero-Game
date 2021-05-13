using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour {

    public static Image HealthBarImage;


    /// <summary>
    /// Sets the health bar value
    /// </summary>
    /// <param name="value">should be between 0 to 1</param>
    public static void SetHealthBarValue(float value)
    {
        HealthBarImage.fillAmount = value;

        //if (HealthBarImage.fillAmount < 0.2f)
        //    SetHealthBarColor(Color.red);    
        //else if (HealthBarImage.fillAmount < 0.4f)
        //    SetHealthBarColor(Color.yellow);
        //else
        //    SetHealthBarColor(Color.green);

        if(HealthBarImage.fillAmount <= 0)
        {
            //player dead 
            //display end game message
        }
    }

    public static float GetHealthBarValue()
    {
        return HealthBarImage.fillAmount;
    }

    /// <summary>
    /// Sets the health bar color
    /// </summary>
    /// <param name="healthColor">Color </param>
    public static void SetHealthBarColor(Color healthColor)
    {
        HealthBarImage.color = healthColor;
    }
}
