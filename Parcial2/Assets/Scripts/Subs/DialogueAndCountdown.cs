using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAndCountdown : Dialogue
{
    public Countdown countdown;
    private void OnTriggerEnter(Collider player) //los dialogue se disparan por con colliders
    {
        if (player.gameObject.layer == 3) //la 3 es la del player, obvio
        {
            Show(desiredText, desiredTime);
            countdown.TurnOnAllCountdownTexts();
        }
    }
}
