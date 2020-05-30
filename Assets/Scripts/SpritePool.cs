using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePool : MonoBehaviour
{
   public static SpritePool instance;

   private Sprite[] spriteArray;

   void Awake() {
      instance = this;
   }

   public Sprite GetSpriteByName(string name) {
      if (spriteArray == null) {
         spriteArray = Resources.LoadAll<Sprite>("Images/Cards/");
      }

      foreach (Sprite sprite in spriteArray) {
         if (sprite.name == name) {
            return sprite;
         }
      }
      return null;
   }
}
