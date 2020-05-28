using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public static Player instance;

   private List<CardObject> selectedCards = new List<CardObject>();
   public List<Card> sets = new List<Card>(); //multiple of 3

   void Awake()
   {
      instance = this;
   }

   // Start is called before the first frame update
   void Start()
   {
       
   }

   // Update is called once per frame
   void Update()
   {
       
   }

   public void SelectCard(CardObject card)
   {
      selectedCards.Add(card);
      if (selectedCards.Count == 3) {
         if (SetCheck.ValidateSet(selectedCards)) {
            CollectSet(selectedCards);
         } else {
            if (sets.Count > 0) {
               List<Card> giveBack = new List<Card>();
               for (int i = 0; i < 3; i++) {
                  giveBack.Add(sets[0]);
                  sets.RemoveAt(0);
               }
               CardManager.instance.ReshuffleCards(giveBack);
            }
         }
         foreach (CardObject selected in selectedCards) {
            selected.Release();
         }
         selectedCards.Clear();
      }
   }

   public void DeselectCard(CardObject card)
   {
      //find card index, then remove
      int cardIdx = -1;
      int i = 0;
      foreach (CardObject selected in selectedCards) {
         if (selected.ToString() == card.ToString()) {
            cardIdx = i;
            break;
         }
         i += 1;
      }
      if (cardIdx != -1) {
         selectedCards.RemoveAt(cardIdx);
      }
   }

   void CollectSet(List<CardObject> cards)
   {
      foreach (CardObject card in cards) {
         card.TakeCard();
         sets.Add(card.info);
      }
   }
}
