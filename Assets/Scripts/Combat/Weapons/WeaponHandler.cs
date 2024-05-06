using UnityEngine;

namespace Ludias.Combat
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] GameObject weaponLogic;

        public void EnableWeapon()
        {
            weaponLogic.SetActive(true);
        }

        public void DisableWeapon()
        {
            weaponLogic.SetActive(false);
        }
    }
}
