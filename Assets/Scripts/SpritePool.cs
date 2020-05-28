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

   // Start is called before the first frame update
   void Start()
   {
      spriteArray = Resources.LoadAll<Sprite>("Images/Cards/");
   }

   public Sprite GetSpriteByName(string name) {
      foreach (Sprite sprite in spriteArray) {
         if (sprite.name == name) {
            return sprite;
         }
      }
      return null;
   }
}
