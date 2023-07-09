using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Button menuButton;

    [SerializeField] private CanvasGroup pontilhadoCanvasGroup;

    private int lastARCodeID;

    private void Start()
    {
        Application.RequestUserAuthorization(UserAuthorization.WebCam);
    }

    public void LoadMenuScene()
    {
        menuButton.interactable = false;
        GameManager.LoadScene("Menu");
    }

    public void AtivarDesativarPontilhado(bool ativar)
    {
        StartCoroutine(PontilhadoEfeito(ativar));
    }

    private IEnumerator PontilhadoEfeito(bool ativar)
    {
        float t;
        float time = .25f;

        for (t = 0; t < time; t += Time.deltaTime)
        {
            pontilhadoCanvasGroup.alpha = ativar ? (t / time) : (1 - (t / time));
            yield return null;
        }

        pontilhadoCanvasGroup.alpha = ativar ? 1 : 0;
    }

    public bool CheckARCode(Object obj) => obj.GetInstanceID() == lastARCodeID;

    public void SetLastARCodeID(Object obj) => lastARCodeID = obj.GetInstanceID();
}
