using Microsoft.AspNetCore.Mvc;
using ProjetoModelo.DbContext;
using ProjetoModelo.Model;

namespace ProjetoModelo.Controllers
{
	public class ProdutoController : Controller
	{
		private AppDbContext _context;

		public ProdutoController(AppDbContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		[HttpGet]
		[Route("consultarProdutosPorId")]
		public async Task<IActionResult> ConsultarPorId([FromQuery] int id)
		{
			var produtos = new List<Produto>();
			try
			{
				_context.OpenConnection();

				var command = _context.CreateCommand();
				command.CommandText = $"SELECT Id, Nome, Preco FROM Produto WHERE 1 = 1 AND Produto.Id = {id}";

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						produtos.Add(new Produto
						{
							Id = reader.GetInt32("Id"),
							Nome = reader.GetString("Nome"),
							Preco = reader.GetDecimal("Preco"),
						});
					}
				}

				return Ok(produtos);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			finally
			{
				_context.CloseConnection();
			}
		}

		[HttpGet]
		[Route("selecionarProdutos")]
		public async Task<IActionResult> Selecionar()
		{
			var produtos = new List<Produto>();
			try
			{
				_context.OpenConnection();

				var command = _context.CreateCommand();
				command.CommandText = "SELECT Id, Nome, Preco FROM Produto";

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						produtos.Add(new Produto
						{
							Id = reader.GetInt32("Id"),
							Nome = reader.GetString("Nome"),
							Preco = reader.GetDecimal("Preco"),
						});
					}
				}

				return Ok(produtos);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			finally
			{
				_context.CloseConnection();
			}
		}

		[HttpPost]
		[Route("inserirProduto")]
		public async Task<IActionResult> Inserir([FromBody] Produto produto)
		{
			try
			{
				_context.OpenConnection();
				var command = _context.CreateCommand();

				command.CommandText = "INSERT INTO Produto (Nome, Preco) VALUES (@Nome, @Preco)";
				command.Parameters.AddWithValue("@Nome", produto.Nome);
				command.Parameters.AddWithValue("@Preco", produto.Preco);

				command.ExecuteScalar();

				return Ok("Produto Inserido com Sucesso!!!");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		[Route("atualizarProduto")]
		public async Task<IActionResult> Atualizar([FromBody] Produto produto)
		{
			try
			{
				_context.OpenConnection();

				var command = _context.CreateCommand();
				command.CommandText = "UPDATE Produto SET Nome = @Nome, Preco = @Preco WHERE Id = @Id";
				command.Parameters.AddWithValue("@Id", produto.Id);
				command.Parameters.AddWithValue("@Nome", produto.Nome);
				command.Parameters.AddWithValue("@Preco", produto.Preco);

				command.ExecuteNonQuery();

				return Ok("Produto Inserido com Sucesso!!!");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		[Route("atualizarProduto")]
		public async Task<IActionResult> Deletar([FromQuery] int id)
		{
			try
			{
				_context.OpenConnection();

				var command = _context.CreateCommand();
				command.CommandText = "DELETE FROM Produto WHERE Id = @Id";
				command.Parameters.AddWithValue("@Id", id);

				command.ExecuteNonQuery();

				return Ok("Produto Inserido com Sucesso!!!");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
