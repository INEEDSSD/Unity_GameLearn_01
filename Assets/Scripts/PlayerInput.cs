using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
public class PlayerInput : MonoBehaviour
{

    // variable
    public string keyUp;   
    public string keyDown;
    public string keyLeft;
    public string keyRight;

    // flag
    // 是否开启此模块
    public bool PlayerInputEnabled = true;
    
    // signal
    public float DUp;   // DirectionUp
    public float DRight;    // DirrectionRight

    public float DMag; // DirectionMagnetic
    public Vector3 DVec; // DirectionVector

    private float targetDUp;
    private float targetDRight;

    private float velocityDUp;

    private float VelocityDRight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 按键输入
        targetDUp = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDRight = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        if (!PlayerInputEnabled)
        {
            targetDUp = 0;
            targetDRight = 0;
        }
        
        // 实现信号量的平滑过渡
        DUp = Mathf.SmoothDamp(DUp, targetDUp, ref velocityDUp, 0.1f);
        DRight = Mathf.SmoothDamp(DRight, targetDRight, ref VelocityDRight, 0.1f);
        DMag = Mathf.Sqrt(DUp * DUp + DRight * DRight) > 1.0f ? 1.0f : Mathf.Sqrt(DUp * DUp + DRight * DRight);
        DVec = DRight * transform.right + DUp * transform.forward;

    }
}
