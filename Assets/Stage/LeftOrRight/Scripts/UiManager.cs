using _2D_BUNDLE.LeftOrRight.Scripts;
using System.Collections;
using UnityEngine;

public class UiManager : MonoBehaviour
{

    [SerializeField] private GameObject fadeInPanel;

    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private GameObject touchToStartObj;

    [SerializeField] private GameObject instructionPanel;

    private void Start()
    {
        ActionManager.onGameStarted += OnGameStarted;
        ActionManager.onPlayerFirstMoved += OnPlayerFirstMoved;
        ActionManager.onGameEnded += OnGameEnded;
    }


    private void OnDestroy()
    {
        ActionManager.onGameStarted -= OnGameStarted;
        ActionManager.onPlayerFirstMoved -= OnPlayerFirstMoved;
        ActionManager.onGameEnded -= OnGameEnded;
    }

    private void OnPlayerFirstMoved()
    {
        touchToStartObj.SetActive(false);
    }


    public void HowToButton()
    {
        instructionPanel.SetActive(true);
    }

    public void CloseButton()
    {
        instructionPanel.SetActive(false);
    }


    private void OnGameStarted()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        fadeInPanel.SetActive(true);
        // wait to end fadein effect (animation)
        yield return new WaitForSecondsRealtime(0.5f);
        fadeInPanel.SetActive(false);
    }

    private void OnGameEnded()
    {
        gameOverPanel.SetActive(true);
    }
}