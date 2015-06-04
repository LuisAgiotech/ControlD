using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlD;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {


            //Se crea una variable del tipo ImagesMethod para usar los metodos y carga el metodo ImagesMethod con los datos de conexion
            ImagesMethod prueba = new ImagesMethod("Agiotech01","192.168.1.1","aguser", "AgioNet v1.3");

           

            //Muestra en pantalla los datos de la conexion
            Console.WriteLine("password " + prueba.Pass);
            Console.WriteLine("Servidor " + prueba.Server);
            Console.WriteLine("Base de Datos " + prueba.Database);
            Console.WriteLine("Usuario " + prueba.User);
            Console.WriteLine("-------------------------");

            //Se crea una variable del tipo ImageBinar y Extenciones para usar sus atributos
            ImageBinar prueba2 = new ImageBinar();
            Extenciones prueba3 = new Extenciones();

            //se muestra en pantalla un msj que entra en el proceso de guardar
            Console.WriteLine("Aviso entra en la segunda etapa: ");


            /*

            //Se declara una variable de tipo Array byte y se llama al metodo para guardar el documento en formato byte
            byte[] bytes = prueba2.downloadDataToByteArray("C:\\Users\\Gateway User\\Desktop\\Material.docx");
            Console.WriteLine("Leyendo archivo");

            //Se declara una variable de tipo string y se llama el metodo para tomar el tipo de documento que se guarda
            string resultado = prueba3.GetExtension("C:\\Users\\Gateway User\\Desktop\\Material.docx");


            //Se manda a llamar al metodo para guardar los datos pedidos en la BD
            prueba.image_save("SA231002", "Diagnostico", bytes, "Captura Cualquier Archivo", "Agiotech", resultado);

            //Se muestra en pantalla un mensaje que la operacion funsiona y dice que tipo de archivo es
            Console.WriteLine("Archivo guardado");
            Console.WriteLine("El tipo de Archivo es" + resultado);
            
            //mensaje en caso de que la insercion no hubiera podido completarse


    */
            



            ShowFile prueba4 = new ShowFile("Agiotech01", "192.168.1.1", "aguser", "AgioNet v1.3");

            MemoryStream[] Convertir/*String vamos*/  = prueba4.GetFile("SA231001", "LoteRec");
            //byte[] binario = Convertir.ToArray;

            Console.WriteLine(Convertir[0]);

            Console.WriteLine(prueba4.ClassError);

            Console.WriteLine(prueba.ClassError);
            Console.ReadKey();
            
        }
    }
}
