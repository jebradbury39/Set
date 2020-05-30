using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{
   public Card info;

   private SpriteRenderer spriteRenderer;
   private bool selected = false;

   public ParticleSystem particles;
   public SquishyScale hint;

   public void SetSelected(bool val)
   {
      selected = val;
      if (selected) {
         particles.Play();   
      } else {
         particles.Stop();   
      }
   }

   // Start is called before the first frame update
   void Start()
   {
      spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
      particles.Stop();   
   }

   // Update is called once per frame
   void Update()
   {
   }

   public override string ToString()
   {
      return info.toString();
   }

   public void SetActive(bool active) {
      gameObject.SetActive(active);
      if (active) {
         Sprite sprite = SpritePool.instance.GetSpriteByName(info.toString());
         if (spriteRenderer == null) {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
         }
         spriteRenderer.sprite = sprite;
         
         //reset
         SetSelected(false);
         SetHintHighlight(false);
      }
   }

   //only human player will trigger mouse clicks
   void OnMouseDown() {
      if (!selected) {
         SetSelected(true);
         GameManager.instance.humanPlayer.SelectCard(this);
      } else {
         SetSelected(false);
         GameManager.instance.humanPlayer.DeselectCard(this);
      }
   }

   public void Release() {
      SetSelected(false);
      SetHintHighlight(false);
   }

   public void TakeCard() {
      GameManager.instance.cardManager.TakeCard(info);
      SetActive(false);
   }

   public void SetHintHighlight(bool val)
   {
      hint.run = val;
   }
}
