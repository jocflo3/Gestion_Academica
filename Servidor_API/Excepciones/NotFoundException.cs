using Servidor_API.Helpers;
namespace Servidor_API.Excepciones
{
    public class NotFoundException: BusinessException
    {
        public string EntityName { get; }

        public object EntityId { get; }

        public NotFoundException(string Entidad,object Clave, bool esFemenino = false)
            : base( $"{Entidad} {Clave} no {ConditionalHelper.Select(esFemenino, "encontrada","encontrado")}",
                404,
                "NOT_FOUND")
        {
            EntityName = Entidad;
            EntityId = Clave;
        }
    } 
}
