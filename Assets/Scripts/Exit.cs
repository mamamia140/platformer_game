using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            int sceneNumber = SceneManager.GetActiveScene().buildIndex;
            StartCoroutine(IELoadTheLevel(sceneNumber + 1));
        }

    }

    IEnumerator IELoadTheLevel(int sceneNumber)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneNumber);

    }
}
