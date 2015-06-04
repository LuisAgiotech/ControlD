using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;

namespace ControlD
{
    /// <summary>
    /// Esta clase contien unacamente las variables que se van a usar para guardar los datos en la BD
    /// utiliza unacamente metodos GET y SET de cada variable
    /// </summary>
    public class Class1
    {

        //Variables para la tabla de imagenes
        private int _ID;
        private String _OrderID;
        private String _ImageReference;
        private Byte _Imagen;
        private String _Comment;
        private DateTime _CreateDate;
        private String _Usuario;


        //Variable para el control de errores

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public String OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        public String ImageReference
        {
            get { return _ImageReference; }
            set { _ImageReference = value; }
        }

        public Byte Image
        {
            get { return _Imagen; }
            set { _Imagen = value; }
        }

        public String comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        public DateTime CreateData
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        public String Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

    }// Fin de la primer clase


    /// <summary>
    /// En esta clase se piden los datos para la conexion y los datos que se van a insertar
    /// se utiliza el metodo image_save para poder ejecutar el procedimiento para insertar
    /// los archivos en la BD
    /// </summary>
    public class ImagesMethod
    {
        //variable para mostrar mensaje en caso de cualquier error en la hora de guardar los datos
        private String _ClassError;

        //GET y SET de la variable error
        public String ClassError
        {
            get { return _ClassError; }
            set { _ClassError = value; }
        }


        //variables para conectarce y leer la BD, contiene password, usuario, contraseña, servidor y nombre de la BD
        public SqlDataReader DR;
        public String Database;
        public String Pass;
        public String Server;
        public String User;


        //variable para dar acceso a la BD
        public DataAccess.DataAccess DA;// = new DataAccess.DataAccess("AgioNet v1.3", "Agiotech01", "192.168.1.1", "aguser");

        //Constructor de la clase que pide los datos necesarios para la coneccion con la BD
        public ImagesMethod(String Pass, String Server, String User, String Database)
        {
            this.Pass = Pass;
            this.Server = Server;
            this.User = User;
            this.Database = Database;

            DA = new DataAccess.DataAccess(Server, Database, User, Pass);

        }

        //Metodo image_save que pide los datos para insertar en la tabla de la BD 
        public void image_save(String OrderID, String References, byte[] Image, String Comment, String User, String Typ)
        {
            try
            {
                //Se ejecuta el Procedimiento Almacenado para insertar Datos
                DR = DA.ExecuteSP("sp_image_image_insert", OrderID, References, Image, Comment, User, Typ);

                if (DA.LastErrorMessage != "")
                {
                    throw new Exception(DA.LastErrorMessage);
                }
            }
            catch (Exception e)
            {
                _ClassError = e.Message;
            }
        }


    }// fin segunda clase


    /// <summary>
    /// Clase ImageBinar que funsiona para convertir los archivos que se quieren guardar en un tipo byte
    /// utiliza la el metodo downloadDataToByteArray que recibe la direccion del archivo(puede ser como 
    /// ...Documentos/minuta.docx), toma el archivo y lo combierte en un arreglo binario.
    /// </summary>
    public class ImageBinar
    {
        public byte[] downloadDataToByteArray(string url)
        {
            //Variable de Arreglo byte que contendra el archivo subido
            byte[] Convertir = new byte[0];
            try
            {
                //se crean variables para tomar el archivo con Webrequest y WebResponse 
                //y se combierte el archivo a una variable de tipo Stream
                WebRequest req = WebRequest.Create(url);
                WebResponse response = req.GetResponse();
                Stream stream = response.GetResponseStream();

                // variables parte del codigo que se usa para los byte del archivo
                byte[] buffer = new byte[1024];
                int dataLength = (int)response.ContentLength;
                MemoryStream memStream = new MemoryStream();
                int bytesRead;

                //siclo para igualar el archivo en una variable de tipo MemoryStream
                do
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    memStream.Write(buffer, 0, bytesRead);
                } while (bytesRead != 0);


                //se iguala el resultado a la primer variable de arreglo byte
                Convertir = memStream.ToArray();
                stream.Close();
                memStream.Close();
            }
            catch (Exception) { throw; }

            //Se retorna el resultado en tipo byte
            return Convertir;
        }
    }//Fin del methodo para guardar archivo


    /// <summary>
    /// La Clase Extenciones funsiona para poder tomar la estencion de los archivos que se pretenden guardar
    /// Dentro de la clase se utiiza el metodo GetExtension que recibe un dato String(la direccion del archivo)
    /// y retorna otro dato String(la extencion del archivo que se va a guardar)
    /// 
    /// ejemplo:
    /// string Resultado = Path.GetExtension(Direccion URL del archivo); // la direccion puede ser como .../imagenes/imagen.jpg
    /// return resultado  //retorna la pura extension del archivo en un caracter de texto
    /// </summary>
    public class Extenciones
    {
        public string GetExtension(string Extensiones)
        {
            string Result = Path.GetExtension(Extensiones); // returns .exe
            return Result;
        }

    }//Fin del metodo para tomar la extencion de archivo


    public class ShowFile
    {

        public SqlDataReader DR;
        public String Database;
        public String Pass;
        public String Server;
        public String User;

        public static SqlConnection conn = new SqlConnection("Server=192.168.1.1; Database= AgioNet v1.3; User Id=aguser; Password=Agiotech01");

        private String _ClassError;

        //GET y SET de la variable error
        public String ClassError
        {
            get { return _ClassError; }
            set { _ClassError = value; }
        }

        //variable para dar acceso a la BD
        public DataAccess.DataAccess DA;

        public string GetExtension(string link)
        {
            string Resultado = link;
            return link;
        }

        public ShowFile(String Pass, String Server, String User, String Database)
        {
            this.Pass = Pass;
            this.Server = Server;
            this.User = User;
            this.Database = Database;

            DA = new DataAccess.DataAccess(Server, Database, User, Pass);

        }


        public MemoryStream[] GetFile(String OrderID, String References)
        {

           // String dat = "no pasa valor";
            MemoryStream[] mst = new MemoryStream[100];
            int suma = 0;
            try
            {

                byte[] Convertir = new byte[suma];

                //Se ejecuta el Procedimiento Almacenado para insertar Datos
                DR = DA.ExecuteSP("sp_image_image_getByRef", OrderID, References);
                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        for (int i = 0; i < DR.VisibleFieldCount; i++)
                        { 
                            //Convertir.SetValue(Convert.ToByte(DR.GetString(4)), 0);
                            Convertir[i] = Convert.ToByte(DR.GetString(4));
                            mst[i] = new MemoryStream(Convertir);
                        }


                            //Convertir.SetValue(Convert.ToByte(DR.GetString(4)), 0);
                       /* Convertir[].SetValue(Convert.ToByte(DR.GetString(4)), 0);
                        mst[suma] = new MemoryStream(Convertir);*/


                        // dat = DR.GetString(4);

                    }
                }
            }
            catch (Exception e)
            {
                //dat = "fallaste";
                _ClassError = e.Message;
            }
            return mst; 
            //return dat;
        }


    }// Fin de la clase ShowFile

}