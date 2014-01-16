using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.ConfigKeys
{
	public struct Cycles : IEquatable<Cycles>, IComparable<Cycles>
	{
		#region Consts

		private const string AUTO_STRING = "Auto";
		private const string MAX_STRING = "Max";
		private const string FIXED_STRING = "Fixed ";
		private const string FIXED_STRING_FORMAT = FIXED_STRING + "#{0}";
		private const char CYCLES_SEPARATOR = '#';
		private const int FIXED_PARTS_COUNT = 2;
		#endregion

		#region Fields

		private bool isMax;

		public static readonly Cycles Auto = new Cycles();

		public static readonly Cycles Max = new Cycles
		{
			Cycles = null,
			isMax = true,
		};
		#endregion
		#region Properties

		public int? Cycles { get; private set; }

		public bool IsAuto
		{
			get { return !this.IsMax && !this.IsFixed; }
		}

		public bool IsMax
		{
			get { return this.isMax && !this.IsFixed; }
		}

		public bool IsFixed
		{
			get { return this.Cycles.HasValue; }
		}
		#endregion

		#region Ctor

		public Cycles(int cycles)
			: this()
		{
			this.isMax = false;
			this.Cycles = cycles;
		}
		#endregion

		#region Methods

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
			return Equals((Cycles)obj);
		}

		// override object.GetHashCode
		public override int GetHashCode()
		{
			// TODO: write your implementation of GetHashCode() here
			return ((this.isMax.GetHashCode() << 24) | 
				(this.Cycles.HasValue.GetHashCode() << 16)) ^ 
				(this.Cycles ?? 0);
		}

		public bool Equals(Cycles other)
		{
			return this.isMax.Equals(other.isMax) &&
				this.Cycles.Equals(other.Cycles);
		}

		public int CompareTo(Cycles other)
		{
			if ((this.isMax && !other.isMax) || (other.IsAuto))
			{
				return 1;
			}
			else if ((!this.isMax && other.isMax) || (this.IsAuto))
			{
				return -1;
			}
			else
			{
				return this.Cycles.Value.CompareTo(other.Cycles.Value);
			}

		}

		public override string ToString()
		{
			if (this.IsAuto)
			{
				return AUTO_STRING;
			}
			else if (this.IsMax)
			{
				return MAX_STRING;
			}
			else
			{
				return string.Format(FIXED_STRING_FORMAT, this.Cycles);
			}
		}

		public static Cycles Parse(string s)
		{
			Cycles result;

			if (!TryParse(s, out result))
			{
				throw new FormatException("Invalid cycles format");
			}

			return result;
		}

		public static bool TryParse(string s, out Cycles result)
		{
			result = new Cycles();
			bool didSucceeded = false;

			if (AUTO_STRING.Equals(s, StringComparison.InvariantCultureIgnoreCase))
			{
				didSucceeded = true;
				result = ConfigKeys.Cycles.Auto;
			}
			else if (MAX_STRING.Equals(s, StringComparison.InvariantCultureIgnoreCase))
			{
				didSucceeded = true;
				result = ConfigKeys.Cycles.Max;
			}
			else
			{
				string[] splitted = s.Split(CYCLES_SEPARATOR);
				int cycles;

				if ((splitted.Length == FIXED_PARTS_COUNT) &&
					FIXED_STRING.Equals(splitted[0], StringComparison.InvariantCultureIgnoreCase) &&
					int.TryParse(splitted[1], out cycles)) 
				{
					didSucceeded = true;
					result = new Cycles(cycles);
				}
			}

			return didSucceeded;
		}
		#endregion

		#region Operators

		public static bool operator ==(Cycles left, Cycles right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Cycles left, Cycles right)
		{
			return !(left == right);
		}

		public static bool operator <(Cycles left, Cycles right)
		{
			return left.IsFixed && right.IsFixed &&
				(left.CompareTo(right) < 0);
		}

		public static bool operator >(Cycles left, Cycles right)
		{
			return left.IsFixed && right.IsFixed &&
				(left.CompareTo(right) > 0);
		}

		public static bool operator <=(Cycles left, Cycles right)
		{
			return left.IsFixed && right.IsFixed &&
				!(left > right);
		}

		public static bool operator >=(Cycles left, Cycles right)
		{
			return left.IsFixed && right.IsFixed &&
				!(left < right);
		}
		#endregion
	}
}
