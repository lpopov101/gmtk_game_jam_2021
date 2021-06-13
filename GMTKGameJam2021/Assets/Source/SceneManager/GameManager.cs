using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManager
{
    public int _coinsCollected {get; private set;}
    public int _totalCoins {get;}

    public GameManager(int totalCoins)
    {
        _totalCoins = totalCoins;
        _coinsCollected = 0;
    }

    public void CollectCoin()
    {
        _coinsCollected++;
        Debug.Log($"Coins collected: {_coinsCollected}/{_totalCoins}");

        UIController uiController = SceneManager.FindUIController();
        if (uiController != null) {
            uiController.notifyCoinCountChanged();
        }

        if (_coinsCollected >= _totalCoins)
        {
            Win();
        }
    }

    private T FindObjectOfType<T>()
    {
        throw new NotImplementedException();
    }

    private void Win()
    {
        Debug.Log("Win!");
        // TODO: Implement win condition
    }
}
