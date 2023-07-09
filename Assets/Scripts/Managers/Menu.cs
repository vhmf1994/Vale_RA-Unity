using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button gameButton;

    public void LoadGameScene()
    {
        gameButton.interactable = false;
        GameManager.LoadScene("Game");
    }
}
