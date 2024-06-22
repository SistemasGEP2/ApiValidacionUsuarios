namespace ApiValidacionUsuarios.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public int Rol { get; set; }  
        public string NombreRol { get; set; }
        public string IdAplicacion { get; set; }//Id de la aplicacion en la tabla rolesaplicaciones para la definicion de permisos

    }
}
