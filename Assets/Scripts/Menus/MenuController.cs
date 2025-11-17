using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        if (mainMenuCanvas) mainMenuCanvas.SetActive(true);
    }

    private void Start()
    {
        if (playButton) playButton.onClick.AddListener(OnPlay);
        if (quitButton) quitButton.onClick.AddListener(OnQuit);
    }

    public void OnPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
