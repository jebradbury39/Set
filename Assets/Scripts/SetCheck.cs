using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheck
{
   public static string ValidateSet(List<CardObject> cards)
   {
      List<Card> infos = new List<Card>();

      foreach (CardObject card in cards) {
         infos.Add(card.info);
      }

      return ValidateSet(infos);
   }

   public static string ValidateSet(List<Card> cards) {
      HashSet<Card.Shape> shapeSet = new HashSet<Card.Shape>();
      HashSet<Card.Number> numberSet = new HashSet<Card.Number>();
      HashSet<Card.Color> colorSet = new HashSet<Card.Color>();
      HashSet<Card.Fill> fillSet = new HashSet<Card.Fill>();

      string err = "";

      for (int i = 0; i < 3; i++) {
         shapeSet.Add(cards[i].shape);
         numberSet.Add(cards[i].number);
         colorSet.Add(cards[i].color);
         fillSet.Add(cards[i].fill);
      }

      /*
         They all have the same number or have three different numbers.
         They all have the same shape or have three different shapes.
         They all have the same shading or have three different shadings.
         They all have the same color or have three different colors.
      */

      //Debug.Log("number = " + numberSet.Count + ", shape = " + shapeSet.Count + ", fill = " + fillSet.Count + ", color = " + colorSet.Count);
      if (numberSet.Count == 2) {
         err += "Only two cards share the same number\n";
      }
      if (shapeSet.Count == 2) {
         err += "Only two cards share the same shape\n";
      }
      if (fillSet.Count == 2) {
         err += "Only two cards share the same fill\n";
      }
      if (colorSet.Count == 2) {
         err += "Only two cards share the same color\n";
      }

      return err;
   }
}
