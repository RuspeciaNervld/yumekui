using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIn : MonoBehaviour
{
    //public bool weaponInBody;
    public ICreature creature;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Weapon")) {
            //weaponInBody = true;
            if (collision.gameObject.GetComponent<IWeapon>()) {
                creature.beHurtController.beHurt(collision.gameObject.GetComponent<IWeapon>().computedAttack);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        //if (collision.CompareTag("Weapon")) {
        //    //weaponInBody = false;
        //}
    }
}
