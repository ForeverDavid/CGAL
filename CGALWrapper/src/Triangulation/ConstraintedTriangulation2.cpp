
#include "stdafx.h"
#include "Triangulation/ConstraintedTriangulation2.h"

#include <CGAL/Exact_predicates_inexact_constructions_kernel.h>
#include <CGAL/Constrained_Delaunay_triangulation_2.h>
#include <CGAL/Triangulation_face_base_with_info_2.h>
#include <CGAL/Triangulation_vertex_base_with_info_2.h>

using namespace std;

namespace ConstraintedTriangulation2
{

	struct FaceInfo2
	{
		FaceInfo2() {}
	};

	struct VertexInfo2
	{
		VertexInfo2() {}
		int id;
	};

	typedef CGAL::Exact_predicates_inexact_constructions_kernel K;

	typedef CGAL::Triangulation_vertex_base_with_info_2<VertexInfo2, K> Vbb;
	typedef CGAL::Triangulation_vertex_base_2<K, Vbb> Vb;
	
	typedef CGAL::Triangulation_face_base_with_info_2<FaceInfo2, K> Fbb;
	typedef CGAL::Constrained_triangulation_face_base_2<K, Fbb> Fb;

	typedef CGAL::Triangulation_data_structure_2<Vb, Fb> TDS;
	typedef CGAL::Exact_predicates_tag Itag;
	typedef CGAL::Constrained_Delaunay_triangulation_2<K, TDS, Itag>  CDT;
	typedef CDT::Point Point;

	CDT cdt;

	vector<Point> points;
	vector<TriangleIndex> triangles;

	CGALWRAPPERAPI CGALResult CALLCON Constrainted2_InsertPoints2f(const Point2f* inPoints, int inSize, BOOL close)
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

	CGALWRAPPERAPI CGALResult CALLCON Constrainted2_Triangulate(MeshDescriptor& descriptor)
	{

		try
		{
			points.clear();
			triangles.clear();

			for (auto vert = cdt.finite_vertices_begin(); vert != cdt.finite_vertices_end(); ++vert)
			{
				vert->info().id = int(points.size());
				points.push_back(vert->point());
			}

			int faceCount = 0;
			for (auto face = cdt.finite_faces_begin(); face != cdt.finite_faces_end(); ++face)
			{
				faceCount++;

				int i0 = face->vertex(0)->info().id;
				int i1 = face->vertex(1)->info().id;
				int i2 = face->vertex(2)->info().id;

				triangles.push_back({ i0, i1, i2 });
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

	CGALWRAPPERAPI void CALLCON Constrainted2_Clear()
	{
		cdt.clear();
		points.clear();
		triangles.clear();
	}

	CGALWRAPPERAPI void CALLCON Constrainted2_Release()
	{
		cdt = CDT();
		points.resize(0);
		triangles.resize(0);
	}

	CGALWRAPPERAPI Point2f CALLCON Constrainted2_GetPoint2f(int i)
	{
		Point p = points[i];
		return{ float(p[0]) , float(p[1]) };
	}

	CGALWRAPPERAPI TriangleIndex CALLCON Constrainted2_GetTriangle(int i)
	{
		return triangles[i];
	}

}