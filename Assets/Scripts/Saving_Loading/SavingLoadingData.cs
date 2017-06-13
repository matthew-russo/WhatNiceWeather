using System;
using System.Collections;
using System.IO;
using System.Net;
using UnityEngine;
using Patterns;
using UnityEngine.Networking;

public class SavingLoadingData : Singleton<SavingLoadingData>
{
    private DateTime lastLoadeDateTime;
    public string lastLoadedData;

    private TimeSpan difference;
    private string savePath;

    public JSONObject json;

    string URL = "https://matthewclayrusso.com/weather";

    public bool finishedLoading = false;

    public void Awake () {
        // Save and Load Assets from the Streaming Assets folder
        //
        savePath = Application.persistentDataPath;

        // Pulls New York Weather data from my server which pulls from OpenWeatherMap API
        // Saves the json string in a file. If weather has been recently checked, it uses the most recent data
        //
        StartCoroutine(LoadWeatherFromAPITEST(URL));
    }

    // Saves string input data into the given file name
    //
    public void SaveDataToFile(string filename, string data)
    {
        string filePath = Path.Combine(savePath, filename);

        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }

        File.WriteAllText(filePath, data);
    }

    // Loads data in the form of a string from a given filename
    //
    public string LoadDataFromFile(string filename)
    {
        string filePath = Path.Combine(savePath, filename);

        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }
        else
        {
            throw new UnityException("Trying to load from a file that does not exist");
        }
    }

    // Loads a JSON string into a C# object from a given file.
    //
    public JSONObject LoadDataAsJSON(string filename)
    {
        string filepath = Path.Combine(savePath, filename);
        if (File.Exists(filepath))
        {
            return JSONObject.Create(LoadDataFromFile(filename), maxDepth: -4);
        }
        else
        {
            throw new UnityException("Trying to load from a file that does not exist");
        }
    }


    // Loads the data from the URL defined above. Used to interface with the weather API.
    // API can only handle 1 request per 10 minutes, so check if the last request was more than 10 minutes ago, and if so, send a new request to the given api url. It takes the data and saves it as a string to "/StreamingAssets/lastAPIData.txt"
    // if previous request was less than 10 minutes, load the previous data from "/StreamingAssets/lastAPIData.txt". Finally it saves the current time of this request to "/StreamingAssets/lastAPIRequest.txt"
    //
    IEnumerator LoadWeatherFromAPITEST(string url)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.Send();
     
        Debug.Log(webRequest.isError ? webRequest.error : webRequest.downloadHandler.text);

        SaveDataToFile("lastAPIData.txt", webRequest.downloadHandler.text);

        // Loads the json string received from the Weather API into a JSON C# Object so that we can access it from other scripts in an easy to use manner.
        //
        json = LoadDataAsJSON("lastAPIData.txt");

        // Gets Sunrise, Sunset, and current WeatherID from JSON data.
        // Sunrise and Sunset times as well as current time are set directly in the To D_Base Script on TimeOfDay object in hierarchy
        // ChangeWeatherSystem fucntion takes WeatherID from JSON and changes the in-game weather accordingly.
        //
        CovertCurrentWeatherToWeatherSystem.Instance.data = json;
        CovertCurrentWeatherToWeatherSystem.Instance.PullSunriseSunsetTimesFromJSON();
        CovertCurrentWeatherToWeatherSystem.Instance.PullWeatherIDFromJSON();
        CovertCurrentWeatherToWeatherSystem.Instance.ChangeWeatherSystem();

        finishedLoading = true;
    }
}
