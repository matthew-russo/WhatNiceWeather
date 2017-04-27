using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

/// <summary>
/// Script that handles Player health, UI effects, and gameover screen
/// Health starts at 0 and works its way up to 4. It is done this way becuase health corresponds to a UI image's alpha
/// </summary>

public class PlayerHealth : MonoBehaviour
{
    private float _deathThreshold = 4; // point at which player dies
    public float currentHealth = 0; // amount of health a player starts with
    private float decayRate = .001f; // How quickly currentHealth goes back to 0

    public GameObject DamageUI;
    private Image _damageImage;
    
    public GameObject gameoverPanel;
    private GameObject playerCanvas;

	void Start ()
	{
	    _damageImage = DamageUI.GetComponent<Image>();
	    playerCanvas = transform.GetChild(1).gameObject;
	}

    // If player's health is between start amount and the death threshold, constantly subtract decayRate
    // Modify the alpha value of the DamageUI Image according to the players currentHealth
    // If health goes over the deathThreshold, triggers gameover
    //
	void Update () {
	    if (currentHealth < _deathThreshold && currentHealth > 0)
	    {
            currentHealth -= decayRate;
	    }
	    _damageImage.color = new Color(_damageImage.color.r,_damageImage.color.g,_damageImage.color.b, currentHealth / _deathThreshold);

	    if (currentHealth >= _deathThreshold)
	    {
	        GameOver();
	    }
	}

    // Activates the Gameover UI, blurs the screen, unlocks the curson, and gets rid of crosshairs
    //
    private void GameOver()
    {
        Camera.main.GetComponent<Blur>().enabled = true;
        gameoverPanel.SetActive(true);
        playerCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    // When player hits the "Play Again" Button, it reloads the scene
    //
    public void GameOverResetButton()
    {
        SceneManager.LoadScene(0);
    }
}
