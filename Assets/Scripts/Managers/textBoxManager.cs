using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine.Audio;
public class textBoxManager : MonoBehaviour
{
    public static textBoxManager instance;

    [SerializeField] CanvasGroup textboxGroup;
    [SerializeField] TextMeshProUGUI textboxText;

    Dictionary<string, List<string>> interactionTexts = new();
    List<string> current = null;
    int currentIndex = -1;

    [SerializeField] AudioMixer masterMixer;
    public float tempFXVol;

    private void Awake()
    {
        instance = this;
        interactionTexts.Add("FirstDoor", new List<string>() { "Let's blow this popsicle stand >:o)","[Press LMB to interact]" });
    }

    public void initiateText(GameObject obj)
    {
        if (current == null && interactionTexts.ContainsKey(obj.name))
        {
            textboxGroup.alpha = 1;

            current = interactionTexts[obj.name];
            currentIndex = 0;

            interactionTexts.Remove(obj.name);

            showDialogue();

            masterMixer.GetFloat("fxVol", out tempFXVol);
            masterMixer.SetFloat("fxVol", -80f);
            Time.timeScale = 0;
        }
    }

    void showDialogue()
    {
        textboxText.text = current[currentIndex];
    }


    private void Update()
    {
        
        if (textboxGroup.alpha == 1f && Input.GetKeyDown(KeyCode.Space))
        {
            // progress dialog
            if (currentIndex < current.Count - 1)
            {
                currentIndex++;
                showDialogue();
            }
            else
            {
                textboxGroup.alpha = 0f;
                current = null;
                currentIndex = -1;
                masterMixer.SetFloat("fxVol", tempFXVol);
                Time.timeScale = 1f;
            }
        }
    }
}
