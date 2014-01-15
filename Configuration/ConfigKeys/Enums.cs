using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.ConfigKeys
{
	public enum Output
	{
		Surface,
		Overlay,
		OpenGL,
		OpenGLNB,
		DDraw,
	}

	public enum PriorityLevel
	{
		Pause = -3,
		Lowest = -2,
		Lower = -1,
		Normal = 0,
		Higher = 1,
		Highest = 2,
	}

	public enum MachineType
	{
		SVGA_S3,
		Hercules,
		CGA,
		Tandy,
		PCJR,
		EGA,
		VGAOnly,
		SVGA_ET3000,
		SVGA_ET4000,
		SVGA_Paradise,
		VESA_NOLFB,
		VESA_OLDVBE,
	}
}
