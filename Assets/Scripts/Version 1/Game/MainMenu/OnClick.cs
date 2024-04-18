using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    public void PickWeapon(int weapon)
    {
        if (weapon == 1)
        {
            if (GameManager.Instance.GetRifleUnlocked())
            {
                GameManager.Instance.SetWeaponType(weapon);

                GameManager.Instance.UpdateGameState(GameManager.GameState.Game);
            }
            else
            {
                Debug.Log("Rifle is locked");
            }
        }
        else
        {
            GameManager.Instance.SetWeaponType(weapon);

            GameManager.Instance.UpdateGameState(GameManager.GameState.Game);
        }
    }
}
