using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CadastroSeguroAPI.Data;
using CadastroSeguroAPI.DTOs;
using CadastroSeguroAPI.Models;

namespace CadastroSeguroAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(UsuarioCriacaoDto dto)
        {
            bool emailJaExiste = await _context.Usuarios.AnyAsync(u => u.Email == dto.Email);

            if (emailJaExiste)
                return BadRequest("Este e-mail já está cadastrado.");

            string senhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = senhaHash
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var retorno = new UsuarioExibicaoDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };

            return CreatedAtAction(nameof(BuscarPorId), new { id = usuario.Id }, retorno);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioExibicaoDto>>> Listar()
        {
            var usuarios = await _context.Usuarios
                .Select(u => new UsuarioExibicaoDto
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    SenhaHash = u.SenhaHash
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioExibicaoDto>> BuscarPorId(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            var retorno = new UsuarioExibicaoDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                SenhaHash = usuario.SenhaHash
            };

            return Ok(retorno);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (usuario == null)
                return Unauthorized("E-mail ou senha inválidos.");

            bool senhaValida = BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash);

            if (!senhaValida)
                return Unauthorized("E-mail ou senha inválidos.");

            return Ok(new
            {
                mensagem = "Login realizado com sucesso.",
                usuario = new
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email
                }
            });
        }
    }
}