using UnityEngine;

/// <summary>
/// 继承了MonoBehaviour的附加单例类
/// 带有一个单例,会随着GameObject销毁
/// </summary>
public abstract class AttachSingleton<T> : MonoBehaviour where T : AttachSingleton<T>
{
	private static T _instance = null;
    /// <summary>
    /// 实例
    /// </summary>
	public static T Ins
	{
		get { return _instance; }    
	}

    protected virtual void Awake()
    {
        _instance = (T)this;
    }

	protected virtual void OnDestroy ()
	{
		_instance = null;
	}
}