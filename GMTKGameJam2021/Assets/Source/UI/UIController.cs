using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Sub Panels
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private GameObject _overlayPanel;

    // In Game Overlay
    [SerializeField] private Text _coinCountField;

    // Tutorial Screen
    [SerializeField] private Button _tutorialHideButton;

    // Start Menu

    // Start is called before the first frame update
    void Start() {
        //_coinCountField.text = "Coins Collected: 32!";
        _tutorialPanel.SetActive(true);
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
}
