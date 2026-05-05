using Data;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PosologiasController : ControllerBase
    {
        private readonly AppDbContext context;

        public PosologiasController(AppDbContext context)
        {
            this.context = context;
        }
    }
}
