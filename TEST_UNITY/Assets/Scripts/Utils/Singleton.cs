/// <summary>
/// Singleton is existing for the whole runtime of the application. Accessible only from code.
/// </summary>

using System;

	public class Singleton<T>
	{

		/// <summary>
		/// The index of the singleton instance
		/// </summary>
		private static object _sInstance = Activator.CreateInstance(typeof(T));

		public static T Instance
		{
			get
			{

				if (null == _sInstance)
				{
					_sInstance = Activator.CreateInstance(typeof(T));
					(_sInstance as Singleton<T>).Init();
				}

				return (T)_sInstance;
			}
		}


		/// <summary>
		/// Default constructor
		/// </summary>
		protected Singleton()
		{
			if (_sInstance != null)
			{
				throw new Exception("instance already exist");
			}

			_sInstance = this;

			if (_sInstance == null)
			{
				throw new Exception("instance creation failed");
			}
		}


		public static void DestroyInstance()
		{
			_sInstance = null;
			GC.Collect();
		}

		public virtual void Init()
		{

		}
	}
