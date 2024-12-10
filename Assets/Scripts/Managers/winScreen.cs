using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class winScreen : MonoBehaviour
{
    [SerializeField] RectTransform credits;
    [SerializeField] float moveSpeed;
    [SerializeField] Button quitButton;

    private void Start()
    {
        StartCoroutine(QuitAfterCredits());
    }

    IEnumerator QuitAfterCredits()
    {
        while (true)
        {
            yield return new WaitForSeconds(68);
            ButtonQuit();
        }
    }

    public void ButtonQuit()
    {
        quitButton.interactable = false;
        AudioSource honkhonk = quitButton.gameObject.GetComponent<AudioSource>();
        honkhonk.Play();
        StartCoroutine(QuitAfterTime(honkhonk.clip.length));
    }

    IEnumerator QuitAfterTime(float quittime)
    {
        
        yield return new WaitForSeconds(quittime);

        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
