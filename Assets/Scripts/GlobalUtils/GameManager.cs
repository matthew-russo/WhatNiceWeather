using System.Collections;
using System.Collections.Generic;
using Patterns;
using UnityEngine;

/// <summary>
/// Singleton to hold general global variables.
/// I should either make this more useful, or get rid of it...
/// </summary>

public class GameManager : Singleton<GameManager>
{
    public int killCount;

    public int multiplier;

    private void Update()
    {
        multiplier = killCount / 3;
    }
}
