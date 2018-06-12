
#include "stdafx.h"
#include "Polygons/PolygonSimplify2.h"

#include <CGAL/Exact_predicates_inexact_constructions_kernel.h>
#include <CGAL/Polygon_2.h>
#include <CGAL/Polyline_simplification_2/simplify.h>

using namespace std;

namespace PolygonSimplify2
{

	namespace PS = CGAL::Polyline_simplification_2;
	typedef CGAL::Exact_predicates_inexact_constructions_kernel K;
	typedef K::Point_2 Point;
	typedef CGAL::Polygon_2<K> Polygon;
	typedef PS::Stop_below_count_ratio_threshold Stop;

	Polygon polygon, simplified;

	CGALWRAPPERAPI void CALLCON Simplify2_LoadPoints2f(const Point2f* inPoints, int inSize)
	{
		vector<Point> points = ToPointArray2<Point, Point2f>(inPoints, inSize);
		polygon = Polygon(points.begin(), points.end());
	}

	CGALWRAPPERAPI void CALLCON Simplify2_Clear()
	{
		polygon.clear();
		simplified.clear();
	}

	CGALWRAPPERAPI void CALLCON Simplify2_Release()
	{
		polygon = Polygon();
		simplified = Polygon();
	}

	CGALWRAPPERAPI int CALLCON Simplify2_SquareDistCostSimplify(float threshold)
	{
		try
		{
			simplified = PS::simplify(polygon, PS::Squared_distance_cost(), Stop(threshold));
			return int(simplified.size());
		}
		catch (...)
		{
			return 0;
		}
	}

	CGALWRAPPERAPI int CALLCON Simplify2_ScaledSquareDistCostSimplify(float threshold)
	{
		try
		{
			simplified = PS::simplify(polygon, PS::Scaled_squared_distance_cost(), Stop(threshold));
			return int(simplified.size());
		}
		catch (...)
		{
			return 0;
		}
	}

	CGALWRAPPERAPI Point2f CALLCON Simplify2_GetSimplifiedPoint2f(int i)
	{
		Point p = simplified[i];
		return { float(p[0]) , float(p[1]) };
	}


}