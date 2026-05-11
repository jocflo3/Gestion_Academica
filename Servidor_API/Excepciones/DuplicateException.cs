using Servidor_API.Helpers;

namespace Servidor_API.Excepciones
{
    public class DuplicateException : BusinessException
    {
        public string EntityName { get; }

        public object EntityId { get; }

        public DuplicateException(string Entidad, object Clave, bool esFemenino = false)
            : base($"{Entidad} {Clave} ya {ConditionalHelper.Select(esFemenino, "registrada", "registrado")}",
                404,
                "DUPLICATED")
        {
            EntityName = Entidad;
            EntityId = Clave;
        }
    }
}
