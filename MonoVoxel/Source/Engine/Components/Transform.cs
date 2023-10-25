using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoVoxel
{
	public class Transform : Component
	{
		public Transform Parent
		{
			get { return _parent; }
			set 
			{ 
				_parent?.Children.Remove(this);
				
				_parent = value;

				_parent?.Children.Add(this);
			}
		}

		public List<Transform> Children
		{
			get { return _children; }
			private set { _children = value; }
		}

		public bool Dirty
		{
			get { return _dirty; }
			set
			{
				if (!_dirty)
				{
					_dirty = value;
				}
			}
		}

		public Vector3 Position
		{
			get
			{
				UpdateWorldMatrix();
				return _worldMatrix.Translation;
			}
			set
			{
				UpdateWorldMatrix();
				_localPosition = Vector3.Transform(value, _worldMatrix);
				_dirty = true;
			}
		}

		public Quaternion Rotation
		{
			get
			{
				UpdateWorldMatrix();
				_worldMatrix.Decompose(out _, out Quaternion r, out _);
				return r;
			}
			set
			{
				UpdateWorldMatrix();
				
			}
		}

		private Transform _parent;
		private List<Transform> _children;

		private Matrix _worldMatrix;
		private Vector3 _localPosition;
		private Vector3 _localRotation;
		private Vector3 _localScale;
		private bool _dirty;

		public Transform GetChild(int index)
		{
			if (index > _children.Count) 
				return null;

			return _children[index];
		}

		public Transform AddChild(Transform child)
		{
			_children.Add(child);
			
			return this;
		}

		private void UpdateWorldMatrix()
		{

		}
	}
}
