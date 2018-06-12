
#include "stdafx.h"
#include "MeshGeneration/ConformingTriangulation2.h"
#include "DelaunayFaces\Delaunay_face_with_id_2 .h"

#include <CGAL/Exact_predicates_inexact_constructions_kernel.h>
#include <CGAL/Constrained_Delaunay_triangulation_2.h>
#include <CGAL/Triangulation_face_base_with_info_2.h>
#include <CGAL/Triangulation_vertex_base_with_info_2.h>

#include <CGAL/Delaunay_mesher_2.h>
#include <CGAL/Delaunay_mesh_vertex_base_2.h>
#include <CGAL/Delaunay_mesh_face_base_2.h>
#include <CGAL/Delaunay_mesh_size_criteria_2.h>
#include <CGAL/lloyd_optimize_mesh_2.h>

using namespace std;
using namespace DelaunayFaces;

namespace ConformingTriangulation2
{

	struct VertexInfo
	{
		VertexInfo() {}
		int id;
	};

	typedef CGAL::Exact_predicates_inexact_constructions_kernel K;

	typedef CGAL::Triangulation_vertex_base_with_info_2<VertexInfo, K> Vbb;
	typedef CGAL::Delaunay_mesh_vertex_base_2<K, Vbb> Vb;

	typedef Delaunay_face_with_id_2<K> Fb;

	typedef CGAL::Triangulation_data_structure_2<Vb, Fb> Tds;
	typedef CGAL::Constrained_Delaunay_triangulation_2<K, Tds> CDT;
	typedef CGAL::Delaunay_mesh_size_criteria_2<CDT> Criteria;
	typedef CGAL::Delaunay_mesher_2<CDT, Criteria> Mesher;
	typedef CDT::Point Point;

	CDT cdt;

	vector<Point> points;
	vector<TriangleIndex> triangles;
	vector<TriangleIndex> neighbors;
	list<Point> seeds;

	CGALWRAPPERAPI CGALResult CALLCON Conforming2_InsertPoints2f(const Point2f* inPoints, int inSize, BOOL close)
	{
		try
		{
			vector<Point> points = ToPointArray2<Point, Point2f>(inPoints, inSize);
			cdt.insert_constraint(points.begin(), points.end(), close);
			return CGAL_SUCCESS;
		}
		catch (...)
		{
			return CGAL_ERROR;
		}
		
	}

	CGALWRAPPERAPI void CALLCON Conforming2_InsertSeed2f(Point2f point)
	{
		seeds.push_back(Point(point.x, point.y));
	}

	CGALWRAPPERAPI void CALLCON Conforming2_Clear()
	{
		cdt.clear();
		seeds.clear();
		points.clear();
		triangles.clear();
		neighbors.clear();
	}

	CGALWRAPPERAPI void CALLCON Conforming2_Release()
	{
		cdt = CDT();
		seeds.resize(0);
		points.resize(0);
		triangles.resize(0);
		neighbors.resize(0);
	}

	CGALWRAPPERAPI CGALResult CALLCON Conforming2_RefineMesh(int iterations, float angleBounds, float lengthBounds)
	{
		try
		{
			CGAL::refine_Delaunay_mesh_2(cdt, seeds.begin(), seeds.end(), Criteria(angleBounds, lengthBounds));

			if (iterations > 0)
				CGAL::lloyd_optimize_mesh_2(cdt, iterations);

			return CGAL_SUCCESS;
		}
		catch (...)
		{
			return CGAL_ERROR;
		}
	}

	CGALWRAPPERAPI CGALResult CALLCON Conforming2_Triangulate(MeshDescriptor& descriptor)
	{

		try
		{
			points.clear();
			triangles.clear();
			neighbors.clear();

			for (auto vert = cdt.finite_vertices_begin(); vert != cdt.finite_vertices_end(); ++vert)
			{
				vert->info().id = int(points.size());
				points.push_back(vert->point());
			}

			int faceCount = 0;
			for (auto face = cdt.finite_faces_begin(); face != cdt.finite_faces_end(); ++face)
			{
				if (!face->is_in_domain()) continue;

				face->id = faceCount;
				faceCount++;
			}

			for (auto face = cdt.finite_faces_begin(); face != cdt.finite_faces_end(); ++face)
			{
				if (!face->is_in_domain()) continue;

				int i0 = face->vertex(0)->info().id;
				int i1 = face->vertex(1)->info().id;
				int i2 = face->vertex(2)->info().id;

				triangles.push_back({ i0, i1, i2 });

				int n0 = face->neighbor(0)->id;
				int n1 = face->neighbor(1)->id;
				int n2 = face->neighbor(2)->id;

				neighbors.push_back({ n0, n1, n2 });
			}

			descriptor.vertices = int(points.size());
			descriptor.edges = 0;
			descriptor.faces = faceCount;

			return CGAL_SUCCESS;
		}
		catch (...)
		{
			return CGAL_ERROR;
		}

	}

	CGALWRAPPERAPI Point2f CALLCON Conforming2_GetPoint2f(int i)
	{
		Point p = points[i];
		return{ float(p[0]) , float(p[1]) };
	}

	CGALWRAPPERAPI TriangleIndex CALLCON Conforming2_GetTriangle(int i)
	{
		return triangles[i];
	}

	CGALWRAPPERAPI TriangleIndex CALLCON Conforming2_GetNeighbor(int i)
	{
		return neighbors[i];
	}

}