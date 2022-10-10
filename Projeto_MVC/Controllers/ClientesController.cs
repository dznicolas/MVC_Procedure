using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Projeto_MVC.Models;
using static Projeto_MVC.Helper;

namespace Projeto_MVC.Controllers
{
    public class ClientesController : Controller
    {
        private readonly Contexto _context;

        public ClientesController(Contexto context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Produto.ToListAsync());      

            var listagem = await _context.Cliente.FromSqlRaw("consultarTodos").ToListAsync();
            return View(listagem);
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var param = new SqlParameter("@id", id);
            var cliente = await _context.Cliente.FromSqlRaw("consultar @id", param).ToListAsync();



            //   .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente.FirstOrDefault());
        }

        // GET: Clientes/AddOrEdit
        // GET: CLientes/AddOrEdit/5
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Cliente());

            else
            {
                //var cliente = await _context.Cliente.FindAsync(id);
                var param = new SqlParameter("@id", id);
                var cliente = await _context.Cliente.FromSqlRaw("consultar @id", param).ToListAsync();

                if (cliente == null)
                {
                    return NotFound();
                }
                return View(cliente.FirstOrDefault());

            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,NomeCliente, TipoCliente, NomeContato, TelefoneContato, Cidade, Bairro, Logradouro, DataCadastro")] Cliente cliente)
        {

            if (ModelState.IsValid)
            {
                if (id == 0) //Insert
                {
                    var param = new SqlParameter("@NomeCliente", cliente.NomeCliente);
                    var param1 = new SqlParameter("@TipoCliente", cliente.TipoCliente);
                    var param2 = new SqlParameter("@NomeContato", cliente.NomeContato);
                    var param3 = new SqlParameter("@TelefoneContato", cliente.TelefoneContato);
                    var param4 = new SqlParameter("@Cidade", cliente.Cidade);
                    var param5 = new SqlParameter("@Bairro", cliente.Bairro);
                    var param6 = new SqlParameter("@Logradouro", cliente.Logradouro);
                    var param7 = new SqlParameter("@DataCadastro", cliente.DataCadastro = DateTime.Now);

                    await _context.Database.ExecuteSqlRawAsync("cadastro @NomeCliente, @TipoCliente, @NomeContato, @TelefoneContato, @Cidade, @Bairro, @Logradouro, @DataCadastro",
                    param, param1, param2, param3, param4, param5, param6, param7);

                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {    //Update
                        _context.Update(cliente);
                        var param = new SqlParameter("@Id", cliente.Id);
                        var param1 = new SqlParameter("@NomeCliente", cliente.NomeCliente);
                        var param2 = new SqlParameter("@TipoCliente", cliente.TipoCliente);
                        var param3 = new SqlParameter("@NomeContato", cliente.NomeContato);
                        var param4 = new SqlParameter("@TelefoneContato", cliente.TelefoneContato);
                        var param5 = new SqlParameter("@Cidade", cliente.Cidade);
                        var param6 = new SqlParameter("@Bairro", cliente.Bairro);
                        var param7 = new SqlParameter("@Logradouro", cliente.Logradouro);
                        var param8 = new SqlParameter("@DataCadastro", cliente.DataCadastro);
                        //var param9 = new SqlParameter("@DataAtualizacao", cliente.DataAtualizacao);
                        await _context.Database.ExecuteSqlRawAsync("alterar @Id, @NomeCliente, @TipoCliente, @NomeContato, @TelefoneContato, @Cidade, @Bairro, @Logradouro, @DataCadastro",
                        param, param1, param2, param3, param4, param5, param6, param7, param8);

                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ClienteExists(cliente.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Cliente.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", cliente) });
        }


        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cliente == null)
            {
                return Problem("Entity set 'Contexto.Cliente'  is null.");
            }
            // var cliente = await _context.Cliente.FindAsync(id);
            var param = new SqlParameter("@id", id);
            var cliente = await _context.Cliente.FromSqlRaw("consultar @id", param).ToListAsync();

            await _context.Database.ExecuteSqlRawAsync("excluir @id", param);

            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Cliente.ToList()) });
        }

        private bool ClienteExists(int id)
        {
            // return _context.Cliente.Any(e => e.Id == id);
            var param = new SqlParameter("@id", id);
            var cliente = _context.Cliente.FromSqlRaw("consultar @id", param).Any(); // Verifica se é true or false
            return cliente;
        }
    }
}
