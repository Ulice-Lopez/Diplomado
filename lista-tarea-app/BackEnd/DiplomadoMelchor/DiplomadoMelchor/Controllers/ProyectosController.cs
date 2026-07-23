using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tareas.Contextos;
using Tareas.Dtos;

using Tareas.Entidades;

namespace Tareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper; 


        public ProyectosController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var lista = await _context.Proyectos
                .Select(x => _mapper.Map<ProyectoGetDto>(x))
                .ToListAsync();

                if (lista == null || lista.Count == 0)
                    return NoContent();

                return Ok(lista);
            }
            catch(Exception ex) {
                
                return BadRequest(ex.Message);

            }

            
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Buscar(int id)
        {
            try
            {

                var entidad = await _context.Proyectos.FindAsync(id);
                if (entidad == null)
                    return NotFound("Proyecto no encontrado");

                return Ok(_mapper.Map<ProyectoGetDto>(entidad));

            }
            catch (Exception ex) {

                return BadRequest(ex.Message);
            
            }


            
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(ProyectoSetDto newObj)
        {
            try
            {
                if(!ModelState.IsValid) 
                        return BadRequest("Modelo invalido");


                var obj = _mapper.Map <ProyectoEntity>(newObj);

                _context.Proyectos.AddAsync(obj);
                await _context.SaveChangesAsync();

                
                return CreatedAtAction(nameof(Buscar), new { id = obj.IdProyecto }, obj);


            }
            catch (Exception ex) {
            
                return BadRequest(ex.Message);

            }

            
        }

        [HttpPut]
        public async Task<IActionResult> Modificar(int id, ProyectoSetDto obj)
        {

            try
            {
                 if (!ModelState.IsValid)
                    return BadRequest(ModelState);



                if (obj == null)
                    return BadRequest("Identificador no coincide o cuerpo inválido.");
               

                var proyecto = await _context.Proyectos.FindAsync(id);
               
                if (proyecto == null)
                    return NotFound("Proyecto no encontrado");

                _mapper.Map(obj, proyecto);

                _context.Proyectos.Update(proyecto);
                await _context.SaveChangesAsync();

                return Ok(_mapper.Map <ProyectoGetDto>(proyecto));


            }
            catch (Exception ex) { 
            
                return BadRequest(ex.Message);
            
            }


            
        }


        //[HttpDelete]
        //public async Task<IActionResult> Borrar(int id)
        //{
        //    try
        //    {
        //        var proyecto = _context.Proyectos.Find(id);

        //        if (proyecto == null)
        //            return BadRequest("Proyecto no encontrado");

        //         _context.Proyectos.Remove(proyecto);
        //        await _context.SaveChangesAsync();

        //        return Ok(_mapper.Map<ProyectoGetDto>(proyecto));

        //    }
        //    catch (Exception ex) {

        //        return BadRequest(ex.Message);

        //    }




        //    //var proyecto = await _context.Proyectos.FindAsync(id);
        //    //if (proyecto == null)
        //    //    return NotFound();

        //    //_context.Proyectos.Remove(proyecto);
        //    //await _context.SaveChangesAsync();

        //    //return NoContent();
        //}
        [HttpDelete]
        public async Task<IActionResult> Borrar(int id)
        {
            try
            {
                var proyecto = await _context.Proyectos.FindAsync(id);

                if (proyecto == null)
                    return NotFound("Proyecto no encontrado");

                proyecto.EstaActivo = false;

                _context.Proyectos.Update(proyecto);
                await _context.SaveChangesAsync();

                return Ok(_mapper.Map<ProyectoGetDto>(proyecto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
