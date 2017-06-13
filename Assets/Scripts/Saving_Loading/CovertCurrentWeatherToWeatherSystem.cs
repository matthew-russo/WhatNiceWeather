using System;
using System.Collections;
using System.Collections.Generic;
using Patterns;
using UnityEngine;

/// <summary>
/// Class that converts weather data in a C# JSON Object to data that can be used in the in-game weather system.
/// </summary>

public class CovertCurrentWeatherToWeatherSystem : Singleton<CovertCurrentWeatherToWeatherSystem>
{
    public JSONObject data;

    public DateTime sunriseTime;
    public DateTime sunsetTime;

    public int weatherID;
    public string weatherIcon;
    public string weatherMain;

    // Dictionary that holds weather ids from JSON data and corresponding in-game Weather Types
    //
    // TODO: MAKE MULTIPLE WEATHER TYPES POSSIBLE (OUT OF THE SCOPE OF THIS PROJECT)
    //
    public Dictionary<int, Weather_Controller.WeatherType> WeatherIDsAndCorrespondingType = new Dictionary<int, Weather_Controller.WeatherType>
    {
        {200, Weather_Controller.WeatherType.THUNDERSTORM },
        {201, Weather_Controller.WeatherType.THUNDERSTORM },
        {202, Weather_Controller.WeatherType.THUNDERSTORM },
        {210, Weather_Controller.WeatherType.THUNDERSTORM },
        {211, Weather_Controller.WeatherType.THUNDERSTORM },
        {212, Weather_Controller.WeatherType.THUNDERSTORM },
        {221, Weather_Controller.WeatherType.THUNDERSTORM },
        {230, Weather_Controller.WeatherType.THUNDERSTORM },
        {231, Weather_Controller.WeatherType.THUNDERSTORM },
        {232, Weather_Controller.WeatherType.THUNDERSTORM },

        {300, Weather_Controller.WeatherType.RAIN  },  //ADD DRIZZLE WEATHER TYPE
        {301, Weather_Controller.WeatherType.RAIN  },  //ADD DRIZZLE WEATHER TYPE
        {302, Weather_Controller.WeatherType.RAIN  },  //ADD DRIZZLE WEATHER TYPE
        {310, Weather_Controller.WeatherType.RAIN  },  //ADD DRIZZLE WEATHER TYPE
        {311, Weather_Controller.WeatherType.RAIN  },  //ADD DRIZZLE WEATHER TYPE
        {312, Weather_Controller.WeatherType.RAIN  },  //ADD DRIZZLE WEATHER TYPE
        {313, Weather_Controller.WeatherType.RAIN  },  //ADD DRIZZLE WEATHER TYPE
        {314, Weather_Controller.WeatherType.RAIN  },  //ADD DRIZZLE WEATHER TYPE
        {321, Weather_Controller.WeatherType.RAIN  },  //ADD DRIZZLE WEATHER TYPE

        {500, Weather_Controller.WeatherType.RAIN },
        {501, Weather_Controller.WeatherType.RAIN },
        {502, Weather_Controller.WeatherType.RAIN },
        {503, Weather_Controller.WeatherType.RAIN },
        {504, Weather_Controller.WeatherType.RAIN },
        {511, Weather_Controller.WeatherType.RAIN },
        {520, Weather_Controller.WeatherType.RAIN },
        {521, Weather_Controller.WeatherType.RAIN },
        {522, Weather_Controller.WeatherType.RAIN },
        {531, Weather_Controller.WeatherType.RAIN },

        {600, Weather_Controller.WeatherType.SNOW },
        {601, Weather_Controller.WeatherType.SNOW },
        {602, Weather_Controller.WeatherType.SNOW },
        {611, Weather_Controller.WeatherType.SNOW },
        {612, Weather_Controller.WeatherType.SNOW },
        {615, Weather_Controller.WeatherType.SNOW },
        {616, Weather_Controller.WeatherType.SNOW },
        {620, Weather_Controller.WeatherType.SNOW },
        {621, Weather_Controller.WeatherType.SNOW },
        {622, Weather_Controller.WeatherType.SNOW },

        {701, Weather_Controller.WeatherType.CLOUDY }, // mist
        {711, Weather_Controller.WeatherType.CLOUDY }, // smoke
        {721, Weather_Controller.WeatherType.CLOUDY }, // haze
        {731, Weather_Controller.WeatherType.CLOUDY }, // sand, dustwhirls
        {741, Weather_Controller.WeatherType.CLOUDY }, // fog
        {751, Weather_Controller.WeatherType.CLOUDY }, // sand
        {761, Weather_Controller.WeatherType.CLOUDY }, // dust
        {762, Weather_Controller.WeatherType.CLOUDY }, // volcanic ash
        {771, Weather_Controller.WeatherType.CLOUDY }, // squall
        {781, Weather_Controller.WeatherType.CLOUDY }, // tornado

        {800, Weather_Controller.WeatherType.SUN },

        {801, Weather_Controller.WeatherType.CLOUDY },
        {802, Weather_Controller.WeatherType.CLOUDY  },
        {803, Weather_Controller.WeatherType.CLOUDY  },
        {804, Weather_Controller.WeatherType.CLOUDY  },

        //{900, "extreme" }, ADD NEW WEATHER TYPES
        //{901, "extreme" }, ADD NEW WEATHER TYPES
        //{902, "extreme" }, ADD NEW WEATHER TYPES
        //{903, "extreme" }, ADD NEW WEATHER TYPES
        //{904, "extreme" }, ADD NEW WEATHER TYPES
        //{905, "extreme" }, ADD NEW WEATHER TYPES
        //{906, "extreme" }, ADD NEW WEATHER TYPES

        //{951, "extreme" }, ADD NEW WEATHER TYPES
        //{952, "extreme" }, ADD NEW WEATHER TYPES
        //{953, "extreme" }, ADD NEW WEATHER TYPES
        //{954, "extreme" }, ADD NEW WEATHER TYPES
        //{955, "extreme" }, ADD NEW WEATHER TYPES
        //{956, "extreme" }, ADD NEW WEATHER TYPES
        //{957, "extreme" }, ADD NEW WEATHER TYPES
        //{958, "extreme" }, ADD NEW WEATHER TYPES
        //{959, "extreme" }, ADD NEW WEATHER TYPES
        //{960, "extreme" }, ADD NEW WEATHER TYPES
        //{961, "extreme" }, ADD NEW WEATHER TYPES
        //{900, "extreme" }, ADD NEW WEATHER TYPES
    };

    // Takes WeatherID from JSON and changes the in-game weather accordingly.
    //
    public void ChangeWeatherSystem()
    {
        Weather_Controller.Instance.ExitCurrentWeather((int)WeatherIDsAndCorrespondingType[weatherID]);
        Debug.Log("Changing weather to : " + WeatherIDsAndCorrespondingType[weatherID]);
    }

    // Pulls the sunrise and sunset data from API
    // Inital format is UTC ticks so we have to use the DateTime class to convert that to normal readable time
    //
    public void PullSunriseSunsetTimesFromJSON()
    {
        var sunriseSec = data["sys"]["sunrise"];
        sunriseTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        sunriseTime = sunriseTime.AddSeconds(sunriseSec.i).ToLocalTime();
        Debug.Log("The sun will rise at : " + sunriseTime);

        var sunsetSec = data["sys"]["sunset"];
        sunsetTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        sunsetTime = sunsetTime.AddSeconds(sunsetSec.i).ToLocalTime();
        Debug.Log("The sun will set at : " + sunsetTime);
    }

    // Gets the weather ID number, icon name, and main detail
    // WeatherID is used to change in game weather
    // Icon name and main detail are used for UI
    //
    public void PullWeatherIDFromJSON()
    {
        weatherID = (int)data["weather"][0]["id"].i;
        weatherIcon = data["weather"][0]["icon"].str;
        weatherMain = data["weather"][0]["main"].str;
        Debug.Log("The current weather ID is : " + weatherID + " corresponding to WeatherType: " + WeatherIDsAndCorrespondingType[weatherID]);
    }
}
