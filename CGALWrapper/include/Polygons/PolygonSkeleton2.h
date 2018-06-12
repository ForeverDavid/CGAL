#pragma once

#include "stdafx.h"
#include "Primatives/Point2.h"
#include "Descriptors/MeshDescriptor.h"
#include "Descriptors/EdgeIndex.h"
#include "Descriptors/EdgeConnection.h"

using namespace Primatives;
using namespace Descriptors;

namespace PolygonSkeleton2
{

	extern "C"
	{

		CGALWRAPPERAPI void CALLCON Skeleton2_LoadPoints2f(const Point2f* inPoints, int inSize);

		CGALWRAPPERAPI void CALLCON Skeleton2_AddHole2f(const Point2f* inPoints, int inSize);

		CGALWRAPPERAPI void CALLCON Skeleton2_Clear();

		CGALWRAPPERAPI void CALLCON Skeleton2_Release();

		CGALWRAPPERAPI CGALResult CALLCON Skeleton2_CreateInteriorSkeleton(BOOL includeBorder, MeshDescriptor& descriptor);

		CGALWRAPPERAPI CGALResult CALLCON Skeleton2_CreateExteriorSkeleton(double maxOffset, BOOL includeBorder, MeshDescriptor& descriptor);

		CGALWRAPPERAPI int CALLCON Skeleton2_CreateInteriorOffset(double offset);

		CGALWRAPPERAPI Point2f CALLCON Skeleton2_GetSkeletonPoint2f(int i);

		CGALWRAPPERAPI EdgeIndex CALLCON Skeleton2_GetSkeletonEdge(int i);

		CGALWRAPPERAPI int CALLCON Skeleton2_NumEdgeConnection();

		CGALWRAPPERAPI EdgeConnection CALLCON Skeleton2_GetEdgeConnection(int i);

		CGALWRAPPERAPI int CALLCON Skeleton2_NumPolygonPoints(int polygonIndex);

		CGALWRAPPERAPI Point2f CALLCON Skeleton2_GetPolygonPoint2f(int polygonIndex, int pointIndex);

	}

}