using MessagePack;

namespace ArticulosAPI.Modelos.Dto
{
    public class ArticulosDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public int Cantidad { get; set; }
    }
}