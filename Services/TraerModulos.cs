using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiValidacionUsuarios.Services
{
    public class TraerModulosService
    {
        private readonly AccesoDatosSoapClient _accesoDatosSoapClient;

        public TraerModulosService()
        {
            _accesoDatosSoapClient = new AccesoDatosSoapClient(AccesoDatosSoapClient.EndpointConfiguration.AccesoDatosSoap);
        }

        public async Task<List<Dictionary<string,object>>> TraerModulosAsync(string ParametroUsuario)
        { //Traer IdUsuario comparando usuario, el idusuario es la relacion con la tabla UsuariosModulos 
            try
            {
                DataTable tablaUsuarios = await _accesoDatosSoapClient.TraerTablaAsync("Usuarios", 0);
                List<Dictionary<string, object>> listaUsuarios = new List<Dictionary<string, object>>();

                foreach (DataRow filaTablaUsuarios in tablaUsuarios.Rows)
                {
                    if (filaTablaUsuarios["Usuario"].ToString() == ParametroUsuario)
                    {
                        var diccionarioUsuario = new Dictionary<string, object>();

                        foreach(DataColumn columna in tablaUsuarios.Columns)
                        {
                            diccionarioUsuario[columna.ColumnName] = filaTablaUsuarios[columna];
                        }

                        listaUsuarios.Add(diccionarioUsuario);

                        var IdUsuario = filaTablaUsuarios["IdUsuario"].ToString();

                        DataTable tablaUsuariosModulos = await _accesoDatosSoapClient.TraerTablaAsync("UsuariosModulos", 0);
                        List<Dictionary<string, object>> listaUsuariosModulos = new List<Dictionary<string, object>>();

                        foreach(DataRow filaModulos in tablaUsuariosModulos.Rows)
                        {
                            if (filaModulos["IdUsuario"].ToString() == IdUsuario)
                            {
                                var diccUsuarioModulos = new Dictionary<string, object>();
                                foreach(DataColumn columnaUsuaModulos in tablaUsuariosModulos.Columns)
                                {
                                    diccUsuarioModulos[columnaUsuaModulos.ColumnName] = filaModulos[columnaUsuaModulos];
                                }

                                listaUsuariosModulos.Add(diccUsuarioModulos);                                
                            }
                        }
                        diccionarioUsuario["ModulosUsuario"] = listaUsuariosModulos;
                    }
                }
                return listaUsuarios;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error con la obtencion de los modulos del Usuario: {ParametroUsuario} Error : {ex}");
            }
            
        }
    }
}
