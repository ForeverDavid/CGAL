using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;

namespace CGAL.Meshes.FaceBased
{

    public class FBVertex
    {

        public FBFace Face { get; set; }

        public FBVertex()
        {
            
        }

        public virtual void Initialize(Vector2f pos)
        {

        }

        public FACE GetFace<FACE>() where FACE : FBFace
        {
            if (Face == null) return null;

            FACE face = Face as FACE;
            if (face == null)
                throw new InvalidCastException("Face is not a " + typeof(FACE));

            return face;
        }

        public virtual void Clear()
        {
            Face = null;
        }

    }
}
