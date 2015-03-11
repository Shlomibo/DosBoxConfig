using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extansions.Object;

namespace Configuration.ConfigKeys
{
	public struct Priority : IEquatable<Priority>, IComparable<Priority>
	{
		#region Consts

		private const char PRIORITY_SEPARETOR = ',';
		private const int MAX_PRIORITY_PARTS_COUNT = 2;
		#endregion

		#region Fields

		public static readonly Priority Default = new Priority();
		private static readonly string[] priorityLevels = Enum.GetNames(typeof(PriorityLevel));
		#endregion

		#region Properties

		public PriorityLevel Focused { get; }
		public PriorityLevel Background { get; }
		#endregion

		#region Ctor

		public Priority(PriorityLevel focused, PriorityLevel background)
			: this()
		{
			this.Focused = focused;
			this.Background = background;
		}
		#endregion

		#region Methods

		public bool Equals(Priority other) =>
			(this.Focused == other.Focused) &&
			(this.Background == other.Background);

		// override object.Equals
		public override bool Equals(object obj)
		{
			//       
			// See the full list of guidelines at
			//   http://go.microsoft.com/fwlink/?LinkID=85237  
			// and also the guidance for operator== at
			//   http://go.microsoft.com/fwlink/?LinkId=85238
			//

			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			// TODO: write your implementation of Equals() here
			return Equals((Priority)obj);
		}

		// override object.GetHashCode
		public override int GetHashCode() =>
			ObjectExtansions.CreateHashCode(this.Focused, this.Background);

		public int CompareTo(Priority other)
		{
			int result = this.Focused.CompareTo(other.Focused);

			if (result == 0)
			{
				result = this.Background.CompareTo(other.Background);
			}

			return result;
		}

		public override string ToString() =>
			$"{this.Focused},{this.Background}";

		public static Priority Parse(string s)
		{
			if (s == null)
			{
				throw new NullReferenceException(nameof(s));
			}

			Priority result;

			if (!TryParse(s, out result))
			{
				throw new FormatException("Invalid priority format");
			}

			return result;
		}

		public static bool TryParse(string s, out Priority result)
		{
			result = Priority.Default;
			string[] splitted = s.Split(PRIORITY_SEPARETOR);
			PriorityLevel focused = 0;
			PriorityLevel backgroud = 0;
			bool didSucceed = false;

			if ((splitted.Length <= MAX_PRIORITY_PARTS_COUNT) &&
				priorityLevels.Any(level => 
					level.Equals(splitted[0], StringComparison.InvariantCultureIgnoreCase)))
			{
				focused = (PriorityLevel)Enum.Parse(typeof(PriorityLevel), splitted[0], true);
				didSucceed = true;

				if (splitted.Length == 1)
				{
					backgroud = focused;
				}
				else if (priorityLevels.Any(level => 
					level.Equals(splitted[1], StringComparison.InvariantCultureIgnoreCase)))
				{
					backgroud = (PriorityLevel)Enum.Parse(typeof(PriorityLevel), splitted[1], true);
				}
				else
				{
					didSucceed = false;
				}
			}

			if (didSucceed)
			{
				result = new Priority(focused, backgroud);
			}

			return didSucceed;
		}
		#endregion

		#region Operators

		public static bool operator ==(Priority left, Priority right) =>
			left.Equals(right);

		public static bool operator !=(Priority left, Priority right) =>
			!(left == right);

		public static bool operator <(Priority left, Priority right) =>
			left.CompareTo(right) < 0;

		public static bool operator >(Priority left, Priority right) =>
			left.CompareTo(right) > 0;

		public static bool operator <=(Priority left, Priority right) =>
			!(left > right);

		public static bool operator >=(Priority left, Priority right) =>
			!(left < right);
		#endregion
	}
}
