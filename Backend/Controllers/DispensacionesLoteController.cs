using Data;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DispensacionesLoteController : ControllerBase
    {
        private readonly AppDbContext context;

        public DispensacionesLoteController(AppDbContext context)
        {
            this.context = context;
        }
    }
}
