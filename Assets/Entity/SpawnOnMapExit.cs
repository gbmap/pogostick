using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DRAP
{
    public class SpawnOnMapExit : MonoBehaviour
    {
        CharacterMovement movement;
        [Inject] ILevelBounds levelBounds;

        public EBoundsSide[] mask = { EBoundsSide.Bot };

        // Start is called before the first frame update
        void Awake()
        {
            movement = GetComponent<CharacterMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            if (levelBounds == null)
                return; 
                
            if (!levelBounds.IsInsideLevel(transform.position, mask))
            {
                movement.ReturnToLastGroundPosition();
            }
        }

    }
}