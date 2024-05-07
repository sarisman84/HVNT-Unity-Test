using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace HVNT.Test.Runtime
{
    public class InteractableChest : MonoBehaviour
    {
        private Animator anim;

        void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            ProjectSystemLocator.Service.SetSpawnPoint(transform.position);

            ProjectSystemLocator.Service.onReset += () => { anim.Play("Idle"); };
        }

        /// <summary>
        /// Opens the chest. Nothing happens if <see cref="ProjectSystemLocator.PointAmount"/> is less than 2 
        /// </summary>
        public void OnChestOpen()
        {
            if (ProjectSystemLocator.Service.PointAmount() < 2)
                return;

            anim.Play("Open");
            ProjectSystemLocator.Service.Start();
        }


    }
}

