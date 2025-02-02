using UnityEngine;

public class RotateHandle : MonoBehaviour
{
    public void PullHandle()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles * -1f);
    }
}
