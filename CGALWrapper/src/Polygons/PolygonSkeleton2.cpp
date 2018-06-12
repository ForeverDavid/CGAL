
#include "stdafx.h"
#include "Polygons/PolygonSkeleton2.h"

#include<boost/shared_ptr.hpp>
#include<CGAL/Exact_predicates_inexact_constructions_kernel.h>
#include<CGAL/Polygon_2.h>
#include <CGAL/Polygon_with_holes_2.h>
#include<CGAL/create_straight_skeleton_from_polygon_with_holes_2.h>
#include<CGAL/create_offset_polygons_2.h>
#include<CGAL/create_offset_polygons_from_polygon_with_holes_2.h>

#include <iostream>

using namespace std;

namespace PolygonSkeleton2
{

	typedef CGAL::Exact_predicates_inexact_constructions_kernel K;
	typedef K::Point_2 Point;
	typedef CGAL::Polygon_2<K> Polygon;
	typedef CGAL::Polygon_with_holes_2<K> PolygonWithHoles;
	typedef CGAL::Straight_skeleton_2<K> Ss;
	typedef boost::shared_ptr<Ss> SsPtr;
	typedef boost::shared_ptr<Polygon> PolygonPtr;
	typedef std::vector<PolygonPtr> PolygonPtrVector;

	typedef Ss::Vertex_handle Vertex;
	typedef Ss::Halfedge_handle HalfEdge;

	PolygonWithHoles polygon;

	vector<Point> points;
	vector<EdgeIndex> edges;
	vector<EdgeConnection> edgeConnections;
	vector<PolygonPtr> polygons;

	CGALWRAPPERAPI void CALLCON Skeleton2_LoadPoints2f(const Point2f* inPoints, int inSize)
	{
		vector<Point> points = ToPointArray2<Point, Point2f>(inPoints, inSize);
		polygon = PolygonWithHoles(Polygon(points.begin(), points.end()));
	}

	CGALWRAPPERAPI void CALLCON Skeleton2_AddHole2f(const Point2f* inPoints, int inSize)
	{
		vector<Point> points = ToPointArray2<Point, Point2f>(inPoints, inSize);
		polygon.add_hole(Polygon(points.begin(), points.end()));
	}

	CGALWRAPPERAPI void CALLCON Skeleton2_Clear()
	{
		polygon.clear();
		points.clear();
		edges.clear();
		edgeConnections.clear();
		polygons.clear();
	}

	CGALWRAPPERAPI void CALLCON Skeleton2_Release()
	{
		polygon = PolygonWithHoles();
		points.resize(0);
		edges.resize(0);
		edgeConnections.resize(0);
		polygons.resize(0);
	}

	MeshDescriptor ConvertFromHalfEdge(SsPtr iss, BOOL includeBorder)
	{
		points.clear();
		edges.clear();
		edgeConnections.clear();

		map<int, int> edgeIndex;
		map<int, int> vertIndex;

		int index = 0;
		for (Vertex v = iss->vertices_begin(); v != iss->vertices_end(); ++v)
		{
			vertIndex.insert(pair<int, int>(v->id(), index++));
			points.push_back(v->point());
		}

		index = 0;
		for (HalfEdge edge = iss->halfedges_begin(); edge != iss->halfedges_end(); ++edge)
		{
			if (edge->is_border()) continue;
			if (edgeIndex.find(edge->id()) != edgeIndex.end()) continue;

			HalfEdge opp = edge->opposite();
			Vertex v0 = edge->vertex();
			Vertex v1 = opp->vertex();

			if (!includeBorder && v0->is_contour() && v1->is_contour())
			{
				edgeIndex.insert(pair<int, int>(edge->id(), -1));
				edgeIndex.insert(pair<int, int>(opp->id(), -1));
			}
			else
			{
				int i0 = vertIndex[v0->id()];
				int i1 = vertIndex[v1->id()];

				edges.push_back({ i0, i1 });
				edgeIndex.insert(pair<int, int>(edge->id(), index++));
				edgeIndex.insert(pair<int, int>(opp->id(), index++));
	
			}
		}

		for (HalfEdge edge = iss->halfedges_begin(); edge != iss->halfedges_end(); ++edge)
		{
			int id = edgeIndex[edge->id()];

			if (id != -1)
			{
				EdgeConnection con = { -1, -1, -1, -1 };

				con.edge = id;
				con.previous = edgeIndex[edge->prev()->id()];
				con.next = edgeIndex[edge->next()->id()];
				con.opposite = edgeIndex[edge->opposite()->id()];

				edgeConnections.push_back(con);
			}

		}

		MeshDescriptor descriptor;
		descriptor.vertices = int(points.size());
		descriptor.edges = int(edges.size());
		descriptor.faces = 0;

		return descriptor;
	}

	CGALWRAPPERAPI CGALResult CALLCON Skeleton2_CreateInteriorSkeleton(BOOL includeBorder, MeshDescriptor& descriptor)
	{
		try
		{
			SsPtr iss = CGAL::create_interior_straight_skeleton_2(polygon);
			descriptor = ConvertFromHalfEdge(iss, includeBorder);
			return CGAL_SUCCESS;
		}
		catch (...)
		{
			return CGAL_ERROR;
		}
	}

	CGALWRAPPERAPI CGALResult CALLCON Skeleton2_CreateExteriorSkeleton(double maxOffset, BOOL includeBorder, MeshDescriptor& descriptor)
	{
		try
		{
			SsPtr iss = CGAL::create_exterior_straight_skeleton_2(maxOffset, polygon.outer_boundary());
			descriptor = ConvertFromHalfEdge(iss, includeBorder);
			return CGAL_SUCCESS;
		}
		catch (...)
		{
			return CGAL_ERROR;
		}
	}

	CGALWRAPPERAPI int CALLCON Skeleton2_CreateInteriorOffset(double offset)
	{
		try
		{
			polygons = CGAL::create_interior_skeleton_and_offset_polygons_2(offset, polygon);
			return polygons.size();
		}
		catch (...)
		{
			return 0;
		}
	}

	CGALWRAPPERAPI Point2f CALLCON Skeleton2_GetSkeletonPoint2f(int i)
	{
		Point p = points[i];
		return{ float(p[0]) , float(p[1]) };
	}

	CGALWRAPPERAPI EdgeIndex CALLCON Skeleton2_GetSkeletonEdge(int i)
	{
		return edges[i];
	}

	CGALWRAPPERAPI int CALLCON Skeleton2_NumEdgeConnection()
	{
		return int(edgeConnections.size());
	}

	CGALWRAPPERAPI EdgeConnection CALLCON Skeleton2_GetEdgeConnection(int i)
	{
		return edgeConnections[i];
	}

	CGALWRAPPERAPI int CALLCON Skeleton2_NumPolygonPoints(int polygonIndex)
	{
		return int(polygons[polygonIndex]->size());
	}

	CGALWRAPPERAPI Point2f CALLCON Skeleton2_GetPolygonPoint2f(int polygonIndex, int pointIndex)
	{
		Point p = polygons[polygonIndex]->vertex(pointIndex);
		return{ float(p[0]) , float(p[1]) };
	}

}