using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheats : MonoBehaviour
{
#if UNITY_EDITOR

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))    
        {
            EventManager.Instance.DispatchEvent(EventManager.EventType.PlayerTakeRedKey, gameObject);
            EventManager.Instance.DispatchEvent(EventManager.EventType.PlayerTakeBlueKey, gameObject);
            EventManager.Instance.DispatchEvent(EventManager.EventType.PlayerTakeYellowKey, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            UIMessage.Instance.ShowMessage("Double <size=150%><color=red>GUN</color></size>\nDouble <size=150%><color=red>FUN</color></size>");
            GameObject.FindObjectOfType<PlayerAttack>().BonusDamage();
        }
    }

#endif
}
