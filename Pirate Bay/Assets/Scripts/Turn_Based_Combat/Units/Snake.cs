using UnityEngine;
using System.Collections;
using System;

public class Snake : Enemy
{
    protected override void SetAbility()
    {
        ability = new AbilityVenom();
    }
}
