using Microsoft.AspNetCore.Mvc;
using Tareas.Entidades;
using Tareas.Dtos;
using Tareas.Contextos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;


        public EmpleadosController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var lista = await _context.Empleados
                .Select(x => _mapper.Map<EmpleadoGetDto>(x))
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
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Buscar(int id)
        {
            try
            {

                var entidad = await _context.Empleados.FindAsync(id);
                if (entidad == null)
                    return NotFound("Empleado no encontrado");

                return Ok(_mapper.Map<EmpleadoGetDto>(entidad));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }



        }
        [HttpPost]
        public async Task<IActionResult> Guardar(EmpleadoSetDto newObj)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Modelo invalido");


                var obj = _mapper.Map<EmpleadosEntity>(newObj);

                _context.Empleados.AddAsync(obj);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(Buscar), new { id = obj.IdEmpleado }, obj);


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }


        }
        [HttpPut]
        public async Task<IActionResult> Modificar(int id, EmpleadoSetDto obj)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);



                if (obj == null)
                    return BadRequest("Identificador no coincide o cuerpo inválido.");


                var Empleado = await _context.Empleados.FindAsync(id);

                if (Empleado == null)
                    return NotFound("Empleado no encontrado");

                //mapear las columnas
                _mapper.Map(obj, Empleado);


                _context.Empleados.Update(Empleado);
                await _context.SaveChangesAsync();

                return Ok(_mapper.Map<EmpleadoGetDto>(Empleado));


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }



        }
        [HttpDelete]
        //public async Task<IActionResult> Borrar(int id)
        //{
        //    try
        //    {
        //        var Empleado = _context.Empleados.Find(id);

        //        if (Empleado == null)
        //            return BadRequest("Empleado no encontrado");

        //        _context.Empleados.Remove(Empleado);
        //        await _context.SaveChangesAsync();

        //        return Ok(_mapper.Map<EmpleadoGetDto>(Empleado));

        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);

        //    }




        //    //var Empleado = await _context.Empleados.FindAsync(id);
        //    //if (Empleado == null)
        //    //    return NotFound();

        //    //_context.Empleados.Remove(Empleado);
        //    //await _context.SaveChangesAsync();

        //    //return NoContent();
        //}

        [HttpDelete]
        public async Task<IActionResult> Borrar(int id)
        {
            try
            {
                var empleado = await _context.Empleados.FindAsync(id);

                if (empleado == null)
                    return NotFound("Empleado no encontrado");


                empleado.EstaActivo = false;


                _context.Empleados.Update(empleado);

                await _context.SaveChangesAsync();


                return Ok(_mapper.Map<EmpleadoGetDto>(empleado));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
