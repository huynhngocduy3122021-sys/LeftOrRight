using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
   [SerializeField] private string goalType;

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         GameCOntroller.Instance.checkAnswer(goalType);
         
      }
   }

}
