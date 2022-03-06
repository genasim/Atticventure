using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{    
    public class ButtonHeld : Button
    {
        public bool pressed = false;

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            pressed = true;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            pressed = false;
            //hide text
        }
    }
}