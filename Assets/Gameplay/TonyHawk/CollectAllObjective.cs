using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DRAP.Objectives
{
    public class CollectAllObjective : BaseObjective
    {
        [SerializeField] List<Collectible> Collectibles;

        void Awake()
        {
            Collectibles.ForEach(c => c.OnCollect += delegate (Collectible c) { CheckCompletion(c); });
        }

        private void CheckCompletion(Collectible c)
        {
            Collectibles.Remove(c);
            if (Collectibles.Count == 0)
            {
                Complete();
            }
        }
    }
}