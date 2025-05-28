using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP04_Ahorcado.Models;
namespace TP04_Ahorcado.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    // todo lo que sea logica del juego va en models
    // llamar con metodos desde controllers para que forme la palabra, se fije la cantidad de intentos, de letras que tiene la palabra, etc.
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult adivinaLetra(char adivinar)
    {
        const int MAX_INTENTOS = 8;
        adivinar = char.ToLower(adivinar);
        ViewBag.intentos = Juego.intentos;
        if(!Juego.aciertos.Contains(adivinar) && !Juego.errores.Contains(adivinar))
        {
            Juego.intentos++;
            ViewBag.intentos = Juego.intentos;
            List<int> indexAciertos = new List<int>();
            bool correcto = false;
            ViewBag.mensaje = null;
            for (int i = 0; i < Juego.palabra.Length; i++)
            {
                if (char.ToLower(Juego.palabra[i]) == adivinar)
                {
                    indexAciertos.Add(i);
                    correcto = true;
                }
            }
            if (correcto)
            {
                Juego.aciertos.Add(adivinar);
                foreach (int index in indexAciertos)
                {
                    Juego.render[index] = adivinar;
                }
            }
            else {
                Juego.errores.Add(adivinar);
                if(Juego.errores.Count >= MAX_INTENTOS){
                    ViewBag.mensaje = $"Parece que {Juego.intentos} intentos no fueron suficientes";
                    ViewBag.resultado = correcto;
                    ViewBag.palabra = Juego.palabra;
                    return View("Resultado");
                }
            }
            int j = -1;
            do
            {
                j++;
            } while (j < Juego.palabra.Length && Juego.palabra[j] == Juego.render[j]);
            if (j >= Juego.palabra.Length)
            {
                ViewBag.resultado = true;
                return View("Resultado");
            }
            else
            {
                ViewBag.aciertos = Juego.aciertos;
                ViewBag.errores = Juego.errores;
                ViewBag.render = Juego.render;
                return View("Partida");
            }
        }else{
            ViewBag.mensaje = "Ya intentaste con esa letra";
            ViewBag.aciertos = Juego.aciertos;
            ViewBag.errores = Juego.errores;
            ViewBag.render = Juego.render;
            return View("Partida");
        }
    }
    public IActionResult adivinaPalabra(string adivinaPalabra)
    {
        Juego.intentos++;
        bool correcto = adivinaPalabra.ToLower() == Juego.palabra.ToLower();
        ViewBag.intentos = Juego.intentos;
        ViewBag.palabra = Juego.palabra;
        ViewBag.resultado = correcto;
        return View("Resultado");
    }
    public IActionResult nuevaPartida()
    {
        Juego.InicializarPalabra();
        ViewBag.intentos = Juego.intentos;
        ViewBag.aciertos = Juego.aciertos;
        ViewBag.errores = Juego.errores;
        return View("Partida");
    }
}