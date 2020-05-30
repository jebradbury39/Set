using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptionsPanel : MonoBehaviour
{
   public GameObject difficultyPanel;

   // Start is called before the first frame update
   void Start()
   {
       difficultyPanel.SetActive(false);
   }

   // Update is called once per frame
   void Update()
   {
       
   }

   public void ToggleDifficulty(bool val)
   {
      Debug.Log("toggle = " + val);
      //bool val = GetComponent<Toggle>().isOn;
      difficultyPanel.SetActive(val);
   }

}
