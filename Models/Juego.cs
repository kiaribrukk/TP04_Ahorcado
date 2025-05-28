using System;
using System.Collections.Generic;
namespace TP04_Ahorcado.Models;
static class Juego
{
    public static int intentos;
    public static List<char> aciertos;
    public static List<char> errores;
    public static string palabra { get; private set; }
    private static List<string> palabras;
    public static List<char> render;
    public static void InicializarPalabra(){
        palabras = new List<string> 
        {
          "ahorcado",
          "programacion",
          "partida", 
          "informatica", 
          "models",
          "controllers",
          "views",
          "estudiar"  
        };
        Random random = new Random();
        palabra =  palabras[random.Next(0, palabras.Count - 1)];
        render = new List<char>();
        aciertos = new List<char>();
        errores = new List<char>();
        foreach (char letra in palabra)
        {
            Juego.render.Add(letra);
        }
        intentos = 0;
    }
}