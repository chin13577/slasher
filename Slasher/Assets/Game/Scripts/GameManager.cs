using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("MockupData")]
    public HeroData mockHeroStatus;
    [Header("Game")]
    public InputSystem inputSystem;
    public CharacterSystem characterSystem;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        inputSystem.Initialize(this);
        characterSystem.Initialize(this);
        inputSystem.SetReceiver(characterSystem.hero);
    }
}
