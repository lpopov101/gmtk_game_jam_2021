using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Sub Panels
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private GameObject _overlayPanel;
    [SerializeField] private GameObject _menuScreenPanel;

    // In Game Overlay
    [SerializeField] private Text _coinCountField;

    // Tutorial Screen
    [SerializeField] private Button _tutorialHideButton;

    // Start Menu
    [SerializeField] private Button _menuScreenPlayButton;
    [SerializeField] private Button _menuScreenTutorialButton;

    // Start is called before the first frame update
    void Start() {
        //_coinCountField.text = "Coins Collected: 32!";
        _tutorialPanel.SetActive(false);
        _overlayPanel.SetActive(true);
        _menuScreenPanel.SetActive(true);

        setupMenuActions();
        setupTutorialActions();
    }

    // In Game Overlay Actions

    // Tutorial Screen Actions
    private void setupTutorialActions() {
        _tutorialHideButton.onClick.AddListener(() => {
            hideTutorial();
        });
    }

    private void hideTutorial() {
        _tutorialPanel.SetActive(false);
    }

    // Start Menu Actions
    private void setupMenuActions()
    {
        _menuScreenPlayButton.onClick.AddListener(() => {
            startScreenPlayGame();
        });

        _menuScreenTutorialButton.onClick.AddListener(() => {
            startScreenPresentTutorial();
        });
    }

    private void startScreenPresentTutorial()
    {
        _menuScreenPanel.SetActive(false);
        _tutorialPanel.SetActive(true);
        _overlayPanel.SetActive(true);
    }

    private void startScreenPlayGame()
    {
        _menuScreenPanel.SetActive(false);
        _overlayPanel.SetActive(true);
        _tutorialPanel.SetActive(false);
    }


}
