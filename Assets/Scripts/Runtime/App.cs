using UnityEngine;

namespace HVNT.Test.Runtime
{
    /// <summary>
    /// <see cref="App"/> is responsible for initializing any services created (currently, it is only the <see cref="ProjectSystemLocator"/>) 
    /// </summary>
    public class App : MonoBehaviour
    {
        [SerializeField]
        private GameObject pointPrefab;

        [SerializeField]
        private BallController ballPrefab;

        void Awake()
        {
            ProjectSystemLocator.Service.SetPointPrefab(pointPrefab);
            ProjectSystemLocator.Service.SetBallPrefab(ballPrefab);
        }
    }
}
