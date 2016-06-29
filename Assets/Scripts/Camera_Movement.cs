using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Camera_Movement : MonoBehaviour
    {


        public float interpVelocity;
        public GameObject target;
        public Vector3 offset = Vector3.zero;
        Vector3 targetPos;
        Camera cameraProperties;

        float bottomLimit, topLimit, leftLimit, rightLimit;

        // Use this for initialization
        void Start()
        {
            targetPos = transform.position;
            cameraProperties = this.GetComponent<Camera>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (target)
            {
                Vector3 posNoZ = transform.position;
                posNoZ.z = target.transform.position.z;

                Vector3 targetDirection = (target.transform.position - posNoZ);

                interpVelocity = targetDirection.magnitude * 8f;

                targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);
                transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
                /*
                bottomLimit = transform.position.y - cameraProperties.orthographicSize + 1;
                topLimit = transform.position.y + cameraProperties.orthographicSize - 1;
                leftLimit = transform.position.x - cameraProperties.orthographicSize + 2;
                rightLimit = transform.position.x + cameraProperties.orthographicSize - 2;

                if ((bottomLimit >= target.transform.position.y && leftLimit >= target.transform.position.x) ||
                     (bottomLimit >= target.transform.position.y && rightLimit <= target.transform.position.x) ||
                    (topLimit <= target.transform.position.y && leftLimit >= target.transform.position.x) ||
                    (topLimit <= target.transform.position.y && rightLimit <= target.transform.position.x))
                    transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
                    */
            }
        }

    }
}