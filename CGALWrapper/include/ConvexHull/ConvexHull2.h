#pragma once

#include "stdafx.h"
#include "Primatives/Point2.h"

using namespace Primatives;

namespace ConvexHull2
{

	extern "C"
	{

		CGALWRAPPERAPI void CALLCON ConvexHull2_LoadPoints2f(const Point2f* inPoints, int inSize);

		CGALWRAPPERAPI void CALLCON ConvexHull2_Clear();

		CGALWRAPPERAPI void CALLCON ConvexHull2_Release();

		CGALWRAPPERAPI int CALLCON ConvexHull2_FindHull();

		CGALWRAPPERAPI BOOL CALLCON ConvexHull2_IsStronglyConvex(BOOL ccw);

		CGALWRAPPERAPI Point2f CALLCON ConvexHull2_GetHullPoint2f(int i);

	}


}
