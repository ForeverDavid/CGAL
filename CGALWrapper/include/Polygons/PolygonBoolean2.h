#pragma once

#include "stdafx.h"
#include "Primatives/Point2.h"

using namespace Primatives;

namespace PolygonBoolean2
{

	extern "C"
	{

		CGALWRAPPERAPI void CALLCON Boolean2_A_LoadPoints2f(const Point2f* inPoints, int inSize);

		CGALWRAPPERAPI void CALLCON Boolean2_A_AddHole2f(const Point2f* inPoints, int inSize);

		CGALWRAPPERAPI void CALLCON Boolean2_B_LoadPoints2f(const Point2f* inPoints, int inSize);

		CGALWRAPPERAPI void CALLCON Boolean2_B_AddHole2f(const Point2f* inPoints, int inSize);

		CGALWRAPPERAPI void CALLCON Boolean2_Clear();

		CGALWRAPPERAPI void CALLCON Boolean2_Release();

		CGALWRAPPERAPI BOOL CALLCON Boolean2_DoIntersect();

		CGALWRAPPERAPI int CALLCON Boolean2_Union();

		CGALWRAPPERAPI int CALLCON Boolean2_Intersection();

		CGALWRAPPERAPI int CALLCON Boolean2_Difference();

		CGALWRAPPERAPI int CALLCON Boolean2_SymmetricDifference();

		CGALWRAPPERAPI void CALLCON Boolean2_PointToPolygon(int polyIndex);

		CGALWRAPPERAPI int CALLCON Boolean2_NumPolygonPoints();

		CGALWRAPPERAPI int CALLCON Boolean2_NumPolygonHoles();

		CGALWRAPPERAPI int CALLCON Boolean2_NumHolePoints(int holeIndex);

		CGALWRAPPERAPI Point2f CALLCON Boolean2_GetPolygonPoint2f(int pointIndex);

		CGALWRAPPERAPI Point2f CALLCON Boolean2_GetHolePoint2f(int holeIndex, int pointIndex);

	}

}
