using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
public class PlayerInput : MonoBehaviour
{
    [Header("----- keys -----")]
    // variable
    public string keyUp;   
    public string keyDown;
    public string keyLeft;
    public string keyRight;

    public string KeyA; // as key run
    public string KeyB; // as key jump
    public string KeyC;
    public string KeyD;

    [Header("----- component enabled -----")]
    // flag
    // 是否开启此模块
    public bool PlayerInputEnabled = true;
    [Header("----- signals -----")]
    // signal
    public float DUp;   // DirectionUp
    public float DRight;    // DirrectionRight
    public float DMag; // DirectionMagnetic
    public Vector3 DVec; // DirectionVector
    // pressing signal
    public bool isRun;  // 是否跑动
    // trigger once signal
    // 两个跳跃标志
    public bool isJump; // 是否触发跳跃了
    public bool lastJump;   // 上次跳跃是否结束了
    // double trigger signal


    [Header("----- others -----")]
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

        Vector2 tmpDAxis = this.SquareToCircle(new Vector2(DRight, DUp));

        DMag = Mathf.Sqrt(tmpDAxis.y * tmpDAxis.y + tmpDAxis.x * tmpDAxis.x);
            //> 1.0f ? 1.0f : Mathf.Sqrt(DUp * DUp + DRight * DRight);
        DVec = tmpDAxis.x * transform.right + tmpDAxis.y * transform.forward;

        // 是否跑动
        isRun = Input.GetKey(KeyA);

        // 是否跳跃
        bool tmpJump = Input.GetKey(KeyB);
        // 保证tmpJump与lastJump保持互斥状态
        isJump = tmpJump != lastJump && tmpJump == true ? true : false;
        //if (tmpJump != lastJump && tmpJump == true)
        //{
        //    isJump = true;
        //}
        //else
        //{
        //    isJump = false;
        //}
        lastJump = tmpJump;
    }

    // 处理斜向移动时超过1.0的问题，使用平面投射到圆
    private Vector2 SquareToCircle(Vector2 input) 
    {
        Vector2 outPut = Vector2.zero;
        outPut.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        outPut.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return outPut;
    }
}
