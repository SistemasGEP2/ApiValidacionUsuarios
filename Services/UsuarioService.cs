using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiValidacionUsuarios.Services
{
    public class UsuarioService
    {
        private readonly AccesoDatosSoapClient _accesoDatosSoapClient;

        public UsuarioService()
        {
            _accesoDatosSoapClient = new AccesoDatosSoapClient(AccesoDatosSoapClient.EndpointConfiguration.AccesoDatosSoap);
        }

        public async Task<List<Dictionary<string, object>>> TraerUsuariosAsync(string ParametroUsuario)
        {
            try
            {
                
                DataTable tablaUsuarios = await _accesoDatosSoapClient.TraerTablaAsync("Usuarios", 0);
                List<Dictionary<string, object>> listaUsuarios = new List<Dictionary<string, object>>();

                foreach (DataRow fila in tablaUsuarios.Rows)
                {
                    if (fila["Usuario"].ToString() == ParametroUsuario)
                    {
                        var diccionarioUsuario = new Dictionary<string, object>();

                        foreach (DataColumn columna in tablaUsuarios.Columns)
                        {
                            diccionarioUsuario[columna.ColumnName] = fila[columna];
                        }

                        listaUsuarios.Add(diccionarioUsuario);

                        
                        var IdRol = fila["IdRol"].ToString();
                        var IdUsuario = fila["IdUsuario"].ToString();

                        
                        DataTable tablaRol = await _accesoDatosSoapClient.TraerTablaAsync("Roles", 0);
                        List<Dictionary<string, object>> listaRolApp = new List<Dictionary<string, object>>();

                        foreach (DataRow filaRol in tablaRol.Rows)
                        {
                            if (filaRol["IdRol"].ToString() == IdRol)
                            {

                                DataTable tablaRolesApp = await _accesoDatosSoapClient.TraerTablaAsync("RolesAplicaciones", 0);

                                foreach (DataRow filaTablaRol in tablaRolesApp.Rows)
                                {
                                    if (filaTablaRol["IdRol"].ToString() == IdRol)
                                    {
                                        var diccRolesApp = new Dictionary<string, object>();

                                        foreach (DataColumn columna in tablaRolesApp.Columns)
                                        {
                                            diccRolesApp[columna.ColumnName] = filaTablaRol[columna];
                                        }

                                        listaRolApp.Add(diccRolesApp);
                                    }
                                }
                        
                            } 
                        }
                        DataTable tablaUsuaApps = await _accesoDatosSoapClient.TraerTablaAsync("Usuarios_Aplicaciones", 0);
                        List<Dictionary<string, object>> listaUsuaApps = new List<Dictionary<string, object>>();

                        foreach(DataRow filaTablaUsuaApps in tablaUsuaApps.Rows)
                        {
                            if (filaTablaUsuaApps["IdUsuario"].ToString() == IdUsuario)
                            {
                                var diccUsuaApps = new Dictionary<string,object>();

                                foreach(DataColumn columnaUsuaApps in tablaUsuaApps.Columns)
                                {
                                    diccUsuaApps[columnaUsuaApps.ColumnName] = filaTablaUsuaApps[columnaUsuaApps];
                                }

                                listaUsuaApps.Add(diccUsuaApps);
                            }
                        }                    
                        diccionarioUsuario["RolesAplicaciones"] = listaRolApp;
                        diccionarioUsuario["Usuario Aplicaciones"] = listaUsuaApps;
                    }
                }

                return listaUsuarios;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener todos los usuarios: {ex.Message}");
            }
        }
    }
}

