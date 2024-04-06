using Firebase.Analytics;
using Firebase;
using UnityEngine;
 
namespace Analytics
{
    public class GameAnalytics : MonoBehaviour
    {
        public static GameAnalytics Instance;
        
        private bool _canUseAnalytics;
        
        private void Awake()
        {
             if (Instance == null)
             {
                 Instance = this;
                 DontDestroyOnLoad(gameObject);
             }
             else
                 Destroy(gameObject);
                
             FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                 var dependencyStatus = task.Result;
                 if (dependencyStatus == Firebase.DependencyStatus.Available) {
                     _canUseAnalytics = true;
                     //var app = FirebaseApp.DefaultInstance;
                 } 
                 else 
                 {
                     Debug.LogError(System.String.Format(
                         "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                 }
             });
        }


         public void Score(int score)
         {
             if(!_canUseAnalytics)
                 return;
             FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventPostScore, new Parameter(FirebaseAnalytics.ParameterScore,score));
         }
		    
         public void LogEvent(string eventName)
         {
             if(!_canUseAnalytics)
                 return;
                
             FirebaseAnalytics.LogEvent(eventName);
         }
    }
}