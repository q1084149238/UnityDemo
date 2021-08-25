
/// <summary>
/// 单例类
/// </summary>
public abstract class Singleton<T> where T : class, new ()  
{
	private static T _instance = null;
	/// <summary>
    /// 实例
    /// </summary>
	public static T Ins
	{
		get
		{
			if(_instance == null)
			{
				_instance = new T();
			}

			return _instance;
		}
	}

	protected Singleton ()
	{
		Init();
	}

	protected virtual void Init ()
	{
	}
}
