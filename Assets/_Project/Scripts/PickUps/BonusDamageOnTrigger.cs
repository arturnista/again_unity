using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDamageOnTrigger : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider collider)
    {
        UIMessage.Instance.ShowMessage("Double <size=150%><color=red>GUN</color></size>\nDouble <size=150%><color=red>FUN</color></size>");
        GameObject.FindObjectOfType<PlayerAttack>().BonusDamage();
        Destroy(gameObject);
    }

}
