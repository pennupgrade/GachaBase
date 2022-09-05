namespace GachaBase
{
    using System;
    using UnityEngine;

    /// <summary>
    /// A monster behavior that allows the player to spin chad to get some money.
    /// </summary>
    public class LennyBehavior : MonoBehaviour
    {
        private bool isSpinning;
        private bool waitingForStop;

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {
            if (this.waitingForStop)
            {
                // If we have almost stopped rotating, set freeze velocity.
                if (Math.Abs(this.GetComponent<Rigidbody2D>().angularVelocity) < 2f)
                {
                    Debug.Log("Stopped spinning");
                    this.GetComponent<Rigidbody2D>().angularVelocity = 0;

                    // If we are close to the original orientation, give the user
                    // a bunch of money. Otherwise, subtract some.
                    if (Math.Abs(this.transform.eulerAngles.z) < 10)
                    {
                        CurrencyManager.Instance.Currency += 5;
                        Debug.Log("Yay!");
                    }
                    else
                    {
                        CurrencyManager.Instance.Currency -= 1;
                        Debug.Log("No :(");
                    }

                    this.waitingForStop = false;
                }
            }
            else
            {
                // Not waiting for end, check if user is clicking us.
                if (this.DidClickChad())
                {
                    if (!this.isSpinning)
                    {
                        this.StartSpin();
                    }
                    else
                    {
                        this.StopSpinning();
                    }
                }
            }
        }

        private void StartSpin()
        {
            // Before we add force, we ensure we have no drag so we rotate indefinitely.
            this.GetComponent<Rigidbody2D>().angularDrag = 0;
            this.GetComponent<Rigidbody2D>().AddTorque(-20f, ForceMode2D.Impulse);
            this.isSpinning = true;
        }

        private void StopSpinning()
        {
            // Give us some drag to slowly stop spinning.
            this.GetComponent<Rigidbody2D>().angularDrag = 0.5f;
            this.isSpinning = false;
            this.waitingForStop = true;
        }

        private bool DidClickChad()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Checking if the click will hit this monster's box collider .
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
                return hit.collider != null && hit.collider.gameObject == this.gameObject;
            }

            return false;
        }
    }
}
