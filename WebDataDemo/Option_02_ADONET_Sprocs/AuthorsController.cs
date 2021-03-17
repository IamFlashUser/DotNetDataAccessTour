﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using WebDataDemo.Model;

namespace WebDataDemo.Option_02_ADONET_Sprocs
{
    [Route("api/v02/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly string _connString;

        public AuthorsController(IConfiguration config)
        {
            _connString = config.GetConnectionString("DefaultConnection");
        }

        // GET: api/<AuthorsController>
        [HttpGet]
        public ActionResult<Author> Get()
        {
            var authors = new List<Author>();
            using var conn = new SqlConnection(_connString);
            var sql = "ListAuthors";
            var cmd = new SqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            conn.Open();
            using var reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                while (reader.Read())
                {
                    var author = new Author();
                    author.Id = reader.GetInt32(0);
                    author.Name = reader.GetString(1);
                    authors.Add(author);
                }
            }

            return Ok(authors);
        }

        // GET api/<AuthorsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthorsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AuthorsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthorsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}