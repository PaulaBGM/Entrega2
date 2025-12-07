using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        if (mainMenuCanvas) mainMenuCanvas.SetActive(true);
    }

    private void Start()
    {
        if (playButton) playButton.onClick.AddListener(OnPlay);
        if (optionsButton) optionsButton.onClick.AddListener(OnOpenOptions);
        if (quitButton) quitButton.onClick.AddListener(OnQuit);

    }

    private void OnPlay()
    {
        SceneManager.LoadScene(1);
    }

    private void OnOpenOptions()
    {
        UIEvents.RequestOpenOptions(false, "MainMenu");
        if (mainMenuCanvas) mainMenuCanvas.SetActive(false);

        UIEvents.OnOptionsClosed += HandleOptionsClosed;
    }

    private void HandleOptionsClosed()
    {
        if (mainMenuCanvas) mainMenuCanvas.SetActive(true);
        UIEvents.OnOptionsClosed -= HandleOptionsClosed;
    }

    private void OnQuit()
    {
        Application.Quit();
    }
}

