using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DRAP.Objectives
{
    public class OnObjectiveComplete : Signal
    {
        public BaseObjective Objective { get; private set; }
        public OnObjectiveComplete(BaseObjective objective)
        {
            this.Objective = objective;
        }
    }

    public class BaseObjective : MonoBehaviour
    {
        [Inject] SignalBus signalBus;
        protected void Complete()
        {
            signalBus.Fire<OnObjectiveComplete>(new OnObjectiveComplete(this));
        }
    }

    public class DestroyObjectObjetive : BaseObjective
    {
        [SerializeField] GameObject target;

        private bool hasFired = false;

        void FixedUpdate()
        {
            if (target == null && !hasFired)
            {
                Complete();
                hasFired = true;
            }
        }
    }
}