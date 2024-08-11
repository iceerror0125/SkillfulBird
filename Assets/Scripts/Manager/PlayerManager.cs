using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMono<PlayerManager>
{
    [SerializeField] private Player player;

    public Player Player => player;
}
