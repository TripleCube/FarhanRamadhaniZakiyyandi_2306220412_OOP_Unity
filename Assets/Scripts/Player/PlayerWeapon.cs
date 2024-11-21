using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    Weapon1 weapon;

    public Weapon1 SetWeapon(Weapon1 weapon)
    {
        this.weapon = weapon;
        return weapon;
    }
}
