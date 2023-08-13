using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI ammoText;
    [SerializeField]
    TextMeshProUGUI healthText;
    [SerializeField]
    TextMeshProUGUI deathText;
    [SerializeField]
    Button replayButton;
    [SerializeField]
    TextMeshProUGUI reloadingText;
    public bool isAlive = true;
    EnemiesKilled kills;
    PlayerMovement player;
    [SerializeField]
    Button quitGame;
    [SerializeField]
    GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        kills = GameObject.Find("Agent").GetComponent<EnemiesKilled>();
        replayButton.gameObject.SetActive(false);
        deathText.gameObject.SetActive(false);
        player = GameObject.Find("Agent").GetComponent<PlayerMovement>();
        reloadingText.gameObject.SetActive(false);
        quitGame.gameObject.SetActive(false);
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

    }

    public void AmmoDisplay(int ammo, int maxAmmo)
    {
        ammoText.text = ammo + "/" + maxAmmo;
    }

    public void UpdateHealth(int health)
    {
        healthText.text = "HEALTH: " + health;
        if (health <= 0)
        {
            isAlive = false;
            EnemiesKilled(kills.kills);
            deathText.gameObject.SetActive(true);
            replayButton.gameObject.SetActive(true);
            player.MouseCursorVisibility(true);
            
        }
    }

    public void EnemiesKilled(int x)
    {
        deathText.text = "You Died\nEnemies killed: " + x;
        quitGame.gameObject.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene(0);
        player.MouseCursorVisibility(false);
    }

    public void ReloadingTextVisibility(bool state)
    {
        reloadingText.gameObject.SetActive(state);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

    }
    public void Resumegame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
