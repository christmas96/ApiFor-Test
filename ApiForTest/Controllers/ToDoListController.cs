using ApiForTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiForTest.Controllers
{
    public class ToDoListController : ApiController
    {

        public IEnumerable<TodoList> Get()
        {
            using (mydbformobileEntities entities = new mydbformobileEntities())
            {
                return entities.TodoList.ToList();
            }
        }

        public IHttpActionResult Put(TodoList list)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new mydbformobileEntities())
            {
                var existingTask = ctx.TodoList.Where(s => s.Id == list.Id).FirstOrDefault<TodoList>();

                if (existingTask != null)
                {
                    existingTask.title = list.title;
                    existingTask.description = list.description;
                    existingTask.done = list.done;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }
        
        public IHttpActionResult Delete(int id)
        {
            var ctx = new mydbformobileEntities();
            var existingTask = ctx.TodoList.Where(s => s.Id == id).FirstOrDefault<TodoList>();
            ctx.TodoList.Remove(existingTask);
            ctx.SaveChanges();
            return Ok();
        }

    }
}
