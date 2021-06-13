using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private GameManager _gameManager;

    // Sub Panels
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private GameObject _overlayPanel;
    [SerializeField] private GameObject _menuScreenPanel;
    [SerializeField] private GameObject _winScreenPanel;
    [SerializeField] private GameObject _creditsScreenPanel;

    // In Game Overlay
    [SerializeField] private Text _coinCountField;

    // Tutorial Screen
    [SerializeField] private Button _tutorialHideButton;

    // Start Menu
    [SerializeField] private Button _menuScreenPlayButton;
    [SerializeField] private Button _menuScreenTutorialButton;
    [SerializeField] private Button _menuScreenCreditsButton;

    // Win Screen
    [SerializeField] private Button _winScreenDismissButton;

    // Credits Screen
    [SerializeField] private Button _creditsDismissButton;

    // Start is called before the first frame update
    void Start()
    {
        //_coinCountField.text = "Coins Collected: 32!";
        _gameManager = SceneManager.FindSceneManager().GetGameManager();
        _tutorialPanel.SetActive(false);
        _overlayPanel.SetActive(true);
        _menuScreenPanel.SetActive(true);
        _winScreenPanel.SetActive(false);
        _creditsScreenPanel.SetActive(false);

        setupMenuActions();
        setupTutorialActions();
        setupWinScreenActions();
        setupCreditsScreenActions();

        // initial coin count
        notifyCoinCountChanged();
    }

    // Public Interface
    public void notifyCoinCountChanged()
    {
        int coinCount = _gameManager._coinsCollected;
        int totalCoins = _gameManager._totalCoins;
        _coinCountField.text =  $"{coinCount}/{totalCoins}";
    }

    public void notifyUserWin() {
        _menuScreenPanel.SetActive(false);
        _tutorialPanel.SetActive(false);
        _overlayPanel.SetActive(false);
        _winScreenPanel.SetActive(true);
        _creditsScreenPanel.SetActive(false);
    }

    // In Game Overlay Actions

    // Tutorial Screen Actions
    private void setupTutorialActions()
    {
        _tutorialHideButton.onClick.AddListener(() =>
        {
            hideTutorial();
        });
    }

    private void hideTutorial()
    {
        startScreenPlayGame();
    }

    // Start Menu Actions
    private void setupMenuActions()
    {
        _menuScreenPlayButton.onClick.AddListener(() =>
        {
            startScreenPlayGame();
        });

        _menuScreenTutorialButton.onClick.AddListener(() =>
        {
            startScreenPresentTutorial();
        });

        _menuScreenCreditsButton.onClick.AddListener(() =>
        {
            showCredits();
        });
    }
    private void showCredits()
    {
        _menuScreenPanel.SetActive(false);
        _tutorialPanel.SetActive(false);
        _overlayPanel.SetActive(false);
        _winScreenPanel.SetActive(false);
        _creditsScreenPanel.SetActive(true);
    }

    private void startScreenPresentTutorial()
    {
        _menuScreenPanel.SetActive(false);
        _tutorialPanel.SetActive(true);
        _overlayPanel.SetActive(false);
        _winScreenPanel.SetActive(false);
        _creditsScreenPanel.SetActive(false);
    }

    private void startScreenPlayGame()
    {
        notifyCoinCountChanged();

        _menuScreenPanel.SetActive(false);
        _overlayPanel.SetActive(true);
        _tutorialPanel.SetActive(false);
        _winScreenPanel.SetActive(false);
        _creditsScreenPanel.SetActive(false);
    }

    // Win Screen Actions
    private void setupWinScreenActions() {
        _winScreenDismissButton.onClick.AddListener(() =>
        {
            returnToMainMenu();
        });
    }

    private void returnToMainMenu() {
        _gameManager.hardRestartGame();
        _menuScreenPanel.SetActive(true);
        _tutorialPanel.SetActive(false);
        _overlayPanel.SetActive(false);
        _winScreenPanel.SetActive(false);
        _creditsScreenPanel.SetActive(false);
    }

    // Win Screen Actions
    private void setupCreditsScreenActions()
    {
        _creditsDismissButton.onClick.AddListener(() =>
        {
            dismissCredits();
        });
    }

    private void dismissCredits()
    {
        _menuScreenPanel.SetActive(true);
        _tutorialPanel.SetActive(false);
        _overlayPanel.SetActive(false);
        _winScreenPanel.SetActive(false);
        _creditsScreenPanel.SetActive(false);
    }
}