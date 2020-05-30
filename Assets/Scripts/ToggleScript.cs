using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{
   public MenuOptions.Difficulty difficulty = MenuOptions.Difficulty.INVALID;
   public MenuOptions.GameMode gamemode = MenuOptions.GameMode.INVALID;

   public void SetOption(bool val) {
      if (!val) {
         return;
      }

      if (difficulty != MenuOptions.Difficulty.INVALID) {
         MenuOptions.difficulty = difficulty;
         return;
      }

      if (gamemode != MenuOptions.GameMode.INVALID) {
         MenuOptions.gamemode = gamemode;
         return;
      }
   }
}
