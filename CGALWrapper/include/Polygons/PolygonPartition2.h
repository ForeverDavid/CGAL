#pragma once

#include "stdafx.h"
#include "Primatives/Point2.h"

using namespace Primatives;

namespace PolygonPartition2
{

	extern "C"
	{

		CGALWRAPPERAPI void CALLCON Partition2_LoadPoints2f(const Point2f* inPoints, int inSize);

		CGALWRAPPERAPI void CALLCON Partition2_Clear();

		CGALWRAPPERAPI void CALLCON Partition2_Release();

		CGALWRAPPERAPI int CALLCON Partition2_ApproxConvexPartition();

		CGALWRAPPERAPI int CALLCON Partition2_GreeneApproxConvexPartition();

		CGALWRAPPERAPI int CALLCON Partition2_YMonotonePartition();

		CGALWRAPPERAPI int CALLCON Partition2_OptimalConvexPartition();

		CGALWRAPPERAPI int CALLCON Partition2_GetPolygonSize(int i);

		CGALWRAPPERAPI Point2f CALLCON Partition2_GetPolygonPoint2f(int i, int j);

	}

}
