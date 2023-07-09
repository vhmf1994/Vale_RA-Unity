using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTargetEvent : MonoBehaviour
{
    [SerializeField] private Game game;

    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;

    private Action onEnable;
    private Action onDisable;

    private float lastTime;

    private bool enabled;
    private bool disabled;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();

        onEnable = () =>
        {
            game.AtivarDesativarPontilhado(false);

            if (animator != null && audioSource != null)
            {
                float currentTime = 0;

                if (game.CheckARCode(this))
                {
                    float animLenght = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
                    currentTime = lastTime / animLenght;

                    audioSource.time = lastTime;
                }
                else
                {
                    game.SetLastARCodeID(this);
                    audioSource.time = 0;
                }

                animator.Play("", -1, currentTime);
                audioSource.Play();
            }
        };

        onDisable = () =>
        {
            game.AtivarDesativarPontilhado(true);

            if (audioSource != null)
            {
                lastTime = audioSource.time;
            }
        };

        enabled = false;
        disabled = false;
    }

    private void OnEnable()
    {
        if (!enabled)
        {
            enabled = true;
            return;
        }

        onEnable.Invoke();
    }

    private void OnDisable()
    {
        if (!disabled)
        {
            disabled = true;
            return;
        }

        onDisable.Invoke();
    }
}
