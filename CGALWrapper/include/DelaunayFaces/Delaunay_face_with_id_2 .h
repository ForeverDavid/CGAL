#pragma once

#include "stdafx.h"
#include <CGAL/Delaunay_mesh_face_base_2.h>

namespace DelaunayFaces
{

	template <class Gt,
		class Fb = CGAL::Delaunay_mesh_face_base_2<Gt> >
		class Delaunay_face_with_id_2 : public Fb
	{
	public:
		typedef Gt Geom_traits;
		typedef typename Fb::Vertex_handle Vertex_handle;
		typedef typename Fb::Face_handle Face_handle;

		template < typename TDS2 >
		struct Rebind_TDS {
			typedef typename Fb::template Rebind_TDS<TDS2>::Other Fb2;
			typedef Delaunay_face_with_id_2 <Gt, Fb2> Other;
		};

		Delaunay_face_with_id_2() : Fb(), id(-1) {}

		Delaunay_face_with_id_2(Vertex_handle v0,
			Vertex_handle v1,
			Vertex_handle v2)
			: Fb(v0, v1, v2), id(-1) {}

		Delaunay_face_with_id_2(Vertex_handle v0,
			Vertex_handle v1,
			Vertex_handle v2,
			Face_handle n0,
			Face_handle n1,
			Face_handle n2)
			: Fb(v0, v1, v2, n0, n1, n2), id(-1) {}

		int id;
	};

}
