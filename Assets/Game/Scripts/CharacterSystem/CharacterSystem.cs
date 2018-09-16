using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generate hero from playerData.
/// </summary>
public class CharacterSystem : MonoBehaviour
{
    private GameManager manager;
    public Hero hero;

    public void Initialize(GameManager manager)
    {
        this.manager = manager;
        hero = GenerateHero(manager.mockHeroStatus);
    }

    private Hero GenerateHero(HeroData data)
    {
        GameObject hero = Instantiate(Resources.Load("Characters/" + data.name) as GameObject);
        Hero heroComponent = hero.GetComponent<Hero>();
        heroComponent.name = data.name;
        heroComponent.status = data;
        return heroComponent;
    }
}
