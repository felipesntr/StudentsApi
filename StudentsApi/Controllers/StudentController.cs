using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsApi.Models;
using StudentsApi.Services;

namespace StudentsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Student>>> GetStudents()
        {
            try
            {
                var students = await this._studentService.GetStudents();
                return Ok(students);
            }
            catch (Exception ex)
            {
                //return BadRequest(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao obter alunos");
            }
        }


        [HttpGet("by-name")]
        public async Task<ActionResult<IAsyncEnumerable<Student>>> GetStudentsByName([FromQuery] string? name)
        {
            try
            {
                var students = await this._studentService.GetStudentsByName(name);
                if (students == null) return NotFound($"Não existe aluno com o nome {name}");
                return Ok(students);
            }
            catch (Exception ex)
            {
                //return BadRequest(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Erro ao obter alunos");
            }
        }

        [HttpGet("{id:int}", Name = "GetStudentsByName")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            try
            {
                var student = await this._studentService.GetStudent(id);
                if (student == null) return NotFound($"Não existe aluno com o ID {id}");
                return Ok(student);
            }
            catch (Exception ex)
            {
                //return BadRequest(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Erro ao obter alunos");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Student student)
        {
            try
            {
                await _studentService.CreateStudent(student);
                return CreatedAtRoute(nameof(GetStudentsByName), new { id = student.Id }, student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Student student)
        {
            try
            {
                if (student.Id == id)
                {
                    await _studentService.UpdateStudent(student);
                }
                return Ok("Aluno atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delte(int id)
        {
            try
            {
                var found = await _studentService.GetStudent(id);
                if (found != null)
                {
                    await _studentService.DeleteStudent(found);
                    return Ok("Aluno apagado com sucesso!");
                }
                else
                {
                    return NotFound("Aluno não encontrado!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
