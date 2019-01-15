using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Schema;
using System.Xml.Linq;
using System.Data.Sql;
using System.Data.SqlClient;

namespace xmlc
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            StringWriter stw = new StringWriter();
            String rfc1, cre;

            
           var cc = new xml.coneccion();
            cc.conectarbd.Open();
            SqlCommand cmd = new SqlCommand();
            //cc.abrir();

            cmd.CommandText = "XMLC";
            // cmd.CommandText = sql;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cc.conectarbd;
            cmd.Parameters.AddWithValue("@FECHA", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            DataTable[] tabla = new DataTable[0];

            using (SqlDataAdapter data = new SqlDataAdapter(cmd))
            {

                using (DataSet set = new DataSet())
                {
                    data.Fill(set);
                    tabla = set.Tables.Cast<DataTable>().ToArray();

                }
            }



            var cv = new ControlesVolumetricos();



            XmlSerializer x = new XmlSerializer(cv.GetType());


            cv.rfc = tabla[0].Rows[0]["rfc"].ToString();
            cv.rfcProveedorSw = tabla[0].Rows[0]["RFCPSWR"].ToString();
            cv.numeroPermisoCRE = tabla[0].Rows[0]["CLVCRE"].ToString();
            cv.sello = "aefd32f2f";
            cv.noCertificado = "qwertyuijkhby6gft5ry";
            cv.certificado = "asd";
            cv.fechaYHoraCorte = Convert.ToDateTime(tabla[0].Rows[0]["FECHA"]);


            int nte = 1;
            int fexi = tabla[nte].Rows.Count;
            cv.EXI = new ControlesVolumetricosEXI[fexi];
            for (int ee = 0; ee < fexi; ee++)
            {
                cv.EXI[ee] = new ControlesVolumetricosEXI()
                {
                    numeroTanque = Convert.ToInt32(tabla[nte].Rows[ee]["numeroTanque"]),
                    claveProducto = (t_claveProductos)Enum.Parse(typeof(t_claveProductos), "Item" + tabla[nte].Rows[ee]["V_clvpro"].ToString()),
                    claveSubProducto = (t_subproductos)Enum.Parse(typeof(t_subproductos), "Item" + tabla[nte].Rows[ee]["V_clvsubpro"].ToString()),
                    composicionOctanajeDeGasolina = Convert.ToInt32(tabla[nte].Rows[ee]["V_CocGas"]),
                    composicionOctanajeDeGasolinaSpecified = true,
                    gasolinaConEtanol = (ControlesVolumetricosEXIGasolinaConEtanol)Enum.Parse(typeof(ControlesVolumetricosEXIGasolinaConEtanol), tabla[nte].Rows[ee]["V_GasEtn"].ToString()),
                    gasolinaConEtanolSpecified = true,
                    composicionDeEtanolEnGasolina = Convert.ToInt32(tabla[nte].Rows[ee]["V_CetnGas"]),
                    composicionDeEtanolEnGasolinaSpecified = true,
                    otros = tabla[nte].Rows[ee]["V_Otros"].ToString(),
                    marca = (t_claveMarca)Enum.Parse(typeof(t_claveMarca), "Item" + tabla[nte].Rows[ee]["V_Marca"].ToString()),
                    marcaSpecified = true,
                    volumenUtil = Convert.ToUInt32(tabla[nte].Rows[ee]["volumenutil"]),
                    volumenFondaje = Convert.ToUInt32(tabla[nte].Rows[ee]["volumenfondaje"]),
                    volumenAgua = Convert.ToUInt32(tabla[nte].Rows[ee]["volumenagua"]),
                    volumenDisponible = Convert.ToUInt32(tabla[nte].Rows[ee]["volumendisponible"]),
                    volumenExtraccion = Convert.ToUInt32(tabla[nte].Rows[ee]["volumenextraccion"]),
                    volumenRecepcion = Convert.ToUInt32(tabla[nte].Rows[ee]["volumenrecepcion"]),
                    temperatura = Convert.ToUInt32(tabla[nte].Rows[ee]["temperatura"]),
                    fechaYHoraEstaMedicion = Convert.ToDateTime(tabla[nte].Rows[ee]["fechaYHoraEstaMedicion"]),
                    fechaYHoraMedicionAnterior = Convert.ToDateTime(tabla[nte].Rows[ee]["fechaYHoraMedicionAnterior"]),

                };
            }


            cv.REC = new ControlesVolumetricosREC()
            {
                totalRecepciones = Convert.ToInt32(tabla[2].Rows[0]["totalRecepciones"]),
                totalDocumentos = Convert.ToInt32(tabla[2].Rows[0]["totalDocumentos"]),
                RECCabecera = new[] { new ControlesVolumetricosRECRECCabecera() {

                    folioUnicoRecepcion = 123,
                               claveProducto = t_claveProductos.Item03,
                               claveSubProducto = t_subproductos.Item1,
                               composicionOctanajeDeGasolina = 87,
                            composicionOctanajeDeGasolinaSpecified = true,
                            gasolinaConEtanol = ControlesVolumetricosRECRECCabeceraGasolinaConEtanol.No,
                            gasolinaConEtanolSpecified=true,
                            composicionDeEtanolEnGasolina = 7,
                            composicionDeEtanolEnGasolinaSpecified = true,
                            otros = "adsasd",
                            marca =t_claveMarca.Item10,
                            marcaSpecified=true,
                            folioUnicoRelacion = 21,


                }




                },
                RECDetalle = new[] {new ControlesVolumetricosRECRECDetalle() {

                                folioUnicoRecepcion= 123,
                                numeroDeTanque = 12,
                                volumenInicialTanque= 12,
                                volumenFinalTanque=23,
                                volumenRecepcion = 21,
                                temperatura = 30,
                                fechaYHoraRecepcion= DateTime.Today,
                                folioUnicoRelacion = 1323,




                            } },

                RECDocumentos = new[] {new ControlesVolumetricosRECRECDocumentos()
                            {

                                folioUnicoRecepcion=21231,
                                terminalAlmacenamientoYDistribucion = "556",
                                permisoAlmacenamientoYDistribucion = "sdqd13",
                                tipoDocumento = ControlesVolumetricosRECRECDocumentosTipoDocumento.CP,
                                fechaDocumento = DateTime.Today,
                                folioDocumentoRecepcion = "21323",
                                volumenDocumentado = 212,
                                precioCompra =18,
                                permisoTransporte = "123SD",
                                claveVehiculo = "ASD",
                                folioUnicoRelacion = 232,
                                tipoProveedor = ControlesVolumetricosRECRECDocumentosTipoProveedor.Nacional ,

                                permisoImportacion = "123sd",
                                rfcProveedor = "IRE040610748",
                                nombreProveedor = "PEMEX",
                                permisoProveedor = "ASD32",

                            } },

            };







            cv.VTA = new ControlesVolumetricosVTA()
            {
                numTotalRegistrosDetalle = Convert.ToInt32(tabla[3].Rows[0]["TOTALREGISTROS"]),
            };

            int ntvc = 4;
            int ftvc = tabla[ntvc].Rows.Count;
            cv.VTA.VTACabecera = new ControlesVolumetricosVTAVTACabecera[ftvc];

            for (int vtac = 0; vtac < ftvc; vtac++)
            {
                cv.VTA.VTACabecera[vtac] = new ControlesVolumetricosVTAVTACabecera()
                {
                    numeroTotalRegistrosDetalle = Convert.ToInt32(tabla[ntvc].Rows[vtac]["NUMEROTOTALREGISTROS"]),
                    numeroDispensario = Convert.ToInt32(tabla[ntvc].Rows[vtac]["NDISPENSARIO"]),
                    identificadorManguera = Convert.ToInt32(tabla[ntvc].Rows[vtac]["NMANGUERA"]),
                    claveProducto = (t_claveProductos)Enum.Parse(typeof(t_claveProductos), "Item" + tabla[ntvc].Rows[vtac]["V_clvpro"].ToString()),
                    claveSubProducto = (t_subproductos)Enum.Parse(typeof(t_subproductos), "Item" + tabla[ntvc].Rows[vtac]["V_clvsubpro"].ToString()),
                    composicionOctanajeDeGasolina = Convert.ToInt32(tabla[ntvc].Rows[vtac]["V_CocGas"]),
                    composicionOctanajeDeGasolinaSpecified = true,
                    gasolinaConEtanol = (ControlesVolumetricosVTAVTACabeceraGasolinaConEtanol)Enum.Parse(typeof(ControlesVolumetricosVTAVTACabeceraGasolinaConEtanol), tabla[ntvc].Rows[vtac]["V_GasEtn"].ToString()),
                    gasolinaConEtanolSpecified = true,
                    composicionDeEtanolEnGasolina = Convert.ToInt32(tabla[ntvc].Rows[vtac]["V_CetnGas"]),
                    composicionDeEtanolEnGasolinaSpecified = true,
                    otros = tabla[ntvc].Rows[vtac]["V_Otros"].ToString(),
                    marca = (t_claveMarca)Enum.Parse(typeof(t_claveMarca), "Item" + tabla[ntvc].Rows[vtac]["V_Marca"].ToString()),
                    marcaSpecified = true,
                    sumatoriaVolumenDespachado = Convert.ToDecimal(tabla[ntvc].Rows[vtac]["SUMATORIAVDESPACHADO"]),
                    sumatoriaVentas = Convert.ToDecimal(tabla[ntvc].Rows[vtac]["SUMATORIAVENTAS"]),
                };
            }

            int ntvd = 5;
            int ftvd = tabla[ntvd].Rows.Count;
            cv.VTA.VTADetalle = new ControlesVolumetricosVTAVTADetalle[ftvd];
            for (int vtad = 0; vtad < ftvd; vtad++)
            {
                cv.VTA.VTADetalle[vtad] = new ControlesVolumetricosVTAVTADetalle()
                {
                    tipoDeRegistro = (ControlesVolumetricosVTAVTADetalleTipoDeRegistro)Enum.Parse(typeof(ControlesVolumetricosVTAVTADetalleTipoDeRegistro), tabla[ntvd].Rows[vtad]["TRN_TIPO"].ToString()),
                    numeroUnicoTransaccionVenta = Convert.ToUInt32(tabla[ntvd].Rows[vtad]["TRN_TRNS"]),
                    numeroDispensario = Convert.ToInt32(tabla[ntvd].Rows[vtad]["TRN_DISP"]),
                    identificadorManguera = Convert.ToInt32(tabla[ntvd].Rows[vtad]["DMNG"]),
                    claveProducto = (t_claveProductos)Enum.Parse(typeof(t_claveProductos), "Item" + tabla[ntvd].Rows[vtad]["V_clvpro"].ToString()),
                    claveSubProducto = (t_subproductos)Enum.Parse(typeof(t_subproductos), "Item" + tabla[ntvd].Rows[vtad]["V_clvsubpro"].ToString()),
                    composicionOctanajeDeGasolina = Convert.ToInt32(tabla[ntvd].Rows[vtad]["V_CocGas"]),
                    composicionOctanajeDeGasolinaSpecified = true,
                    gasolinaConEtanol = (ControlesVolumetricosVTAVTADetalleGasolinaConEtanol)Enum.Parse(typeof(ControlesVolumetricosVTAVTADetalleGasolinaConEtanol), tabla[ntvd].Rows[vtad]["V_GasEtn"].ToString()),
                    gasolinaConEtanolSpecified = true,
                    composicionDeEtanolEnGasolina = Convert.ToDecimal(tabla[ntvd].Rows[vtad]["V_CetnGas"]),
                    composicionDeEtanolEnGasolinaSpecified = true,
                    otros = tabla[ntvd].Rows[vtad]["V_Otros"].ToString(),
                    marca = (t_claveMarca)Enum.Parse(typeof(t_claveMarca), "Item" + tabla[ntvd].Rows[vtad]["V_Marca"].ToString()),
                    marcaSpecified = true,
                    volumenDespachado = Convert.ToDecimal(tabla[ntvd].Rows[vtad]["TRN_LTRS"]),
                    precioUnitarioProducto = Convert.ToDecimal(tabla[ntvd].Rows[vtad]["TRN_PREC"]),
                    importeTotalTransaccion = Convert.ToDecimal(tabla[ntvd].Rows[vtad]["TRN_IMPO"]),
                    fechaYHoraTransaccionVenta = Convert.ToDateTime(tabla[ntvd].Rows[vtad]["FECHA"]),

                };
            }





            int ntt = 6;
            int filatqs = tabla[ntt].Rows.Count;
            cv.TQS = new ControlesVolumetricosTQS[filatqs];
            for (int i = 0; i < filatqs; i++)
            {
                cv.TQS[i] = new ControlesVolumetricosTQS()
                {
                    numeroTanque = Convert.ToInt32(tabla[ntt].Rows[i]["V_Tanq"]),
                    claveProducto = (t_claveProductos)Enum.Parse(typeof(t_claveProductos), "Item" + tabla[ntt].Rows[i]["V_clvpro"].ToString()),
                    claveSubProducto = (t_subproductos)Enum.Parse(typeof(t_subproductos), "Item" + tabla[ntt].Rows[i]["V_clvsubpro"].ToString()),
                    composicionOctanajeDeGasolina = Convert.ToInt32(tabla[ntt].Rows[i]["V_CocGas"]),
                    composicionOctanajeDeGasolinaSpecified = true,
                    gasolinaConEtanol = (ControlesVolumetricosTQSGasolinaConEtanol)Enum.Parse(typeof(ControlesVolumetricosTQSGasolinaConEtanol), tabla[ntt].Rows[i]["V_GasEtn"].ToString()),
                    gasolinaConEtanolSpecified = true,
                    composicionDeEtanolEnGasolina = Convert.ToInt32(tabla[ntt].Rows[i]["V_CetnGas"]),
                    composicionDeEtanolEnGasolinaSpecified = true,
                    otros = tabla[ntt].Rows[i]["V_Otros"].ToString(),
                    marca = (t_claveMarca)Enum.Parse(typeof(t_claveMarca), "Item" + tabla[ntt].Rows[i]["V_Marca"].ToString()),
                    marcaSpecified = true,
                    capacidadTotalTanque = Convert.ToUInt32(tabla[ntt].Rows[i]["V_CTT"]),
                    capacidadOperativaTanque = Convert.ToUInt32(tabla[ntt].Rows[i]["V_COT"]),
                    capacidadUtilTanque = Convert.ToUInt32(tabla[ntt].Rows[i]["V_CUT"]),
                    capacidadFondajeTanque = Convert.ToUInt32(tabla[ntt].Rows[i]["V_CFT"]),
                    volumenMinimoOperacion = Convert.ToUInt32(tabla[ntt].Rows[i]["V_VMO"]),
                    estadoTanque = (ControlesVolumetricosTQSEstadoTanque)Enum.Parse(typeof(ControlesVolumetricosTQSEstadoTanque), tabla[ntt].Rows[i]["V_EST"].ToString()),



                };

            }
            int ndis = 7;
            int fdis = tabla[ndis].Rows.Count;
            cv.DIS = new ControlesVolumetricosDIS[fdis];
            for (int d = 0; d < fdis; d++)
            {
                cv.DIS[d] = new ControlesVolumetricosDIS()
                {

                    numeroDispensario = Convert.ToInt32(tabla[ndis].Rows[d]["NDISPENSARIO"]),
                    identificadorManguera = Convert.ToInt32(tabla[ndis].Rows[d]["NMANGUERA"]),
                    claveProducto = (t_claveProductos)Enum.Parse(typeof(t_claveProductos), "Item" + tabla[ndis].Rows[d]["V_clvpro"].ToString()),
                    claveSubProducto = (t_subproductos)Enum.Parse(typeof(t_subproductos), "Item" + tabla[ndis].Rows[d]["V_clvsubpro"].ToString()),
                    composicionOctanajeDeGasolina = Convert.ToInt32(tabla[ndis].Rows[d]["V_CocGas"]),
                    composicionOctanajeDeGasolinaSpecified = true,
                    gasolinaConEtanol = (ControlesVolumetricosDISGasolinaConEtanol)Enum.Parse(typeof(ControlesVolumetricosDISGasolinaConEtanol), tabla[ndis].Rows[d]["V_GasEtn"].ToString()),
                    gasolinaConEtanolSpecified = true,
                    composicionDeEtanolEnGasolina = Convert.ToDecimal(tabla[ndis].Rows[d]["V_CetnGas"]),
                    composicionDeEtanolEnGasolinaSpecified = true,
                    otros = tabla[ndis].Rows[d]["V_Otros"].ToString(),
                    marca = (t_claveMarca)Enum.Parse(typeof(t_claveMarca), "Item" + tabla[ndis].Rows[d]["V_Marca"].ToString()),
                    marcaSpecified = true,
                };


            }


            rfc1 = cv.rfc;
            cre = cv.numeroPermisoCRE.Replace("/", "_");

            //Inicializa el serializador con el tipo Notificación




            //Convierte a XML y lo almacena en un StringWriter
            x.Serialize(stw, cv);

            String xml = stw.ToString().Replace("utf-16", "utf-8");
            //Imprimimos el resultado 



            String ExtensionArchivo = "xml";

            String NombreArchivo = cre + DateTime.Now.AddDays(-1).ToString("yyyyMMdd.235959") + rfc1 + "." + ExtensionArchivo;

            String ruta = @"C:\Users\tresd\Documents\Visual Studio 2015\Projects\xmlc\xmlc\bin\Debug\" + NombreArchivo;


  

            using (StreamWriter escritor = new StreamWriter(ruta))

            {

                escritor.WriteLine(xml);
                acep acep = new acep();
                acep.ShowDialog();
            }

            XmlReaderSettings booksSettings = new XmlReaderSettings();
            booksSettings.Schemas.Add("http://www.sat.gob.mx/esquemas/controlesvolumetricos", "ControlesVolumetricos_v1.2.xsd");
            booksSettings.ValidationType = ValidationType.Schema;
            booksSettings.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);

            XmlReader books = XmlReader.Create(NombreArchivo, booksSettings);

            while (books.Read()) { }



            // Console.ReadKey();



        }

        static void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {


            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.Write("WARNING: ");
                Console.WriteLine(e.Message);

            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.Write("ERROR: ");
                Console.WriteLine(e.Message);

            }


           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

       


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
  
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

            
     
        }

     
    }
}
