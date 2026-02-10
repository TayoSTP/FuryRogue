using UnityEngine;

public class AI_Boss : MonoBehaviour
{
    private GameObject _player;
    bool _fight = false;
    bool _seePlayer = false;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);
        if (distance < 20f)
        {
         _fight = true;   
        }
        Vector3 fwd = transform.TransformDirection(Vector3.forward) + new Vector3(0,1,0);
        if (Physics.Raycast(transform.position, fwd, out RaycastHit hit, distance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                _seePlayer = true;
            }
        }
    }
}
