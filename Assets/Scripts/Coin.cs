using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    AudioClip audio;

    [SerializeField]
    int points = 50;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<GameSession>().AddToScore(points);
            AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }

}
