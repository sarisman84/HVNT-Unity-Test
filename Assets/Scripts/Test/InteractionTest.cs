using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace HVNT.Test.Runtime
{
    [RequireComponent(typeof(ARRaycastManager))]
    public class InteractionTest : MonoBehaviour
    {
        private ARRaycastManager raycastManager;
        private GameObject spawnedObject;

        private static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        [SerializeField]
        private GameObject placablePrefab;

        void Awake()
        {
            raycastManager = GetComponent<ARRaycastManager>();
        }

        bool TryGetTouchPosition(out Vector2 touchPos)
        {
            if (Input.touchCount > 0)
            {
                touchPos = Input.GetTouch(0).position;
                return true;
            }

            touchPos = default;
            return false;
        }

        void Update()
        {
            if(!TryGetTouchPosition(out var touchPos))
                return;
            
            if(raycastManager.Raycast(touchPos, s_Hits, TrackableType.AllTypes))
            {
                var hitPose = s_Hits[0].pose;
                if(spawnedObject == null)
                {
                    spawnedObject = Instantiate(placablePrefab, hitPose.position, hitPose.rotation);
                }
                else
                {
                    spawnedObject.transform.position = hitPose.position;
                    spawnedObject.transform.rotation = hitPose.rotation;
                }
            }
        }
    }

}
