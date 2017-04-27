using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;
using Patterns;

public class SavingLoadingData : Singleton<SavingLoadingData>
{
    private DateTime lastLoadeDateTime;
    public string lastLoadedData;

    private TimeSpan difference;
    private string savePath;

    public JSONObject json;

    string URL = "http://matthewclayrusso.com/weather";
    

    public void Awake () {
        // Save and Load Assets from the Streaming Assets folder
        //
        savePath = Application.streamingAssetsPath;

        // Pulls New York Weather data from my server which pulls from OpenWeatherMap API
        // Saves the json string in a file. If weather has been recently checked, it uses the most recent data
        //
        LoadWeatherFromAPITEST(URL);

        // Loads the json string received from the Weather API into a JSON C# Object so that we can access it from other scripts in an easy to use manner.
        //
        json = LoadDataAsJSON("lastAPIData.txt");
    }

    // Saves string input data into the given file name
    //
    public void SaveDataToFile(string filename, string data)
    {
        string filePath = Path.Combine(savePath, filename);

        if (!File.Exists(filePath))
        {
            File.Create(filePath);
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
    void LoadWeatherFromAPITEST(string url)
    {
       
        WWW newWWW = new WWW(url);
        while (!newWWW.isDone)
        {
            Debug.Log("waiting for :  " + Time.time);
        }
        Debug.Log(newWWW.text);
        SaveDataToFile("lastAPIData.txt", newWWW.text);
    }
}
