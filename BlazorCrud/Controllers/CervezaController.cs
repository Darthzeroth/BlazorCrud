using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorCrud.Models.Response; //Para utilizar la clase respuesta que se creo en response
using BlazorCrud.Models; //Para utilizar todos los modelos

namespace BlazorCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CervezaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta <List<Cerveza>> oRespuesta = new Respuesta<List<Cerveza>>(); //Creamos el objeto para mandar una respuesta
            //Creamos la consulta
            try //Cachamos los posibles errores
            { 
                using (BlazorCrudContext db = new BlazorCrudContext()) //Se abre y cierra la conexion al dejar de usar la BD
                {
                    var lst = db.Cervezas.ToList(); ///obtenemos la tabla
                    //Creamos la respuesta como exito y bajamos los datos
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }
            }catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;//Si hay un error regresa el mensaje del error
            }

                return Ok(oRespuesta); //Metodo para devolver el objeto json
        }

        [HttpPost] //Los datos llegaran por POST
        public IActionResult Add(Models.Request.CervezaRequest model) //Metodo para agregar datos a la BD
        {
            Respuesta<object> oRespuesta = new Respuesta<object>(); //Creamos el objeto para mandar una respuesta
            
            try //Cachamos los posibles errores
            {
                using (BlazorCrudContext db = new BlazorCrudContext()) //Se abre y cierra la conexion al dejar de usar la BD
                {
                    Cerveza oCerveza = new Cerveza(); //Creamos un objeto de tipo Cerveza
                    oCerveza.Marca = model.Marca; //Recibimos la marca
                    oCerveza.Nombre = model.Nombre; //Recibimos el nombre
                    db.Cervezas.Add(oCerveza); //Agregamos a la bd la info
                    db.SaveChanges(); //Guardamos los cambios 
                    oRespuesta.Exito = 1; //Devolvemos como exito
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;//Si hay un error regresa el mensaje del error
            }

            return Ok(oRespuesta); //Metodo para devolver el objeto json
        }

        [HttpPut] //Protocolo para editar
        public IActionResult Edit(Models.Request.CervezaRequest model) //Metodo para editar datos a la BD
        {
            Respuesta<object> oRespuesta = new Respuesta<object>(); //Creamos el objeto para mandar una respuesta

            try //Cachamos los posibles errores
            {
                using (BlazorCrudContext db = new BlazorCrudContext()) //Se abre y cierra la conexion al dejar de usar la BD
                {
                    Cerveza oCerveza = db.Cervezas.Find(model.Id); //Buscamos el elemento por el ID
                    oCerveza.Marca = model.Marca; //Recibimos la marca
                    oCerveza.Nombre = model.Nombre; //Recibimos el nombre
                    db.Entry(oCerveza).State= Microsoft.EntityFrameworkCore.EntityState.Modified; //Identificamos que se han hecho los cambios
                    db.SaveChanges(); //Guardamos los cambios 
                    oRespuesta.Exito = 1; //Devolvemos como exito
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;//Si hay un error regresa el mensaje del error
            }

            return Ok(oRespuesta); //Metodo para devolver el objeto json
        }

        [HttpDelete("{Id}")] //Protocolo para eliminar recibiendo el id para eliminar
        public IActionResult Delete(int Id) //Metodo para eliminar datos a la BD
        {
            Respuesta<object> oRespuesta = new Respuesta<object>(); //Creamos el objeto para mandar una respuesta

            try //Cachamos los posibles errores
            {
                using (BlazorCrudContext db = new BlazorCrudContext()) //Se abre y cierra la conexion al dejar de usar la BD
                {
                    Cerveza oCerveza = db.Cervezas.Find(Id); //Buscamos el elemento por el ID
                    db.Remove(oCerveza);
                    db.SaveChanges(); //Guardamos los cambios 
                    oRespuesta.Exito = 1; //Devolvemos como exito
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;//Si hay un error regresa el mensaje del error
            }

            return Ok(oRespuesta); //Metodo para devolver el objeto json
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            Respuesta<Cerveza> oRespuesta = new Respuesta<Cerveza>(); //Creamos el objeto para mandar una respuesta
            //Creamos la consulta

            try //Cachamos los posibles errores
            {
                using (BlazorCrudContext db = new BlazorCrudContext()) //Se abre y cierra la conexion al dejar de usar la BD
                {
                    var lst = db.Cervezas.Find(Id); ///obtenemos el elemento a editar
                    //Creamos la respuesta como exito y bajamos los datos
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;//Si hay un error regresa el mensaje del error
            }

            return Ok(oRespuesta); //Metodo para devolver el objeto json
        }


    }
}
