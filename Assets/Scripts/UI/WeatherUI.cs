using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gets the weather info from CovertCurrentWeatherToWeatherSystem class and updates the UI with it
/// </summary>

public class WeatherUI : MonoBehaviour
{
    private Text weatherText;
    private Image weatherImage;
    
    // Reference to the weatherIcon we will load
    //
    public Sprite weatherIcon;

    private bool hasUpdated = false;

	void Start ()
	{
	    weatherText = transform.GetChild(0).GetComponent<Text>();
        weatherImage = transform.GetChild(1).GetComponent<Image>();
    }
	
	void Update ()
	{
	    if (!hasUpdated && SavingLoadingData.Instance.finishedLoading)
	    {
            // Adds the name of the type of weather to the UI
            //
            weatherText.text = "Current Weather : " + CovertCurrentWeatherToWeatherSystem.Instance.weatherMain;

            // Loads the weather icon based upon API response
            // Loads in as a Texture2D so we need to create a sprite from that via scripting before we can apply it to the UI Image.
            //
            string filepath = "WeatherIcons/" + CovertCurrentWeatherToWeatherSystem.Instance.weatherIcon;
	        weatherIcon = Resources.Load<Sprite>(filepath);
            //weatherIcon = Resources.Load(filepath) as Texture2D;
            //Sprite icon = Sprite.Create(weatherIcon, new Rect(0,0,weatherIcon.width,weatherIcon.height), new Vector2(.5f,.5f));
	        weatherImage.sprite = weatherIcon;

            // Bool so we only do this once in the beginning of the game, but after start so everything is sure to be initialized
            //
	        hasUpdated = true;
	    }
	}
}
