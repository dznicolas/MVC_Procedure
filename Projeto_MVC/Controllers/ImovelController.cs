using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Projeto_MVC.Models;
using static Projeto_MVC.Helper;

namespace Projeto_MVC.Controllers
{
    public class ImovelController : Controller
    {
        private readonly Contexto _context;

        public ImovelController(Contexto context)
        {
            _context = context;
        }

        // GET: Imovel
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Imovel.ToListAsync());

            var listagem = await _context.Imovel.FromSqlRaw("consultarTodos").ToListAsync();
            return View(listagem);
        }



        // GET: Imovel/AddOrEdit (Insert)
        // GET: Imovel/AddOrEdit/5 (Update)
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Imovel());
            else
            {
                var param = new SqlParameter("@ImovelId", id);
                var imovel = await _context.Imovel.FromSqlRaw("consultar @ImovelId", param).ToListAsync(); ;

                //var imovel = await _context.Imovel.FindAsync(id);
                if (imovel == null)
                {
                    return NotFound();
                }
                return View(imovel.FirstOrDefault());
            }
        }

        // POST: Imovel/AddOrEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("ImovelId,Finalidade,Valor,DataCadastro,ClienteId")] Imovel imovel)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    //imovel.DataCadastro = DateTime.Now;
                    var param = new SqlParameter("@Finalidade", imovel.Finalidade);
                    var param1 = new SqlParameter("@Valor", imovel.Valor);
                    var param2 = new SqlParameter("@DataCadastro", imovel.DataCadastro = DateTime.Now);
                    var param3 = new SqlParameter("@ClienteId", imovel.ClienteId);

                    await _context.Database.ExecuteSqlRawAsync("cadastro @Finalidade, @Valor, @DataCadastro,  @ClienteId",
                    param, param1, param2, param3);

                    //_context.Add(imovel);
                    await _context.SaveChangesAsync();

                }

                //Update
                else
                {
                    try
                    {
                        _context.Update(imovel);

                        var param = new SqlParameter("@ImovelId", imovel.ImovelId);
                        var param1 = new SqlParameter("@Finalidade", imovel.Finalidade);
                        var param2 = new SqlParameter("@Valor", imovel.Valor);
                        var param3 = new SqlParameter("@DataCadastro", imovel.DataCadastro = DateTime.Now);
                        var param4 = new SqlParameter("@ClienteId", imovel.ClienteId);

                        await _context.Database.ExecuteSqlRawAsync("alterar @ImovelId, @Finalidade, @Valor, @DataCadastro, @ClienteId",
                        param, param1, param2, param3, param4);

                        await _context.SaveChangesAsync();

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ImovelExists(imovel.ImovelId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Imovel.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", imovel) });
        }

        // POST: Imovel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Imovel == null)
            {
                return Problem("Entity set 'Contexto.Imovel'  is null.");
            }

            //var imovel = await _context.Imovel.FindAsync(id);
            var param = new SqlParameter("@ImovelId", id);
            await _context.Imovel.FromSqlRaw("consultar @ImovelId", param).ToListAsync();
            await _context.Database.ExecuteSqlRawAsync("excluir @ImovelId", param);

            //if (imovel != null)
            //{
            //    _context.Imovel.Remove(imovel);
            //}

            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Imovel.ToList()) });
        }

        private bool ImovelExists(int id)
        {
            //return _context.Imovel.Any(e => e.ImovelId == id);

            var param = new SqlParameter("@ImovelId", id);
            var imovel = _context.Imovel.FromSqlRaw("consultar @ImovelId", param).Any(); // Verifica se é true or false
            return imovel;
        }
    }
}
