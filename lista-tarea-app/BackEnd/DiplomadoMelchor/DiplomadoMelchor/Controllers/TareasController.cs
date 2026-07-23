using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Tareas.Contextos;
using Tareas.Dtos;
using Tareas.Entidades;


namespace Tareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;


        public TareasController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var lista = await _context.Tareas
                    .Select(x => new TareaGetDto
                    {
                        IdTarea = x.IdTarea,
                        Nombre = x.Nombre,
                        Descripcion = x.Descripcion,
                        Fecha = x.Fecha,
                        FechaCompromiso = x.FechaCompromiso,
                        EstadoTarea = x.EstadoTarea,
                    
                        IdEmpleado = x.IdEmpleado,
                        NombreEmpleado = x.Empleado!.Nombre,
                    
                        IdProyecto = x.IdProyecto,
                        NombreProyecto = x.Proyecto!.Nombre,
                    
                        EstaActivo = x.EstaActivo
                    
                    })
                    .ToListAsync();


                if (lista == null || lista.Count == 0)
                    return NoContent();

                return Ok(lista);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
                return NotFound();

            return Ok(_mapper.Map<TareaGetDto>(tarea));
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(TareaSetDto newObj)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var obj = _mapper.Map<TareaEntity>(newObj);

                await _context.Tareas.AddAsync(obj);

                await _context.SaveChangesAsync();

                var dto = _mapper.Map<TareaGetDto>(obj);

                return CreatedAtAction(
                    nameof(ObtenerPorId),
                    new { id = obj.IdTarea },
                    dto
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }



        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> Buscar(int id)
        //{
        //    try
        //    {

        //        var entidad = await _context.Tareas.FindAsync(id);
        //        if (entidad == null)
        //            return NotFound("Tarea no encontrado");

        //        return Ok(_mapper.Map<TareaGetDto>(entidad));

        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);

        //    }



        //}

        [HttpPut]
        public async Task<IActionResult> Modificar(int id, TareaSetDto obj)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);



                if (obj == null)
                    return BadRequest("Identificador no coincide o cuerpo inválido.");


                var Tarea = await _context.Tareas.FindAsync(id);

                if (Tarea == null)
                    return NotFound("Tarea no encontrado");

                //mapear las columnas
                _mapper.Map(obj, Tarea);


                _context.Tareas.Update(Tarea);
                await _context.SaveChangesAsync();

                return Ok(_mapper.Map<TareaGetDto>(Tarea));


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }



        [HttpDelete]
        public async Task<IActionResult> Borrar(int id)
        {
            try
            {
                var Tarea = _context.Tareas.Find(id);

                if (Tarea == null)
                    return BadRequest("Tarea no encontrado");

                Tarea.EstaActivo = false ;
                await _context.SaveChangesAsync();

                return Ok(_mapper.Map<TareaGetDto>(Tarea));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }

       

    }
}
