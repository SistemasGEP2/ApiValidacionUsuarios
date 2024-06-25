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

        public async Task<List<Dictionary<string, object>>> TraerModulosAsync(string ParametroUsuario)
        {
            try
            {
                DataTable tablaUsuarios = await _accesoDatosSoapClient.TraerTablaAsync("Usuarios", 0);
                List<Dictionary<string, object>> listaUsuarios = new List<Dictionary<string, object>>();

                foreach (DataRow filaTablaUsuarios in tablaUsuarios.Rows)
                {
                    if (filaTablaUsuarios["Usuario"].ToString() == ParametroUsuario)
                    {
                        var diccionarioUsuario = new Dictionary<string, object>();

                        foreach (DataColumn columna in tablaUsuarios.Columns)
                        {
                            diccionarioUsuario[columna.ColumnName] = filaTablaUsuarios[columna];
                        }

                        listaUsuarios.Add(diccionarioUsuario);

                        var IdUsuario = filaTablaUsuarios["IdUsuario"].ToString();
                        var IdRol = filaTablaUsuarios["IdRol"].ToString();

                        DataTable tablaUsuariosModulos = await _accesoDatosSoapClient.TraerTablaAsync("UsuariosModulos", 0);
                        var listaUsuariosModulos = new List<Dictionary<string, object>>();
                        var listaModulosApp = new List<Dictionary<string, object>>();

                        DataTable tablaModulosApp = await _accesoDatosSoapClient.TraerTablaAsync("ModulosAplicaciones", 0);

                        foreach (DataRow filaModulos in tablaUsuariosModulos.Rows)
                        {
                            if (filaModulos["IdUsuario"].ToString() == IdUsuario)
                            {
                                var diccUsuarioModulos = new Dictionary<string, object>();
                                foreach (DataColumn columnaUsuaModulos in tablaUsuariosModulos.Columns)
                                {
                                    diccUsuarioModulos[columnaUsuaModulos.ColumnName] = filaModulos[columnaUsuaModulos];
                                }

                                listaUsuariosModulos.Add(diccUsuarioModulos);

                        }
                        var IdModulo = filaModulos["IdModulo"].ToString();

                        foreach (DataRow filaModulosApp in tablaModulosApp.Rows)
                        {
                            if (filaModulosApp["IdModulo"].ToString() == IdModulo)
                            {
                                var diccModulosApp = new Dictionary<string, object>();
                                foreach (DataColumn columnaModulosApp in tablaModulosApp.Columns)
                                {
                                    diccModulosApp[columnaModulosApp.ColumnName] = filaModulosApp[columnaModulosApp];
                                }

                                listaModulosApp.Add(diccModulosApp);
                            }
                        }

                        DataTable rolesModulos = await _accesoDatosSoapClient.TraerTablaAsync("RolesModulos", 0);
                        List<Dictionary<string,object>> listaRolesModulos = new List<Dictionary<string, object>>();

                        foreach(DataRow filaRolesModulos in rolesModulos.Rows)
                            {
                                if (filaRolesModulos["IdRol"].ToString() == IdRol)
                                {
                                    var diccRolesModulos = new Dictionary<string, object>();
                                    foreach(DataColumn columnaRolesModulos in rolesModulos.Columns)
                                    {
                                        diccRolesModulos[columnaRolesModulos.ColumnName] = filaRolesModulos[columnaRolesModulos];
                                    }

                                    listaRolesModulos.Add(diccRolesModulos);
                                }
                            }

                        diccionarioUsuario["ModulosUsuario"] = listaUsuariosModulos;
                        diccionarioUsuario["ModulosAplicaciones"] = listaModulosApp;
                        diccionarioUsuario["Roles Modulos"] = listaRolesModulos;
                    }


                        return listaUsuarios;
                    }
                }

                return listaUsuarios;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error con la obtención de los módulos del Usuario: {ParametroUsuario}. Error: {ex}");
            }
        }
    }
}
