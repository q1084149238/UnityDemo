using UnityEngine;

/// <summary>
/// 继承了MonoBehaviour的基础单例类
/// 带有一个单例,并且在加载的时候不会被销毁
/// </summary>
public abstract class MBSingleton<T> : MonoBehaviour where T : MBSingleton<T>
{
	/// <summary>
	/// 是否销毁
	/// </summary>
	private static bool isDestroy = false;

	private static T _instance = null;
	/// <summary>
    /// 实例
    /// </summary>
	public static T Ins
	{
		get
		{
			//标记销毁的的不在创建
			if (_instance == null && !isDestroy) 
			{
				GameObject go = GameObject.Find ("AllManage");
				if (go == null) 
				{
					go = new GameObject ("AllManage");
					
					DontDestroyOnLoad (go);
				}

				_instance = go.AddComponent<T>();
			}

			return _instance;
		}    
	}

	protected virtual void OnDestroy ()
	{
		_instance = null;
		isDestroy = true;
	}
}