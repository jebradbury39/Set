using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VsCardManager : CardManager
{
   // Start is called before the first frame update
   void Start()
   {
       
   }

   // Update is called once per frame
   void Update()
   {
       
   }

   public void ApplyDifficulty(MenuOptions.Difficulty difficulty)
   {
      switch (difficulty) {
      case MenuOptions.Difficulty.EASY:
         GameManager.instance.computerPlayer.maxTimeLeft = 30.0f;
         break;
      case MenuOptions.Difficulty.MEDIUM:
         GameManager.instance.computerPlayer.maxTimeLeft = 20.0f;
         break;
      case MenuOptions.Difficulty.HARD:
         GameManager.instance.computerPlayer.maxTimeLeft = 10.0f;
         break;
      default:
         break;
      }
   }
}
