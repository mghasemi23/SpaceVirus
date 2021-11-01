using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class TouchHandler : MonoBehaviour
    {
        public Camera MainCamera;

        private const int MaxTouch = 3;


        [UsedImplicitly]
        private void Update()
        {
            if (Input.touchCount <= 0)
                return;

            for (var i = 0; i < Input.touchCount; i++)
            {
                var theTouch = Input.GetTouch(i);

                if (theTouch.fingerId >= MaxTouch)
                    return;

                switch (theTouch.phase)
                {
                    case TouchPhase.Began:
                        EventBroker.CallMakeBlackHole(theTouch.fingerId, MainCamera.ScreenToWorldPoint(theTouch.position));
                        break;
                    case TouchPhase.Ended:
                        EventBroker.CallDestroyBlackHole(theTouch.fingerId);
                        break;
                }
            }
        }

        //void Update()
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        EventBroker.CallMakeBlackHole(1, MainCamera.ScreenToWorldPoint(Input.mousePosition));
        //    }

        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        EventBroker.CallDestroyBlackHole(1);
        //    }
        //}
    }
}
