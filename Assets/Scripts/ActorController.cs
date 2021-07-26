using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;    // 引用模型对象
    public PlayerInput playerInput;    // 引用输入模块
    public float walkSpeed; // 移动速度用来设置与动画的播放速度匹配
    [SerializeField]  // 序列化下,能够序列化的只能是unity的组件内容
    private Animator anim;  // 引用动画控制器
    private Rigidbody rigid;    // 引用刚体
    // 移动位置
    private Vector3 MovingVec;
    // Start is called before the first frame update

    private void Awake()
    {
        // 加载动画控制器
        anim = model.GetComponent<Animator>();
        // 加载输入模块
        playerInput = this.GetComponent<PlayerInput>();
        // 加载刚体
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 设置动画控制器的forward变量，出入Dup信号
        //                    y
        //                    |
        //                    |
        //                    |
        //                    |
        //                    |                             
        //---------------------------------------x
        //                    |
        //                    |
        //          p(0.3,0.4)|
        //                    |
        //                    |
        //                    |
        anim.SetFloat("forward", playerInput.DMag);
        // 处理因为松开按键导致的模型朝向突变的问题
        if (playerInput.DMag > 0.1f)
        {
            // 转换模型的朝向 标量 * 单位向量, 两个向量的和
            model.transform.forward = playerInput.DVec;
        }

        MovingVec = playerInput.DMag * model.transform.forward * walkSpeed;
    }

    private void FixedUpdate()
    {
        // 进行物理计算
        //rigid.position = MovingVec * Time.fixedDeltaTime + rigid.position;

        // 也可以直接使用Velocity, 刷新y轴上的速度与坐标
        rigid.velocity = new Vector3(MovingVec.x, rigid.velocity.y, MovingVec.z);
    }

    private void LateUpdate()
    {
       
    }
}
