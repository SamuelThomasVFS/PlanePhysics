using UnityEngine;

public class AreaCalculator : MonoBehaviour
{
    [SerializeField] private Transform _checkBoxProbeOrigin;
    [SerializeField] private Vector2 _checkBoxDimensions = Vector2.zero;
    [SerializeField] private float _probeDensity = 1.0f;    // probes per meter

    [SerializeField, Header("Testing")] private bool _snapshotTest = false;

    private void Update()
    {
        if (_snapshotTest)
        {
            float test = Return2DArea(); // Dummy test
            _snapshotTest = false;
        }
    }

    public float Return2DArea()
    {
        int positiveContacts = RunOriginPositionCircuit();
        float totalContacts = _checkBoxDimensions.x  * _checkBoxDimensions.y  * Mathf.Pow(_probeDensity, 2);
        float contactRatio = positiveContacts / totalContacts;
        float area = _checkBoxDimensions.x * _checkBoxDimensions.y * contactRatio;
        Debug.Log(positiveContacts + "/" + totalContacts + "=" + contactRatio + "=" + area);
        return area;
    }
    
    // Fire a raycast from the check direction in a strafe motion across object in question
    private int RunOriginPositionCircuit()
    {
        int contacts = 0;
        for (float positionX = -_checkBoxDimensions.x / 2; positionX < _checkBoxDimensions.x / 2 + 1 / _probeDensity; positionX += 1 / _probeDensity) 
        {
            for (float positionY = -_checkBoxDimensions.y / 2; positionY < _checkBoxDimensions.y / 2 + 1 / _probeDensity; positionY += 1 / _probeDensity) 
            {
                Vector3 xAddition = _checkBoxProbeOrigin.right * positionX;
                Vector3 yAddition = _checkBoxProbeOrigin.up * positionY;
                Vector3 position = _checkBoxProbeOrigin.position + xAddition + yAddition;
                if (IsSomethingDetected(_checkBoxProbeOrigin, position))
                {
                    contacts++;
                }
            }
        }
        return contacts;
    }

    private bool IsSomethingDetected(Transform origin,Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position, origin.forward))
        {
            Debug.DrawRay(position, origin.forward, Color.red,10f);
            return true;
        }
        Debug.DrawRay(position, origin.forward, Color.blue,10f);
        return false;
    }
}
