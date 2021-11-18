using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Caso_de_Estudio.Filters
{/*
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class Seguridad_Opciones : AuthorizeAttribute
    {
        private Models.Usuario oUsuario;
        Models.BDLabTICEntities2 db = new Models.BDLabTICEntities2();
        private int idOperacion; 

        public Seguridad_Opciones(int idOperaciones =0)
        {
            this.idOperacion = idOperacion;
        }
           public override void OnAuthorization(AuthorizationContext filterContext)
        {
            String nombreOpcion = "";
            String nombreModulo = "";
            try
            {
                oUsuario = (usuario)HttpContext.Current.Session["Usuario"];
                var lsOperaciones = from m in db.RolOpcion where
                m.idRol == oUsuario.idRol && m.idOpcion == idOperacion select m;
            
                if(lsOperaciones.ToList().Count()==0)
                {
                    var oOperacion = db.Opcion.Find(idOperacion);
                    int? idModulo = oOperacion.id

                }
            
            
            }catch(Exception ex)
            {

            }
        }
    }*/
}