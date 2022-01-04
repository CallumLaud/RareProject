using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class items : MonoBehaviour//base abstract class for items
{
    public abstract void onUse(Collider2D other);//abstract function for using items such as potions, armour, bombs, gems, etc
}
