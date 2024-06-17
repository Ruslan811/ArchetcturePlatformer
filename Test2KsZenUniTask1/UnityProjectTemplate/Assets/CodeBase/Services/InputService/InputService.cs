using UnityEngine;

namespace CodeBase.Services.InputService
{
    public class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string Jump = "Jump";

        public Vector2 Axis => InputAxis();

        /*protected static Vector2 SimpleInputAxis()
        {
            return new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        }*/

        private static Vector2 InputAxis() => new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));

        public bool IsJumpButtonUp() => Input.GetButtonUp(Jump);
    }
}