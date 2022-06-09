using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public int lives = 3;
    public float respawnTime = 3;
    public float respawnCollision = 3;
    public int score = 0;

    public TextMeshProUGUI liveText;
    public TextMeshProUGUI scoreText;
    

    private void Start()
    {
        liveText.text = "x " + lives.ToString();
        scoreText.text = score.ToString();
    }

    public void AsteroidDestroid(Asteroid asteroid) 
    {
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();

        //
        if (asteroid.size < 0.75f)
        {
            score += 100;
        }
        else if (asteroid.size < 1.2f)
        {
            score += 50;
        }
        else {
            score += 25;
        }
        scoreText.text = score.ToString();
    }

    public void PlayerDied() 
    {
        explosion.transform.position = player.transform.position;
        explosion.Play();
        lives--;
        if (lives <= 0)
        {
            GameOver();
        }
        else { 
            Invoke(nameof(Respawn), respawnTime);
        }
        liveText.text = "x " + lives.ToString();
    }
    private void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");

        Invoke(nameof(TurnOnCollision), respawnCollision);
    }


    private void TurnOnCollision() {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        lives = 3;
        score = 0;
        Invoke(nameof(Respawn), respawnTime);

        liveText.text = "x " + lives.ToString();
        scoreText.text = score.ToString();
    }
}
