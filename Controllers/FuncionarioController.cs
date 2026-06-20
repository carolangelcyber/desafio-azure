using Microsoft.AspNetCore.Mvc;
using System;
using TrilhaNetAzureDesafio.Context;
using TrilhaNetAzureDesafio.Models;

namespace TrilhaNetAzureDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly RHContext _context;

        public FuncionarioController(RHContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var funcionario = _context.Funcionarios.Find(id);
            if (funcionario == null) return NotFound();
            return Ok(funcionario);
        }

        [HttpPost]
        public IActionResult Criar(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            _context.SaveChanges();

            var log = new FuncionarioLog
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                TipoAcao = "Inclusao",
                PartitionKey = funcionario.Departamento,
                RowKey = funcionario.Id.ToString(),
                Timestamp = DateTimeOffset.UtcNow
            };
            _context.FuncionariosLog.Add(log);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterPorId), new { id = funcionario.Id }, funcionario);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Funcionario funcionario)
        {
            var funcionarioBanco = _context.Funcionarios.Find(id);
            if (funcionarioBanco == null) return NotFound();

            funcionarioBanco.Nome = funcionario.Nome;
            funcionarioBanco.Endereco = funcionario.Endereco;
            funcionarioBanco.Ramal = funcionario.Ramal;
            funcionarioBanco.EmailProfissional = funcionario.EmailProfissional;
            funcionarioBanco.Departamento = funcionario.Departamento;
            funcionarioBanco.Salario = funcionario.Salario;
            funcionarioBanco.DataAdmissao = funcionario.DataAdmissao;

            _context.SaveChanges();

            var log = new FuncionarioLog
            {
                Id = id,
                Nome = funcionario.Nome,
                TipoAcao = "Atualizacao",
                PartitionKey = funcionario.Departamento,
                RowKey = id.ToString(),
                Timestamp = DateTimeOffset.UtcNow
            };
            _context.FuncionariosLog.Add(log);
            _context.SaveChanges();

            return Ok(funcionarioBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var funcionarioBanco = _context.Funcionarios.Find(id);
            if (funcionarioBanco == null) return NotFound();

            _context.Funcionarios.Remove(funcionarioBanco);

            var log = new FuncionarioLog
            {
                Id = id,
                Nome = funcionarioBanco.Nome,
                TipoAcao = "Remocao",
                PartitionKey = funcionarioBanco.Departamento,
                RowKey = id.ToString(),
                Timestamp = DateTimeOffset.UtcNow
            };
            _context.FuncionariosLog.Add(log);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
