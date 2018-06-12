
#include "stdafx.h"
#include "Polygons/PolygonPartition2.h"

#include <CGAL/Exact_predicates_inexact_constructions_kernel.h>
#include <CGAL/Polygon_2.h>
#include <CGAL/partition_2.h>
#include <CGAL/enum.h>

using namespace std;

namespace PolygonPartition2
{

	typedef CGAL::Exact_predicates_inexact_constructions_kernel K;
	typedef CGAL::Partition_traits_2<K> Traits;
	typedef Traits::Point_2 Point;
	typedef Traits::Polygon_2 Polygon;

	Polygon polygon;
	vector<Polygon> partition;

	CGALWRAPPERAPI void CALLCON Partition2_LoadPoints2f(const Point2f* inPoints, int inSize)
	{
		vector<Point> points = ToPointArray2<Point, Point2f>(inPoints, inSize);
		polygon = Polygon(points.begin(), points.end());
	}

	CGALWRAPPERAPI void CALLCON Partition2_Clear()
	{
		polygon.clear();
		partition.clear();
	}

	CGALWRAPPERAPI void CALLCON Partition2_Release()
	{
		polygon = Polygon();
		partition.resize(0);
	}

	CGALWRAPPERAPI int CALLCON Partition2_ApproxConvexPartition()
	{
		try
		{
			list<Polygon> tmp;
			CGAL::approx_convex_partition_2(polygon.vertices_begin(), polygon.vertices_end(), back_inserter(tmp));
			partition = { begin(tmp), end(tmp) };

			return int(partition.size());
		}
		catch (...)
		{
			return 0;
		}
	}

	CGALWRAPPERAPI int CALLCON Partition2_GreeneApproxConvexPartition()
	{
		try
		{
			list<Polygon> tmp;
			CGAL::greene_approx_convex_partition_2(polygon.vertices_begin(), polygon.vertices_end(), back_inserter(tmp));
			partition = { begin(tmp), end(tmp) };

			return int(partition.size());
		}
		catch (...)
		{
			return 0;
		}
	}

	CGALWRAPPERAPI int CALLCON Partition2_YMonotonePartition()
	{
		try
		{
			list<Polygon> tmp;
			CGAL::y_monotone_partition_2(polygon.vertices_begin(), polygon.vertices_end(), back_inserter(tmp));
			partition = { begin(tmp), end(tmp) };

			return int(partition.size());
		}
		catch (...)
		{
			return 0;
		}
	}

	CGALWRAPPERAPI int CALLCON Partition2_OptimalConvexPartition()
	{
		try
		{
			list<Polygon> tmp;
			CGAL::optimal_convex_partition_2(polygon.vertices_begin(), polygon.vertices_end(), back_inserter(tmp));
			partition = { begin(tmp), end(tmp) };

			return int(partition.size());
		}
		catch (...)
		{
			return 0;
		}
	}

	CGALWRAPPERAPI int CALLCON Partition2_GetPolygonSize(int i)
	{
		return int(partition[i].size());
	}

	CGALWRAPPERAPI Point2f CALLCON Partition2_GetPolygonPoint2f(int i, int j)
	{
		Point p = partition[i][j];
		return { float(p[0]) , float(p[1]) };
	}

}