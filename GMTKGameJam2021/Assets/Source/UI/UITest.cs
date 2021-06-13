using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Button _button;
    // Start is called before the first frame update
    void Start()
    {
        _button.onClick.AddListener(() => {
            _text.text = "Yay!";
        });
    }
}
