using Microsoft.AspNetCore.Mvc;
using SistemaAutenticacao.Data;
using SistemaAutenticacao.Helper;
using SistemaAutenticacao.Models;

namespace SistemaAutenticacao.Controllers
{
    public class UsuariosController : Controller
    {

        readonly private ApplicationDbContext _db;

        public UsuariosController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Sucesso()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Perfil(UsuariosModel user)
        {
            return View(user);
        }

        [HttpPost]
        public IActionResult Cadastro(UsuariosModel usuario)
        {
            if(usuario != null)
            {
                UsuariosModel emailCheck = _db.Usuarios.FirstOrDefault(emailCheck => emailCheck.Email == usuario.Email);

                if(emailCheck == null) 
                {
                    if(usuario.Senha != null && usuario.Email != null && usuario.Nome != null)
                    {
                        string senhaHash = Criptografia.GerarHash(usuario.Senha);

                        usuario.Senha = senhaHash;

                        _db.Usuarios.Add(usuario);
                        _db.SaveChanges();
                        return RedirectToAction("Sucesso");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Preencha Todos os campos!";
                        return View();
                    }
                    
                }
                else
                {
                    TempData["ErrorMessage"] = "Email já cadastrado!";
                    return View();
                }

                
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(UsuariosModel usuario)
        {
            if(usuario != null)
            {
                UsuariosModel user = _db.Usuarios.FirstOrDefault(user => user.Email == usuario.Email);
                
                if(user != null)
                {
                    if (user.Senha == Criptografia.GerarHash(usuario.Senha))
                    {
                        return RedirectToAction("Perfil", user);
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Email ou Senha incorretos!";
                    return View();
                }

            }
            TempData["ErrorMessage"] = "Email ou Senha incorretos!";
            return View();
        }
    }
}
