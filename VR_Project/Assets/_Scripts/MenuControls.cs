using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuControls : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip click;
    public GameObject menu, options, low, medium, high;
    TextMesh lowMesh, mediumMesh, highMesh;
    Color offColor = Color.white;
    Color onColor = Color.red;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        lowMesh = low.GetComponent<TextMesh>();
        mediumMesh = medium.GetComponent<TextMesh>();
        highMesh = high.GetComponent<TextMesh>();

        Application.targetFrameRate = 90;
        QualitySettings.SetQualityLevel(2);
        mediumMesh.color = onColor;
    }
    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20))
        {
            switch (hit.collider.gameObject.name)
            {
                case "Start":
                    if (Input.GetMouseButtonDown(0))
                    {
                        Click();
                        SceneManager.LoadScene("Main");
                    }
                    break;

                case "Options":
                    if (Input.GetMouseButtonDown(0))
                    {
                        Click();
                        menu.SetActive(false);
                        options.SetActive(true);
                    }
                    break;

                case "Quit":
                    if (Input.GetMouseButtonDown(0))
                    {
                        Click();
                        Application.Quit();
                    }
                    break;

                case "Back":
                    if (Input.GetMouseButtonDown(0))
                    {
                        Click();
                        menu.SetActive(true);
                        options.SetActive(false);
                    }
                    break;

                case "Low":
                    if (Input.GetMouseButtonDown(0))
                    {
                        Click();
                        Application.targetFrameRate = 60;
                        QualitySettings.SetQualityLevel(0);
                        lowMesh.color = onColor;
                        mediumMesh.color = offColor;
                        highMesh.color = offColor;
                    }
                    break;

                case "Medium":
                    if (Input.GetMouseButtonDown(0))
                    {
                        Click();
                        Application.targetFrameRate = 90;
                        QualitySettings.SetQualityLevel(1);
                        lowMesh.color = offColor;
                        mediumMesh.color = onColor;
                        highMesh.color = offColor;
                    }
                    break;

                case "High":
                    if (Input.GetMouseButtonDown(0))
                    {
                        Click();
                        Application.targetFrameRate = 144;
                        QualitySettings.SetQualityLevel(2);
                        lowMesh.color = offColor;
                        mediumMesh.color = offColor;
                        highMesh.color = onColor;
                    }
                    break;

                default:
                    break;
            }                
        }
    }


    public void Click()
    {
        audioSource.clip = click;
        audioSource.Play();
    }
}
