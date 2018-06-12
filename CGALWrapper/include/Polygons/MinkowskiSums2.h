#pragma once

#include "stdafx.h"
#include "Primatives/Point2.h"

using namespace Primatives;

namespace MinkowskiSums2
{

	extern "C"
	{

		CGALWRAPPERAPI void CALLCON MinkowskiSums2_A_LoadPoints2f(const Point2f* inPoints, int inSize);

		CGALWRAPPERAPI void CALLCON MinkowskiSums2_B_LoadPoints2f(const Point2f* inPoints, int inSize);

		CGALWRAPPERAPI void CALLCON MinkowskiSums2_Clear();

		CGALWRAPPERAPI void CALLCON MinkowskiSums2_Release();

		CGALWRAPPERAPI CGALResult CALLCON MinkowskiSums2_ComputeSum();

		CGALWRAPPERAPI int CALLCON MinkowskiSums2_NumPolygonPoints();

		CGALWRAPPERAPI int CALLCON MinkowskiSums2_NumPolygonHoles();

		CGALWRAPPERAPI int CALLCON MinkowskiSums2_NumHolePoints(int holeIndex);

		CGALWRAPPERAPI Point2f CALLCON MinkowskiSums2_GetPolygonPoint2f(int pointIndex);

		CGALWRAPPERAPI Point2f CALLCON MinkowskiSums2_GetHolePoint2f(int holeIndex, int pointIndex);

	}

}