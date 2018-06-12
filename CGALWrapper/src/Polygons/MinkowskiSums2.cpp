
#include "stdafx.h"
#include "Polygons/MinkowskiSums2.h"
#include <CGAL/Exact_predicates_exact_constructions_kernel.h>
#include <CGAL/minkowski_sum_2.h>

#include <CGAL/Small_side_angle_bisector_decomposition_2.h>
#include <CGAL/Polygon_triangulation_decomposition_2.h>
#include <CGAL/Polygon_nop_decomposition_2.h>

using namespace std;

namespace MinkowskiSums2
{

	typedef CGAL::Exact_predicates_exact_constructions_kernel Kernel;
	typedef Kernel::Point_2 Point;
	typedef CGAL::Polygon_2<Kernel> Polygon;
	typedef CGAL::Polygon_with_holes_2<Kernel> PolygonWithHoles;

	Polygon A, B;
	PolygonWithHoles sum;
	vector<PolygonWithHoles> holes;

	void LoadPolygonPoints(Polygon& polygon, const vector<Point>& points)
	{
		polygon = Polygon(points.begin(), points.end());
	}

	void LoadPolygonPoints(PolygonWithHoles& polygon, const vector<Point>& points)
	{
		polygon = PolygonWithHoles(Polygon(points.begin(), points.end()));
	}

	CGALWRAPPERAPI void CALLCON MinkowskiSums2_A_LoadPoints2f(const Point2f* inPoints, int inSize)
	{
		vector<Point> points = ToPointArray2<Point, Point2f>(inPoints, inSize);
		LoadPolygonPoints(A, points);
	}

	CGALWRAPPERAPI void CALLCON MinkowskiSums2_B_LoadPoints2f(const Point2f* inPoints, int inSize)
	{
		vector<Point> points = ToPointArray2<Point, Point2f>(inPoints, inSize);
		LoadPolygonPoints(B, points);
	}

	CGALWRAPPERAPI void CALLCON MinkowskiSums2_Clear()
	{
		A.clear();
		B.clear();
		sum.clear();
		holes.clear();
	}

	CGALWRAPPERAPI void CALLCON MinkowskiSums2_Release()
	{
		A = Polygon();
		B = Polygon();
		sum = PolygonWithHoles();
		holes.resize(0);
	}

	CGALWRAPPERAPI CGALResult CALLCON MinkowskiSums2_ComputeSum()
	{
		try
		{
			//CGAL::Polygon_triangulation_decomposition_2<Kernel> pt_decomp;
			CGAL::Small_side_angle_bisector_decomposition_2<Kernel> ssab_decomp;

			sum = CGAL::minkowski_sum_2(A, B, ssab_decomp);
			holes = vector<PolygonWithHoles>(sum.holes_begin(), sum.holes_end());

			return CGAL_SUCCESS;
		}
		catch (...)
		{
			return CGAL_ERROR;
		}
	}

	CGALWRAPPERAPI int CALLCON MinkowskiSums2_NumPolygonPoints()
	{
		return int(sum.outer_boundary().size());
	}

	CGALWRAPPERAPI int CALLCON MinkowskiSums2_NumPolygonHoles()
	{
		return int(holes.size());
	}

	CGALWRAPPERAPI int CALLCON MinkowskiSums2_NumHolePoints(int holeIndex)
	{
		return int(holes[holeIndex].outer_boundary().size());
	}

	CGALWRAPPERAPI Point2f CALLCON MinkowskiSums2_GetPolygonPoint2f(int pointIndex)
	{
		Point p = sum.outer_boundary()[pointIndex];

		float x = float(CGAL::to_double(p.x()));
		float y = float(CGAL::to_double(p.y()));

		return{ x, y };
	}

	CGALWRAPPERAPI Point2f CALLCON MinkowskiSums2_GetHolePoint2f(int holeIndex, int pointIndex)
	{
		Point p = holes[holeIndex].outer_boundary()[pointIndex];

		float x = float(CGAL::to_double(p.x()));
		float y = float(CGAL::to_double(p.y()));

		return{ x, y };
	}

}