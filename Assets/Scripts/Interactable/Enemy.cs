using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable
{
    CharacterCombat playerCombat;
    CharacterStats _stats;

    private void Start()
    {
        playerCombat = PlayerManager.sharedInstance.player.GetComponent<CharacterCombat>();
        _stats = this.GetComponent<CharacterStats>();
    }
    public override void Interact()
    {
        base.Interact();
        //Attack enemy


        if (playerCombat != null)
        {
            StartCoroutine(Attack());
        }

    }
    IEnumerator Attack()
    {
        while (isFocus)
        {
            playerCombat.Attack(_stats);
            yield return new WaitForEndOfFrame();
        }
    }
}
