using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoVoxel
{
	public class Component
	{
		#region Fields

		public Guid ID { get; private set; }
		public GameObject GameObject { get; set; }

		public bool Enabled
		{
			get { return _enabled; }
			set
			{
				if (_enabled != value)
				{
					_enabled = value;
					if (_enabled)
					{
						OnEnable();
					}
					else
					{
						OnDisable();
					}
				}
			}
		}

		#endregion

		private bool _enabled;

		public Component()
		{
			ID = Guid.NewGuid();
			Enabled = true;
		}

		public virtual void Awake()
		{

		}

		public virtual void Start()
		{

		}

		public virtual void OnEnable()
		{

		}

		public virtual void OnDisable()
		{

		}
	}
}
