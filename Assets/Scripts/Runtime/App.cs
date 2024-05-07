using UnityEngine;

namespace HVNT.Test.Runtime
{
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
