using Cainos.PixelArtPlatformer_VillageProps;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public event Action<CharacterStatus> OnPotionEnd;

    public void CallPosionEndEvent(CharacterStatus status, float time)
    {
        WaitForIt(time);
        OnPotionEnd?.Invoke(status);
    }

    IEnumerator WaitForIt(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
