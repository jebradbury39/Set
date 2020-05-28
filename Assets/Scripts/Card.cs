using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
   public enum Shape {
      TRIANGLE = 'T',
      OVAL = 'O',
      SQUIGGLE = 'S'
   }

   public enum Number {
      ONE = '1',
      TWO = '2',
      THREE = '3'
   }

   public enum Color {
      RED = 'R',
      GREEN = 'G',
      BLUE = 'B'
   }

   public enum Fill {
      NONE = 'N',
      PARTIAL = 'P',
      FULL = 'F'
   }

   Shape shape = Shape.TRIANGLE;
   Number number = Number.ONE;
   Color color = Color.RED;
   Fill fill = Fill.NONE;

   public Card(Shape shape, Number number, Color color, Fill fill)
   {
      this.shape = shape;
      this.number = number;
      this.color = color;
      this.fill = fill;
   }

   public string toString()
   {
      return (char)shape + "_" + (char)number + "_" + (char)color + "_" + (char)fill;
   }

}
