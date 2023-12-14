using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDt : MonoBehaviour
{
  public ZombieHand zombieHand;
  public int zombieDamage;

  private void Start()
  {
    zombieHand.damage = zombieDamage;
  }
}
