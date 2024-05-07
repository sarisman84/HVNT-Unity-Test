using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace HVNT.Test.Runtime
{
    public class PlayerController : MonoBehaviour
    {
        private Camera _cam;
        void Awake()
        {
            _cam = ProjectSystemLocator.Service.Cam;
        }

        void FixedUpdate()
        {   
            //Casts a ray from the camera position that looks at the camera forward direction
            var hitSomething = Physics.Raycast(_cam.transform.position, _cam.transform.forward.normalized, out var hit);

            //Checks if the user has touched the screen
            var hasTouchedScreen = Input.touchCount > 0;

            if (hitSomething && hit.collider.GetComponent<InteractableChest>() && hasTouchedScreen)
            {
                var ic = hit.collider.GetComponent<InteractableChest>();
                ic.OnChestOpen();
            }
        }
    }
}
