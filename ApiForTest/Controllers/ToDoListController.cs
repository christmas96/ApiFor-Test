using ApiForTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ApiForTest.Controllers
{
    [RoutePrefix("api/ToDoList")]
    public class ToDoListController : ApiController
    {

        public IEnumerable<TodoList> Get()
        {
            using (mydbformobileEntitiesList entities = new mydbformobileEntitiesList())
            {
                return entities.TodoList.ToList();
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(TodoList))]
        public IHttpActionResult Put(int Id, TodoList list)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new mydbformobileEntitiesList())
            {
                var existingStudent = ctx.TodoList.Where(s => s.Id == list.Id).FirstOrDefault<TodoList>();

                if (existingStudent != null)
                {
                    existingStudent.title = list.title;
                    existingStudent.description = list.description;
                    existingStudent.done = list.done;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        [Route("{id:int}")]
        [ResponseType(typeof(TodoList))]
        public IHttpActionResult Delete(int id)
        {
            var ctx = new mydbformobileEntitiesList();
            var existingTask = ctx.TodoList.Where(s => s.Id == id).FirstOrDefault<TodoList>();
            ctx.TodoList.Remove(existingTask);
            ctx.SaveChanges();
            return Ok();
        }

    }
}
