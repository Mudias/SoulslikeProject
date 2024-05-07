using UnityEngine;

namespace Ludias.Combat
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] GameObject[] weaponLogicArray;

        public void EnableWeapon()
        {
            foreach (GameObject weapon in weaponLogicArray)
            {
                weapon.SetActive(true);
            }
        }

        public void DisableWeapon()
        {
            foreach (GameObject weapon in weaponLogicArray)
            {
                weapon.SetActive(false);
            }
        }
    }
}
