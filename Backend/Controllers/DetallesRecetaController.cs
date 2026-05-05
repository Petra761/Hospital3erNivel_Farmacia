using Data;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetallesRecetaController : ControllerBase
    {
        private readonly AppDbContext context;

        public DetallesRecetaController(AppDbContext context)
        {
            this.context = context;
        }
    }
}
