using UnityEngine;

namespace Player
{
    public class State : StateMachine
    {
        public Controller Controller;

        public State(Controller controller)
        {
            Controller = controller;
        }

        protected bool Idle()
        {
            if (Mathf.Abs(Input.GetAxisRaw(PlayerInput.Horizontal)) <= Mathf.Epsilon
                && Mathf.Abs(Input.GetAxisRaw(PlayerInput.Vertical)) <= Mathf.Epsilon)
            {
                Controller.SetState(new Idle(Controller));
                return true;
            }
            return false;
        }

        protected bool Walk()
        {
            if (Mathf.Abs(Input.GetAxisRaw(PlayerInput.Horizontal)) > 0
                || Mathf.Abs(Input.GetAxisRaw(PlayerInput.Vertical)) > 0)
            {
                Controller.SetState(new Walk(Controller));
                return true;
            }
            return false;
        }

        protected bool Run()
        {
            if (Input.GetButton(PlayerInput.Run))
            {
                Controller.SetState(new Run(Controller));
                return true;
            }
            return false;
        }

        protected bool Dragging()
        {
            if (Input.GetButton(PlayerInput.Interact) && Controller.SelectedObject != null)
            {
                if (!Controller.SelectedObject.CompareTag(Tag.Draggable)) return false;
                Controller.SetState(new Dragging(Controller));
                return true;
            }
            return false;
        }
    }

    class Idle : State
    {
        public Idle(Controller controller) : base(controller) {}

        public override void DoStateBehaviour()
        {
            Controller.GetComponent<SpriteRenderer>().color = Color.white;
        }

        public override void Transitions()
        {
            if (Walk()) {}
            else if (Dragging()) {}
        }
    }

    class Walk : State
    {
        public Walk(Controller controller) : base(controller) {}

        protected void Move(float speed)
        {
            float verticalSpeedRatio = 0.75f;
            Vector2 direction = new Vector2(Input.GetAxisRaw(PlayerInput.Horizontal), Input.GetAxisRaw(PlayerInput.Vertical) * verticalSpeedRatio);
            Vector2 velocity = Controller.Rigidbody2d.position + direction * speed * Time.fixedDeltaTime;
            Controller.Rigidbody2d.MovePosition(velocity);
        }

        private void SetDirection()
        {
            if (Mathf.Abs(Input.GetAxisRaw(PlayerInput.Horizontal)) > 0)
                Controller.Direction = new Vector2(Input.GetAxisRaw(PlayerInput.Horizontal), 1);
            Controller.transform.localScale = Controller.Direction;
        }

        public override void DoStateBehaviour()
        {
            Controller.GetComponent<SpriteRenderer>().color = Color.yellow;
            SetDirection();
        }

        public override void DoStateBehaviourFixedUpdate()
        {
            Move(Controller.WalkSpeed);
        }

        public override void Transitions()
        {
            if      (Idle()) {}
            else if (Run())  {}
            else if (Dragging()) {}
        }
    }

    class Run : Walk
    {
        public Run(Controller controller) : base(controller) {}

        public override void DoStateBehaviour()
        {
            base.DoStateBehaviour();
            Controller.GetComponent<SpriteRenderer>().color = Color.red;
        }

        public override void DoStateBehaviourFixedUpdate()
        {
            Move(Controller.RunSpeed);
        }

        public override void Transitions()
        {
            if (Idle()) {}
            else if (Dragging()) {}
            if (Input.GetButton(PlayerInput.Run)) return;
            if (Walk()) {}
        }
    }

    class Dragging : State
    {
        private const float _verticalSpeedRatio = 0.75f;

        public Dragging(Controller controller) : base(controller) {}

        public override void EnterState()
        {
            Controller.ApplySpeedMultiplier(0.5f);
            //NoiseEvents.Instance.DragTriggerEnter();
        }

        public override void DoStateBehaviour()
        {
            Controller.GetComponent<SpriteRenderer>().color = Color.blue;
            //NoiseEvents.Instance.DragTriggerStay();
        }

        public override void DoStateBehaviourFixedUpdate()
        {
            if ((Mathf.Abs(Input.GetAxisRaw(PlayerInput.Horizontal)) < Mathf.Epsilon
                && Mathf.Abs(Input.GetAxisRaw(PlayerInput.Vertical)) < Mathf.Epsilon)
                || Controller.SelectedObject == null)
                return;

            Vector2 direction = new Vector2(Input.GetAxisRaw(PlayerInput.Horizontal), Input.GetAxisRaw(PlayerInput.Vertical) * _verticalSpeedRatio);
            Vector2 velocity = direction * Controller.WalkSpeed * Time.fixedDeltaTime;

            Controller.Rigidbody2d.MovePosition(Controller.Rigidbody2d.position + velocity);
            Controller.SelectedObject.GetComponent<Rigidbody2D>().MovePosition((Vector2)Controller.SelectedObject.transform.position + velocity);
        }

        public override void ExitState()
        {
            Controller.ResetSpeedMultiplier();
            //NoiseEvents.Instance.DragTriggerExit();
        }

        public override void Transitions()
        {
            if (Input.GetButton(PlayerInput.Interact)
                && Controller.SelectedObject != null)
                return;
            if (Idle()) {}
            else if (Walk()) {}
            else if (Run()) {}
        }
    }
}