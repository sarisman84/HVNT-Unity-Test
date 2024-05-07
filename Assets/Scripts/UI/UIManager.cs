using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace HVNT.Test.Runtime.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class UIManager : MonoBehaviour
    {
        private UIDocument doc;

        void Start()
        {
            doc = GetComponent<UIDocument>();
            InitCallbacks(doc.rootVisualElement);
        }

        private void InitCallbacks(VisualElement root)
        {
            var resetButton = root.Q<Button>("reset");
            var addPointButton = root.Q<Button>("add_point");
            var removeLastPointButton = root.Q<Button>("remove_point");

            resetButton.RegisterCallback<ClickEvent>((e) => { ProjectSystemLocator.Service.ResetAll(); });

            addPointButton.RegisterCallback<ClickEvent>((e) =>
            {
                var cam = ProjectSystemLocator.Service.Cam;
                var pos = cam.transform.position + cam.transform.forward.normalized;

                ProjectSystemLocator.Service.AddPoint(pos);
            });

            removeLastPointButton.RegisterCallback<ClickEvent>((e) => { ProjectSystemLocator.Service.RemoveLastPoint(); });

        }
    }
}
