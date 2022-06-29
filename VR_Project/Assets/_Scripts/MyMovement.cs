using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MyMovement : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip move;
    public Text name, uiText;
    public EnemyController enemy;
    string text;
    bool coroutine = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        name.text = "";
        if (Input.GetMouseButtonDown(0))
        {            
            if (coroutine)
            {
                StopAllCoroutines();
            }
            uiText.text = "";
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20))
        {
            text = hit.collider.gameObject.name;
            switch (hit.collider.gameObject.tag)
            {                
                case "Arrow":
                    showName(text);
                    if (Input.GetMouseButtonDown(0))
                    {
                        moveSound();
                        movePlayer(text);
                    }
                    break;

                case "Sign":
                    if (Input.GetMouseButtonDown(0))
                    {
                        uiText.text = "";
                        StartCoroutine(scrollText(text));
                    }
                    break;

                case "Zombie":
                    if (Input.GetMouseButtonDown(0))
                    {
                        uiText.text = "";
                        StartCoroutine(scrollText(text));
                        enemy.leave();
                    }
                    break;
            }                
        }
    }

    private IEnumerator scrollText(string text)
    {
        string line = "";
        switch (text)
        {
            case "SwordSign":
                line = "Excalibur is the legandary sword of King Arthur, it is also known as 'The Sword in The Stone'.";
                break;

            case "ZombieSign":
                line = "The first instance of zombies in history was in ancient Greece. Rocks were attached to dead bodies to stop the dead from rising.";
                break;

            case "BirdSign":
                line = "When a flock of birds fly in circles they could be creating an updraft in order to conserve energy.";
                break;

            case "Zombie":
                line = "Zombie: Leave me alone!";
                break;

        }

        coroutine = true;
        foreach (char c in line)
        {
            uiText.text += c;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        coroutine = false;
    }

    private void movePlayer(string text)
    {
        switch (text)
        {
            case "HouseArrow":
                gameObject.transform.position = new Vector3(394.62f, 13, 358);
                break;

            case "SpawnArrow":
                gameObject.transform.position = new Vector3(426.27f, 13, 394.03f);
                break;

            case "BeachArrow":
                gameObject.transform.position = new Vector3(557.73f, 13, 416.4f);
                break;

            case "MenuArrow":
                SceneManager.LoadScene("Menu");
                break;
        }
    }

    public void moveSound()
    {
        audioSource.clip = move;
        audioSource.Play();
    }

    private void showName(string text)
    {
        name.text = text;
    }
}
