using System;
using System.Collections.Generic;
using System.Linq;



namespace TESTFINAL
{
  
    class Programa
    {
        static void Main(string[] args)
        {
           
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            
            var interfaz = new CapasDisenoInterfazLogistica();
            interfaz.Ejecutar();
        }
    }
}
