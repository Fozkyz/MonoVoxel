using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework
{
	public static class QuaternionExtension
	{
		public static Vector3 AngleTo(Vector3 from, Vector3 location)
		{
			var angle = new Vector3();
			var v3 = Vector3.Normalize(location - from);
			angle.X = (float)Math.Asin(v3.Y);
			angle.Y = (float)Math.Atan2(-v3.Z, -v3.X);
			return angle;
		}

		public static void ToEuler(float x, float y, float z, float w, ref Vector3 result)
		{
			var rotation = new Quaternion(x, y, z, w);
			var forward = Vector3.Transform(Vector3.Forward, rotation);
			var up = Vector3.Transform(Vector3.Up, rotation);
			result = AngleTo(new Vector3(), forward);
			if (result.X == MathHelper.PiOver2)
			{
				result.Y = (float)Math.Atan2(up.Z, up.X);
				result.Z = 0;
			}
			else if (result.X == -MathHelper.PiOver2)
			{
				result.Y = (float)Math.Atan2(-up.Z, -up.X);
				result.Z = 0;
			}
			else
			{
				up = Vector3.Transform(up, Matrix.CreateRotationY(-result.Y));
				up = Vector3.Transform(up, Matrix.CreateRotationX(-result.X));
				result.Z = (float)Math.Atan2(up.Y, -up.X);
			}
		}

		public static void ToEuler(this Quaternion quaternion, ref Vector3 result)
		{
			ToEuler(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W, ref result);
		}

		public static Vector3 ToEuler(this Quaternion quaternion)
		{
			var result = Vector3.Zero;
			ToEuler(quaternion, ref result);
			return result;
		}
	}
}
