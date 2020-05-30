using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer : Player
{
   public TMPro.TextMeshProUGUI timerMsg;
   public float maxTimeLeft = 30.0f;
   
   private float timeLeft = 30.0f;

   // Start is called before the first frame update
   void Start()
   {
      scorePrefix = "Computer Sets: ";
      SetScore();
   }

   // Update is called once per frame
   void Update()
   {
      timeLeft -= Time.deltaTime;
      if (timeLeft <= 0.0f) {
         //select the hint cards
         SelectHintCards();

         //restart timer
         timeLeft = maxTimeLeft;
      }

      //update timer text
      int t = ((int)timeLeft);
      string txt = "00:";
      if (t < 10) {
         txt += "0";
      }
      timerMsg.text = txt + t;
   }

   public override void Reset()
   {
      base.Reset();
      ResetTimer();
   }

   public override void SetActive(bool val)
   {
      base.SetActive(val);
      timerMsg.gameObject.SetActive(val);
      ResetTimer();
   }

   void SelectHintCards()
   {
      List<CardObject> cards = GameManager.instance.cardManager.GetHintSetObj();
      CollectSet(cards);
      SetScore();
   }

   public void ResetTimer()
   {
      timeLeft = maxTimeLeft;
   }
}
