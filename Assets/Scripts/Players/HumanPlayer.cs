using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player
{
   
   public TMPro.TextMeshProUGUI setMsg;

   // Start is called before the first frame update
   void Start()
   {
      scorePrefix = "Sets: ";
      SetScore();
   }

   // Update is called once per frame
   void Update()
   {
       
   }

   public override void SetMsg(string msg)
   {
      setMsg.text = msg;
   }

   public override void Reset()
   {
      base.Reset();
      
      setMsg.text = "Welcome to SET!";
   }
}
